using Autodesk.AutoCAD.Geometry;
using Structures.Utilities;

namespace WellDetailing.WellDetailer.Entities
{
    public class CircularHood : Hood
    {
        public double Diameter { get; private set; }
        public CircularHood(double horizontalGap, double verticalGap, Point3d position, double diameter)
        : base(horizontalGap, verticalGap, position)
        {
            Diameter = diameter;
            InternalShiftness = Diameter;
        }
        public override void DrawHoodUpperView(Point3d HoodCenter)
        {
            string insidelayer = "2";
            DrawingShapes.DrawCircle(HoodCenter, Diameter, insidelayer);

            string outsidelayer = "3";
            double externalSideHood = Diameter + 2*HorizontalGap;
            DrawingShapes.DrawCircle(HoodCenter, externalSideHood, outsidelayer);
        }

        public override void DrawHoodUpperViewAnnotations(Point3d CenterHoodInUpperView)
        {
            DrawingShapes.AddRadialDimension(CenterHoodInUpperView, (Diameter + 2 * HorizontalGap) * 0.5, -75.0);
            DrawingShapes.AddRadialDimension(CenterHoodInUpperView, Diameter * 0.5, -115.0);
        }
        public override void DrawAACut(Point3d startAACut)
        {
            
        }



    }
}