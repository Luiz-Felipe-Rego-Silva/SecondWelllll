using Autodesk.AutoCAD.Geometry;
using Structures.Utilities;
using System;

namespace WellDetailing.WellDetailer.Entities
{
    public class RectangularHood : Hood
    {
        public double HorizontalLength { get; private set; }
        public double VerticalLength { get; private set; }

        public RectangularHood()
        {
        }
        public RectangularHood(double horizontalGap, double verticalGap, Point3d position, double horizontalLength, double verticalLength) 
        : base(horizontalGap, verticalGap, position) 
        {
            HorizontalLength = horizontalLength;
            VerticalLength = verticalLength;
            InternalShiftness = HorizontalLength;
            SecondShiftness = VerticalLength;
        }

        public override void DrawHoodUpperView(Point3d CenterHoodInUpperView)
        {
            Point3d HoodCenter = new Point3d(CenterHoodInUpperView.X + Position.X, CenterHoodInUpperView.Y + Position.Y, 0);
            
            string insidelayer = "2";
            DrawingShapes.DrawRectangle(HoodCenter,HorizontalLength, VerticalLength, insidelayer);

            string outsidelayer = "3";
            double externalHorizontalHood = HorizontalLength + 2*HorizontalGap;
            double externalVerticalHood = VerticalLength + 2*HorizontalGap;
            DrawingShapes.DrawRectangle(HoodCenter, externalHorizontalHood, externalVerticalHood, outsidelayer);
        }
        private void DrawXLine(Point3d HoodCenter)
        {
            Point3d LeftUp = new Point3d(HoodCenter.X - HorizontalLength/2.0, HoodCenter.Y + VerticalLength/2.0, 0);
            Point3d LeftDown = new Point3d(HoodCenter.X - HorizontalLength/2.0, HoodCenter.Y - VerticalLength/2.0, 0);
            Point3d RightDown = new Point3d(HoodCenter.X + HorizontalLength/2.0, HoodCenter.Y - VerticalLength/2.0, 0);
            Point3d RightUp = new Point3d(HoodCenter.X + HorizontalLength/2.0, HoodCenter.Y + VerticalLength/2.0, 0);

            string layer = "1";
            string lineType = "DASHED";
            DrawingShapes.DrawLine(LeftUp, RightDown, layer, lineType);
            DrawingShapes.DrawLine(RightUp, LeftDown, layer, lineType);
        }

        public override void DrawHoodUpperViewAnnotations(Point3d CenterHoodInUpperView)
        {

        }


    }
}
