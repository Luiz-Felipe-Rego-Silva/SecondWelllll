using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Geometry;
using Structures.WellDetailer.Entities;

namespace WellDetailing.WellDetailer.Entities
{
    public abstract class Hood
    {
        public double HorizontalGap { get;  set; }
        public double VerticalGap { get;  set; }
        public double InternalShiftness {get;  set;}
        public Point3d Position {get;  set;} 

        public Hood()
        {
        }
        public Hood(double horizontalGap, double verticalGap, double internalShiftness, Point3d position)
        {
            HorizontalGap = horizontalGap;
            VerticalGap = verticalGap;
            InternalShiftness = internalShiftness;
            Position = position; 
        }

        public abstract void DrawHoodUpperView(Point3d CenterHoodInUpperView);

        public abstract void DrawHoodUpperViewAnnotations(Point3d CenterHoodInUpperView);

    }
}