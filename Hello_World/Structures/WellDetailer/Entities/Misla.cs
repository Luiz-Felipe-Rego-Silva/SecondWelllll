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

            for (int i = 1; i < numberOfCircles; i++) { Utilities.DrawingShapes.DrawCircle(bluePrintCenter, MislaInsideDiameter + 2 * efectiveSpacing, layer); }
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
        }
        private void DoPointsInMislaCut(Point3d startMislaPoint, double MislaInsideDiameter) 
        {
            MislaInCut[0] = startMislaPoint;
            MislaInCut[1] = new Point3d(MislaInCut[0].X, MislaInCut[0].Y + Heigth, 0);
            MislaInCut[2] = new Point3d(MislaInCut[0].X + Length, MislaInCut[0].Y, 0);
            
            
            MislaInCut[3] = new Point3d(MislaInCut[0].X + MislaInsideDiameter + 2*Length, MislaInCut[0].Y ,0);
            MislaInCut[4] = new Point3d(MislaInCut[3].X - Length, MislaInCut[3].Y, 0);
            MislaInCut[5] = new Point3d(MislaInCut[3].X, MislaInCut[3].Y + Heigth, 0);
        }
        private void DrawMislaCutLines()
        {
            string layer = "4";
            Utilities.DrawingShapes.DrawLine(MislaInCut[1], MislaInCut[2], layer);
            Utilities.DrawingShapes.DrawLine(MislaInCut[4], MislaInCut[5], layer);
        }

        public void DrawAACutAnnotations()
        {

        }
        public bool HasMisla() 
        {
            if(this != null) { return true; }
            else { return false; }
        }
    }
}
