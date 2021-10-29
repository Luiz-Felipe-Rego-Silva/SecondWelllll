using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.EditorInput;

namespace Structures.Utilities
{
    class DrawingShapes
    {
        public static void DrawCircle(Point3d center, double diameter, string layer)
        {
            Circle Circle = new Circle();
            Circle.Center = center;
            Circle.Radius = diameter / 2.0;
            Circle.Layer = layer;
            DrawingUtilities.AddToDrawing(Circle);
        }
        public static void DrawCircle(Point3d center, double diameter, string layer, string lineType)
        {
            Circle Circle = new Circle();
            Circle.Center = center;
            Circle.Radius = diameter / 2.0;
            Circle.Layer = layer;
            Circle.Linetype = lineType;
            DrawingUtilities.AddToDrawing(Circle);
        }

        public static void DrawArc(Point3d center, double radius, double startAngle, double endAngle, string layer)
        {
            Arc Arc = new Arc(center, radius, DegrresToRadians(startAngle), DegrresToRadians(endAngle));
            Arc.Layer = layer;
            DrawingUtilities.AddToDrawing(Arc);
        }

        public static double DegrresToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static void DrawLine(Point3d startPoint, Point3d endPoint, string layer)
        {
            Polyline line = new Polyline();
            line.Layer = layer;
            line.AddVertexAt(0, new Point2d(startPoint.X, startPoint.Y), 0, 0, 0);
            line.AddVertexAt(0, new Point2d(endPoint.X, endPoint.Y), 0, 0, 0);

            DrawingUtilities.AddToDrawing(line);
        }
        public static void DrawLine(Point3d startPoint, Point3d endPoint, string layer, string lineType)
        {
            Polyline line = new Polyline();
            line.Layer = layer;
            line.Linetype = lineType;
            line.AddVertexAt(0, new Point2d(startPoint.X, startPoint.Y), 0, 0, 0);
            line.AddVertexAt(0, new Point2d(endPoint.X, endPoint.Y), 0, 0, 0);

            DrawingUtilities.AddToDrawing(line);
        }
        public static void DrawRectangle(Point3d Center, double X_length, double Y_length, string layer)
        {
            Point3d LeftUp = new Point3d(Center.X - X_length / 2.0, Center.Y + Y_length / 2.0, 0);
            Point3d LeftDown = new Point3d(Center.X - X_length / 2.0, Center.Y - Y_length / 2.0, 0);
            Point3d RightDown = new Point3d(Center.X + X_length / 2.0, Center.Y - Y_length / 2.0, 0);
            Point3d RightUp = new Point3d(Center.X + X_length / 2.0, Center.Y + Y_length / 2.0, 0);

            DrawLine(LeftUp, RightUp, layer);
            DrawLine(RightUp, RightDown, layer);
            DrawLine(RightDown, LeftDown, layer);
            DrawLine(LeftDown, LeftUp, layer);
        }
        public static void AddQuotesInPolylines(Polyline polyline, double xPadding, double yPadding)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            Transaction transaction = database.TransactionManager.StartTransaction();
            using (DocumentLock documentLock = document.LockDocument())
            {
                int numberOfVertices = polyline.NumberOfVertices;
                for (int i = 0; i < numberOfVertices - 1; i++)
                {
                    AddAlignedDimension("1-50", polyline.GetPoint3dAt(i), polyline.GetPoint3dAt(i + 1), xPadding, yPadding);
                }
                transaction.Commit();
            }
        }
        public static void AddAlignedDimension(string dimStyleName, Point3d startPoint, Point3d endPoint)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            DocumentLock documentLock = document.LockDocument();
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
                BlockTableRecord blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                DimStyleTable dimensionStyleTable = (DimStyleTable)transaction.GetObject(database.DimStyleTableId, OpenMode.ForWrite);
                DimStyleTableRecord dimensionStyleTableRecord = new DimStyleTableRecord();

                ObjectId dimensionStyleId = dimensionStyleTable[dimStyleName];
                string textContent = (Math.Round(Distance(startPoint, endPoint))).ToString();
                AlignedDimension cota = new AlignedDimension(startPoint, endPoint, MiddlePoint(startPoint, endPoint), textContent, dimensionStyleId) { Layer = "1" };

                blockTableRecord.AppendEntity(cota);
                transaction.AddNewlyCreatedDBObject(cota, true);
                documentLock.Dispose();
                transaction.Commit();
            }
        }
        public static void AddAlignedDimension(string dimStyleName, Point3d startPoint, Point3d endPoint, string textContent)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            Database database = document.Database;
            DocumentLock documentLock = document.LockDocument();
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
                BlockTableRecord blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                DimStyleTable dimensionStyleTable = (DimStyleTable)transaction.GetObject(database.DimStyleTableId, OpenMode.ForWrite);
                DimStyleTableRecord dimensionStyleTableRecord = new DimStyleTableRecord();
                ObjectId dimensionStyleId = dimensionStyleTable[dimStyleName];
                AlignedDimension cota = new AlignedDimension(startPoint, endPoint, MiddlePoint(startPoint, endPoint), textContent, dimensionStyleId) { Layer = "1" };

                blockTableRecord.AppendEntity(cota);
                transaction.AddNewlyCreatedDBObject(cota, true);
                documentLock.Dispose();
                transaction.Commit();
            }
        }
        public static void AddAlignedDimension(string dimStyleName, Point3d startPoint, Point3d endPoint, double xPadding, double yPadding)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            Database database = document.Database;
            DocumentLock documentLock = document.LockDocument();
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
                BlockTableRecord blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                DimStyleTable dimensionStyleTable = (DimStyleTable)transaction.GetObject(database.DimStyleTableId, OpenMode.ForWrite);
                DimStyleTableRecord dimensionStyleTableRecord = new DimStyleTableRecord();
                ObjectId dimensionStyleId = dimensionStyleTable[dimStyleName];
                string textContent = (Math.Round(Distance(startPoint, endPoint))).ToString();
                AlignedDimension cota = new AlignedDimension(startPoint, endPoint,
                    new Point3d(MiddlePoint(startPoint, endPoint).X + xPadding,
                    MiddlePoint(startPoint, endPoint).Y + yPadding, 0), textContent, dimensionStyleId)
                { Layer = "1",
                DIM};

                blockTableRecord.AppendEntity(cota);
                transaction.AddNewlyCreatedDBObject(cota, true);
                documentLock.Dispose();
                transaction.Commit();
            }
        }
        public static double Distance(Point3d startPoint, Point3d endPoint)
        {
            double distance = Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
            return Math.Round(distance);
        }
        public static Point3d MiddlePoint(Point3d startPoint, Point3d endPoint)
        {
            Point3d middlePoint = new Point3d((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2, 0);
            return middlePoint;
        }
        public static void AddDiameterDimension() 
        {

        }
    }
}
