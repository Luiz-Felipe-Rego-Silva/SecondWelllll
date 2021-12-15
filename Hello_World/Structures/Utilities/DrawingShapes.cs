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
    public static class DrawingShapes
    {
        public static Point3d Move(Point3d pt, double dx, double dy)
        {
            return new Point3d(pt.X + dx, pt.Y + dy, 0);
        }
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
            Arc Arc = new Arc(center, radius, DegreesToRadians(startAngle), DegreesToRadians(endAngle));
            Arc.Layer = layer;
            DrawingUtilities.AddToDrawing(Arc);
        }
        public static double DegreesToRadians(double angle)
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
        public static void DrawLine(Point3d startPoint, Point3d endPoint, string layer, string lineType, int lineTypeScale)
        {
            Polyline line = new Polyline();
            line.Layer = layer;
            line.Linetype = lineType;
            line.LinetypeScale = lineTypeScale;
            line.AddVertexAt(0, new Point2d(startPoint.X, startPoint.Y), 0, 0, 0);
            line.AddVertexAt(0, new Point2d(endPoint.X, endPoint.Y), 0, 0, 0);

            DrawingUtilities.AddToDrawing(line);
        }
        public static void DrawHorizontalLine(Point3d startPoint, double length, string layer)
        {
            Point3d endPoint = new Point3d(startPoint.X + length, startPoint.Y, 0);
            DrawLine(startPoint, endPoint, layer);
        }
        public static void DrawHorizontalLine(Point3d startPoint, double length, string layer, string lineType)
        {
            Point3d endPoint = new Point3d(startPoint.X + length, startPoint.Y, 0);
            DrawLine(startPoint, endPoint, layer, lineType);
        }
        public static void DrawVerticalLine(Point3d startPoint, double length, string layer)
        {
            Point3d endPoint = new Point3d(startPoint.X, startPoint.Y + length, 0);
            DrawLine(startPoint, endPoint, layer);
        }
        public static void DrawVerticalLine(Point3d startPoint, double length, string layer, string lineType)
        {
            Point3d endPoint = new Point3d(startPoint.X, startPoint.Y + length, 0);
            DrawLine(startPoint, endPoint, layer, lineType);
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
        public static void DrawRectangle(double X_length, double Y_length, string layer, Point3d start)
        {
            Point3d Center = new Point3d(start.X + (X_length / 2.0), start.Y - (Y_length / 2.0), 0);
            DrawRectangle(Center, X_length, Y_length, layer);
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
                transaction.Commit();
                documentLock.Dispose();
            }
        }
        public static void AddAlignedDimension(string dimStyleName, Point3d startPoint, Point3d endPoint, string textContent)
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
                AlignedDimension cota = new AlignedDimension(startPoint, endPoint, MiddlePoint(startPoint, endPoint), textContent, dimensionStyleId) { Layer = "1" };

                blockTableRecord.AppendEntity(cota);
                transaction.AddNewlyCreatedDBObject(cota, true);
                transaction.Commit();
                documentLock.Dispose();
            }
        }
        public static void AddAlignedDimension(string dimStyleName, Point3d startPoint, Point3d endPoint, string textContent, double xPadding, double yPadding)
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
                AlignedDimension cota = new AlignedDimension(startPoint, endPoint, new Point3d(MiddlePoint(startPoint, endPoint).X + xPadding, MiddlePoint(startPoint, endPoint).Y + yPadding, 0), textContent, dimensionStyleId) { Layer = "1" };

                blockTableRecord.AppendEntity(cota);
                transaction.AddNewlyCreatedDBObject(cota, true);
                transaction.Commit();
                documentLock.Dispose();
            }
        }
        public static void AddAlignedDimension(string dimStyleName, Point3d startPoint, Point3d endPoint, double xPadding, double yPadding)
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
                AlignedDimension cota = new AlignedDimension(startPoint, endPoint,
                    new Point3d(MiddlePoint(startPoint, endPoint).X + xPadding,
                    MiddlePoint(startPoint, endPoint).Y + yPadding, 0), textContent, dimensionStyleId)
                { Layer = "1" };
                blockTableRecord.AppendEntity(cota);
                transaction.AddNewlyCreatedDBObject(cota, true);
                transaction.Commit();
                documentLock.Dispose();
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
        public static void AddDiameterDimension(Point3d center, double diameter, double angle)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            DocumentLock documentLock = document.LockDocument();
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                Point3d chordPoint = new Point3d(center.X + diameter * 0.5 * Math.Cos(DegreesToRadians(angle)), center.Y + diameter * 0.5 * Math.Sin(DegreesToRadians(angle)), 0);
                Point3d farChordPoint = new Point3d(center.X - diameter * 0.5 * Math.Cos(DegreesToRadians(angle)), center.Y - diameter * 0.5 * Math.Sin(DegreesToRadians(angle)), 0);
                double leaderLength = 55;
                string textContent = "φ" + (Math.Round(diameter)).ToString();

                BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
                BlockTableRecord blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                DimStyleTable dimensionStyleTable = (DimStyleTable)transaction.GetObject(database.DimStyleTableId, OpenMode.ForWrite);
                DimStyleTableRecord dimensionStyleTableRecord = new DimStyleTableRecord();
                ObjectId dimensionStyleId = dimensionStyleTable["1-50"];

                DiametricDimension diameterDimension = new DiametricDimension(chordPoint, farChordPoint, leaderLength, textContent, dimensionStyleId) { Layer = "1" };
                blockTableRecord.AppendEntity(diameterDimension);
                transaction.AddNewlyCreatedDBObject(diameterDimension, true);
                transaction.Commit();
                documentLock.Dispose();
            }
        }
        public static void AddRadialDimension(Point3d center, double radius, double angle)
        {
            Document document = Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            DocumentLock documentLock = document.LockDocument();
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                Point3d chordPoint = new Point3d(center.X + radius * Math.Cos(DegreesToRadians(angle)), center.Y + radius * Math.Sin(DegreesToRadians(angle)), 0);
                double leaderLength = 55;
                string dimensionText = "R" + (Math.Round(radius)).ToString();
                BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
                BlockTableRecord blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                DimStyleTable dimensionStyleTable = (DimStyleTable)transaction.GetObject(database.DimStyleTableId, OpenMode.ForWrite);
                DimStyleTableRecord dimensionStyleTableRecord = new DimStyleTableRecord();
                ObjectId dimensionStyleId = dimensionStyleTable["1-50"];

                RadialDimension radialDimension = new RadialDimension(center, chordPoint, leaderLength, dimensionText, dimensionStyleId);

                blockTableRecord.AppendEntity(radialDimension);
                transaction.AddNewlyCreatedDBObject(radialDimension, true);
                transaction.Commit();
                documentLock.Dispose();
            }
        }
        public static Point3d MoveVerticalPoint(Point3d point, double yOffset)
        {
            return new Point3d(point.X, point.Y + yOffset, point.Z);
        }
        public static double AngleWith(Point3d startPoint, Point3d endPoint)
        {
            return Math.Atan((startPoint.Y - endPoint.Y) / (startPoint.X - endPoint.X));
        }
        public static double CalculateLinesMediumLength(List<Line> lines)
        {
            double sum = 0;
            foreach (Line line in lines) sum += line.Length;

            return Math.Round(sum / lines.Count, 2);
        }
        public static double Sum(double [] values) 
        {
            double sum = 0.0;
            foreach(double value in values) { sum += value; }
            return sum;
        }

    }
}
