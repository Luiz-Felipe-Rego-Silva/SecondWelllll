using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Geometry;
using Structures.WellDetailer.Entities;

namespace WellDetailing.WellDetailer.Entities
{
    public class Hood
    {
        public double HorizontalGap { get; private set; }
        public double VerticalGap { get; private set; }
        public Point3d Position { get; private set; }
        public double InternalShiftness { get; protected set; }
        public double SecondShiftness { get; protected set; }

        public Hood()
        {
        }
        public Hood(double horizontalGap, double verticalGap, Point3d position)
        {
            HorizontalGap = horizontalGap;
            VerticalGap = verticalGap;
            Position = position;
        }

        public virtual void DrawHoodUpperView(Point3d CenterHoodInUpperView) { }

        public virtual void DrawHoodUpperViewAnnotations(Point3d CenterHoodInUpperView) { }
        public virtual void DrawAACut(Point3d startAACut) { }
        public virtual void DrawAACutAnnotations(Point3d startAACut) { }
    }
}