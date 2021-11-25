using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Detailing
{
    internal class VariableDistribuction : StandardDistribuction
    {
        public double[] LineLengths { get; set; }
        public double ReferenceLength { get; private set; }

        public VariableDistribuction(int id, Polyline barLine, double gauge, double spacing, int quantity, int numberOfAmendments, double amendmentLength, double[] lineLengths) : base(id, barLine, gauge, spacing, quantity, numberOfAmendments, amendmentLength)
        {
            LineLengths = lineLengths;
            ReferenceLength = barLine.Length;
            SetVariable();
        }
        public void SetVariable()
        {
            IsVariable = true;
        }


    }
}
