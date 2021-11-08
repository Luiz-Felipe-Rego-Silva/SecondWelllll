using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello_World.Detailing
{
    class StandardDistribuction
    {
        public ObjectId DrawingID { get; private set; }
        public Polyline BarLine { get; private set; }
        public double Gauge { get; private set; }
        public double Spacing { get; private set;}
        public double Length { get; private set; }
        public double Weigth { get; private set; }
        public double quantity { get; private set;}

        public StandardDistribuction(Polyline line, double gauge, double spacing, int quantity)
        {
            Gauge = gauge;
            Spacing = spacing;
            Length = 0.0;
            for (int i = 0; i < line.NumberOfVertices - 1; i++)
            {

            }

        }
    }
}
