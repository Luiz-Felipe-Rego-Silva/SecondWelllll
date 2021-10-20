using Autodesk.AutoCAD.Geometry;
using Structures.Utilities;
using Structures.WellDetailer.Entities.Enums;

namespace WellDetailing.WellDetailer.Entities
{
    public class SemiCircularHood : Hood
    {
        public double Diameter { get; private set; }
        public Orientation Orientation { get; private set; }
        private double startAngle;
        private double endAngle;

        public SemiCircularHood()
        {
        }
        public SemiCircularHood(double horizontalGap, double verticalGap, double internalShiftness, Point3d position, double diameter, Orientation orientation) 
        : base(horizontalGap, verticalGap, internalShiftness, position) 
        {
            Diameter = diameter;
            Orientation = orientation;
            SetAngles();
        }

        public override void DrawHoodUpperView(Point3d CenterHoodInUpperView)
        {
            
            Point3d HoodCenter = new Point3d(CenterHoodInUpperView.X + Position.X, CenterHoodInUpperView.Y + Position.Y, 0);

            string insidelayer = "2";
            DrawingShapes.DrawArc(HoodCenter, Diameter, startAngle, endAngle, insidelayer);

            string outsidelayer = "3";
            double externalSideHood = Diameter + 2*HorizontalGap;
            DrawingShapes.DrawArc(HoodCenter, externalSideHood, startAngle, endAngle, outsidelayer);
        }
        private void SetAngles()
        {
            if(true) //Orientation == "Horizontal"
            {
                startAngle = 0;
                endAngle = 180.0;
            }
            else
            {
                startAngle = 90.0;
                endAngle = -90.0;
            }
        }

        public override void DrawHoodUpperViewAnnotations(Point3d CenterHoodInUpperView)
        {

        }

    }
}