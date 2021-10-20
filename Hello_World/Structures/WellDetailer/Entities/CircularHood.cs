using Autodesk.AutoCAD.Geometry;
using Structures.Utilities;

namespace WellDetailing.WellDetailer.Entities
{
    public class CircularHood : Hood
    {
        public double Diameter { get; private set; }

        public CircularHood() 
        {
        }

        public CircularHood(double horizontalGap, double verticalGap, double internalShiftness, Point3d position, double diameter)
        : base(horizontalGap, verticalGap, internalShiftness, position)
        {
            Diameter = diameter;
        }


        public override void DrawHoodUpperView(Point3d CenterHoodInUpperView)
        {
            Point3d HoodCenter = new Point3d(CenterHoodInUpperView.X + Position.X, CenterHoodInUpperView.Y + Position.Y, 0);
            string insidelayer = "2";
            DrawingShapes.DrawCircle(HoodCenter,InternalShiftness, insidelayer);

            string outsidelayer = "3";
            double externalSideHood = Diameter + 2*HorizontalGap;
            DrawingShapes.DrawCircle(HoodCenter, externalSideHood, outsidelayer);
        }

        public override void DrawHoodUpperViewAnnotations(Point3d CenterHoodInUpperView)
        {

        }


    }
}