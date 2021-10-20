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
            LeanConcretePointsInCut = new Point3d[4];
            HorizontalShiftness = 5.0;
            VerticalShiftness = 5.0;
        }
        public LeanConcrete(double horizontalShiftness, double verticalShiftness, double bottomDiameter)
        {
            BottomDiameter = bottomDiameter;
            LeanConcretePointsInCut = new Point3d[4];
            HorizontalShiftness = 5.0;
            VerticalShiftness = 5.0;
            HorizontalShiftness = horizontalShiftness;
            VerticalShiftness = verticalShiftness;
        }

        public void DrawLeanConcreteInCut(Point3d startPointOfLeanConcreteCut )
        {
            DoPointsLeanConreteInCut(startPointOfLeanConcreteCut);
            DrawLinesLeanConcreteInCut();
            DrawHatchLeanConcrete();
        }
        private void DoPointsLeanConreteInCut(Point3d startPointOfLeanConcreteCut)
        {
            LeanConcretePointsInCut[0] = startPointOfLeanConcreteCut;
            LeanConcretePointsInCut[1] = new Point3d(LeanConcretePointsInCut[0].X, LeanConcretePointsInCut[0].Y - VerticalShiftness, 0);
            LeanConcretePointsInCut[2] = new Point3d(LeanConcretePointsInCut[1].X + 2 * HorizontalShiftness + BottomDiameter, LeanConcretePointsInCut[1].Y, 0);
            LeanConcretePointsInCut[3] = new Point3d(LeanConcretePointsInCut[2].X, LeanConcretePointsInCut[2].Y + VerticalShiftness, 0);
        }
        
        private void DrawLinesLeanConcreteInCut()
        {
            string layer = "1";
            for (int i = 0; i < 3; i++) {DrawingShapes.DrawLine(LeanConcretePointsInCut[i], LeanConcretePointsInCut[i+1], layer);}
        }
        private void DrawHatchLeanConcrete()
        {
            string layer = "1";
        }

    }
}