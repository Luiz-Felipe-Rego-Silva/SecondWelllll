using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Geometry;
using Structures.WellDetailer.Entities;
using Structures.Utilities;

namespace WellDetailing.WellDetailer.Entities
{
    public class LeanConcrete
    {
        public double HorizontalShiftness { get; private set; }
        public double VerticalShiftness { get; private set; }
        public double BottomDiameter { get; private set; }
        private Point3d[] LeanConcretePointsInCut;

        public LeanConcrete(double bottomDiameter)
        {
            BottomDiameter = bottomDiameter;
            LeanConcretePointsInCut = new Point3d[6];
            HorizontalShiftness = 5.0;
            VerticalShiftness = 5.0;
        }
        public LeanConcrete(double horizontalShiftness, double verticalShiftness, double bottomDiameter)
        {
            BottomDiameter = bottomDiameter;
            LeanConcretePointsInCut = new Point3d[6];
            HorizontalShiftness = horizontalShiftness;
            VerticalShiftness = verticalShiftness;
        }

        public void DrawLeanConcreteInCut(Point3d startPointOfLeanConcreteCut)
        {
            DoPointsLeanConreteInCut(startPointOfLeanConcreteCut);
            DrawLinesLeanConcreteInCut();
            DrawHatchLeanConcrete();
            DrawLeanConcreteAnnotations();
        }
        private void DoPointsLeanConreteInCut(Point3d startPointOfLeanConcreteCut)
        {
            LeanConcretePointsInCut[0] = new Point3d(startPointOfLeanConcreteCut.X - HorizontalShiftness, startPointOfLeanConcreteCut.Y, 0);
            LeanConcretePointsInCut[1] = new Point3d(LeanConcretePointsInCut[0].X, LeanConcretePointsInCut[0].Y - VerticalShiftness, 0);
            LeanConcretePointsInCut[2] = new Point3d(LeanConcretePointsInCut[1].X + 2 * HorizontalShiftness + BottomDiameter, LeanConcretePointsInCut[1].Y, 0);
            LeanConcretePointsInCut[3] = new Point3d(LeanConcretePointsInCut[2].X, LeanConcretePointsInCut[2].Y + VerticalShiftness, 0);
            LeanConcretePointsInCut[4] = new Point3d(LeanConcretePointsInCut[3].X - HorizontalShiftness, LeanConcretePointsInCut[3].Y, 0);
            LeanConcretePointsInCut[5] = new Point3d(LeanConcretePointsInCut[4].X - BottomDiameter, LeanConcretePointsInCut[4].Y, 0);
        }

        private void DrawLinesLeanConcreteInCut()
        {
            string layer = "1";
            for (int i = 0; i < 4; i++) { DrawingShapes.DrawLine(LeanConcretePointsInCut[i], LeanConcretePointsInCut[i + 1], layer); }
            DrawingShapes.DrawLine(LeanConcretePointsInCut[0], LeanConcretePointsInCut[5], layer);
        }
        private void DrawHatchLeanConcrete()
        {
#pragma warning disable CS0219 // The variable 'layer' is assigned but its value is never used
            string layer = "1";
#pragma warning restore CS0219 // The variable 'layer' is assigned but its value is never used
        }
        public void DrawLeanConcreteAnnotations()
        {
            Point3d starPoint = LeanConcretePointsInCut[2];
            Point3d endPointX = new Point3d(starPoint.X - HorizontalShiftness, starPoint.Y, 0);
            Point3d endPointY = LeanConcretePointsInCut[3];

            DrawingShapes.AddAlignedDimension("1-50", starPoint, endPointX, 0, -20);
            DrawingShapes.AddAlignedDimension("1-50", starPoint, endPointY, 20, 0);
        }

    }
}