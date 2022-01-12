using Autodesk.AutoCAD.Geometry;
using AutoDetailers.Structures.WellDetailer.Entities.Enums;
using Structures.Utilities;
using Structures.WellDetailer.Entities.Enums;
using System;

namespace WellDetailing.WellDetailer.Entities
{
    public class SemiCircularHood : Hood
    {
        public double Radius { get; private set; }
        public Orientation Orientation { get; private set; }
        public Direction Direction{ get; private set; }
        private double startAngle;
        private double endAngle;

        public SemiCircularHood()
        {
        }
        public SemiCircularHood(double horizontalGap, double verticalGap,  Point3d position, double radius, Orientation orientation) 
        : base(horizontalGap, verticalGap, position) 
        {
            Radius = radius;
            Orientation = orientation;
            InternalShiftness = 2 * radius;
            SecondShiftness = radius;
            SetAngles();
        }

        public override void DrawHoodUpperView(Point3d CenterHoodInUpperView)
        {
            
            Point3d HoodCenter = new Point3d(CenterHoodInUpperView.X + Position.X, CenterHoodInUpperView.Y + Position.Y, 0);

            string insidelayer = "2";
            DrawingShapes.DrawArc(HoodCenter, Radius, startAngle, endAngle, insidelayer);

            string outsidelayer = "3";
            double externalSideHood = Radius + HorizontalGap;
            DrawingShapes.DrawArc(HoodCenter, externalSideHood, startAngle, endAngle, outsidelayer);
        }
        private void SetAngles()
        {
            if(Orientation == (Orientation) Enum.Parse(typeof(Orientation), "Horizontal", true)) 
                if (Direction == (Direction) Enum.Parse(typeof(Direction), "Positive", true))
                {
                    startAngle = 0;
                    endAngle = 180.0;
                }
                else 
                {
                    startAngle = 0.0;
                    endAngle = -180.0;
                }
            else
                if (Direction == (Direction)Enum.Parse(typeof(Direction), "Positive", true))
                {
                    startAngle = 90.0;
                    endAngle = -90.0;
                }
                else
                {
                    startAngle = 90.0;
                    endAngle = 270.0;
                } 
        }
        public override void DrawHoodUpperViewAnnotations(Point3d CenterHoodInUpperView)
        {

        }

    }
}