using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.WellDetailer.Entities
{
    public class Misla
    {
        const double TheoricalSpacingLines = 3.0;
        public double Length { set; get; }
        public double Heigth { set; get; }
        private Point3d[] MislaInCut;

        public Misla()
        {
            MislaInCut = new Point3d[6];
        }
        public Misla(double length, double heigth)
        {
            Length = length;
            Heigth = heigth;
            MislaInCut = new Point3d[6];
        }

        public void DrawBluePrint(Point3d bluePrintCenter, double MislaInsideDiameter)
        {
            DrawFloorLine(bluePrintCenter, MislaInsideDiameter);
            DrawMiddleLines(bluePrintCenter, MislaInsideDiameter);
        }
        private void DrawFloorLine(Point3d bluePrintCenter, double MislaInsideDiameter)
        {
            string layer = "2";
            Utilities.DrawingShapes.DrawCircle(bluePrintCenter, MislaInsideDiameter, layer);
        }
        private void DrawMiddleLines(Point3d bluePrintCenter, double MislaInsideDiameter)
        {
            int numberOfCircles = CalculateNumberOfCircles();
            double efectiveSpacing = CalculateEfectiveSpacingOfCircles();
            string layer = "1";

            for (int i = 1; i < numberOfCircles; i++) { Utilities.DrawingShapes.DrawCircle(bluePrintCenter, MislaInsideDiameter + 2 * efectiveSpacing * i, layer); }
        }
        private int CalculateNumberOfCircles()
        {
            return (int)(Length / TheoricalSpacingLines);
        }
        private double CalculateEfectiveSpacingOfCircles()
        {
            return Length / CalculateNumberOfCircles();
        }

        public void DrawMislaAACut(Point3d startMislaPoint, double MislaInsideDiameter)
        {
            DoPointsInMislaCut(startMislaPoint, MislaInsideDiameter);
            DrawMislaCutLines();
            DrawMislaHatch();
        }
        private void DoPointsInMislaCut(Point3d startMislaPoint, double MislaInsideDiameter)
        {
            MislaInCut[0] = startMislaPoint;
            MislaInCut[1] = new Point3d(MislaInCut[0].X, MislaInCut[0].Y + Heigth, 0);
            MislaInCut[2] = new Point3d(MislaInCut[0].X + Length, MislaInCut[0].Y, 0);


            MislaInCut[3] = new Point3d(MislaInCut[0].X + MislaInsideDiameter + 2 * Length, MislaInCut[0].Y, 0);
            MislaInCut[4] = new Point3d(MislaInCut[3].X - Length, MislaInCut[3].Y, 0);
            MislaInCut[5] = new Point3d(MislaInCut[3].X, MislaInCut[3].Y + Heigth, 0);
        }
        private void DrawMislaCutLines()
        {
            string layer = "4";
            Utilities.DrawingShapes.DrawLine(MislaInCut[1], MislaInCut[2], layer);
            Utilities.DrawingShapes.DrawLine(MislaInCut[4], MislaInCut[5], layer);
        }
        public void DrawMislaHatch()
        {
            Document document = Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = transaction.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;
                using (DocumentLock documentLock = document.LockDocument())
                {
                    BlockTableRecord blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    Polyline leftBoundary = new Polyline();
                    leftBoundary.AddVertexAt(0, new Point2d(MislaInCut[0].X, MislaInCut[0].Y), 0, 0, 0);
                    leftBoundary.AddVertexAt(1, new Point2d(MislaInCut[1].X, MislaInCut[1].Y), 0, 0, 0);
                    leftBoundary.AddVertexAt(2, new Point2d(MislaInCut[2].X, MislaInCut[2].Y), 0, 0, 0);
                    leftBoundary.AddVertexAt(3, new Point2d(MislaInCut[0].X, MislaInCut[0].Y), 0, 0, 0);

                    blockTableRecord.AppendEntity(leftBoundary);
                    transaction.AddNewlyCreatedDBObject(leftBoundary, true);
                    ObjectIdCollection leftHatchBoundary = new ObjectIdCollection();
                    leftHatchBoundary.Add(leftBoundary.ObjectId);

                    Hatch mislaHatch = new Hatch() { Layer = "1", PatternScale = 2.0 };
                    blockTableRecord.AppendEntity(mislaHatch);
                    mislaHatch.Associative = true;
                    mislaHatch.SetHatchPattern(HatchPatternType.PreDefined, "AR-CONC");
                    mislaHatch.AppendLoop(HatchLoopTypes.Outermost, leftHatchBoundary);
                    
                    transaction.AddNewlyCreatedDBObject(mislaHatch, true);
                    transaction.Commit();
                }
            }
        }

        public void DrawAACutAnnotations()
        {
            Polyline heigthLine = new Polyline();
            Polyline lengthLine = new Polyline();

            heigthLine.AddVertexAt(0, new Point2d(MislaInCut[0].X, MislaInCut[0].Y), 0, 0, 0);
            heigthLine.AddVertexAt(1, new Point2d(MislaInCut[1].X, MislaInCut[1].Y), 0, 0, 0);

            lengthLine.AddVertexAt(0, new Point2d(MislaInCut[0].X, MislaInCut[0].Y), 0, 0, 0);
            lengthLine.AddVertexAt(1, new Point2d(MislaInCut[2].X, MislaInCut[2].Y), 0, 0, 0);

            Utilities.DrawingShapes.AddQuotesInPolylines(heigthLine, Length + 20.0, 0.0);
            Utilities.DrawingShapes.AddQuotesInPolylines(lengthLine, 0.0, Heigth + 15.0);
        }
        public bool HasMisla()
        {
            if (this != null) { return true; }
            else { return false; }
        }
    }
}
