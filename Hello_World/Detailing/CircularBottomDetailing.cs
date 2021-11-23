using Autodesk.AutoCAD.Geometry;
using Hello_World.Detailing.Entities;

namespace Hello_World.Detailing
{
    class CircularBottomDetailing
    {
        public string Title { get; private set; }
        public double Cover { get; private set; }
        public double AnchorFactor { get; private set; }
        public int multiplier { get; set; }
        public BorderCondition ExternalHorizontalTop { get; private set; }
        public BorderCondition InternalHorizontalTop { get; private set; }
        public BorderCondition ExternalVerticalTop { get; private set; }
        public BorderCondition InternalVerticalTop { get; private set; }
        public BorderCondition ExternalHorizontalBottom { get; private set; }
        public BorderCondition InternalHorizontalBottom { get; private set; }
        public BorderCondition ExternalVerticalBottom { get; private set; }
        public BorderCondition InternalVerticalBottom { get; private set; }
        public double GaugeX { get; private set; }
        public double GaugeY { get; private set; }
        public double SpacingX { get; private set; }
        public double SpacingY { get; private set; }
        private double wallThickness;
        Point3d BasePoint;
        
        public void SetBorderConditions(bool isT) 
        { 
            
        }
    }
}
