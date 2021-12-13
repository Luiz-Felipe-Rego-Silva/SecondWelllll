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
        public bool defined;
        public List<Line> ListOfLines;
        public double[] LenghtOfLines;
        public double constantParts;
        public VariableDistribuction(StandardDistribuction bar, double constantParts) : base()
        {
            this.Id = bar.Id;
            this.Gauge = bar.Gauge;
            this.Spacing = bar.Spacing;
            this.Length = bar.Length;
            this.IsVariable = true;
            this.Quantity = bar.Quantity;
            this.MarkHandle = bar.MarkHandle;
            defined = false;
            this.constantParts = constantParts;
            this.AmendmentLength = bar.AmendmentLength;
        }
        public void ResgisterDistribuction(List<Line> lines, int amendmentLength)
        {
            double sum = 0.0;
            int numberOfAmendments = 0;
            int index = 0;
            foreach (Line line in lines)
            {
                LenghtOfLines[index] = Math.Floor(line.Length);
                sum += Math.Floor(line.Length);
                numberOfAmendments += Amendments.getNumberOfAmendments(Math.Round(line.Length + constantParts), amendmentLength);
                index++;
            }
            Length = Math.Round(sum / lines.Count);
            NumberOfAmendments = numberOfAmendments;
        }
        public void SetLengths(List<Line> lines)
        {
            LenghtOfLines = new double[lines.Count];
            int index = 0;
            foreach (Line line in lines)
            {
                LenghtOfLines[index] = Math.Floor(line.Length);
                index++;
            }
            ResgisterDistribuction(lines, this.AmendmentLength);
        }
        public void SetLengths(double[] lengths)
        {
            LenghtOfLines = new double[lengths.Length];
            LenghtOfLines = lengths;
        }
        public void SetLengths(List<double> lengths)
        {
            LenghtOfLines = new double[lengths.Count];
            int index = 0;
            foreach (double value in lengths)
            {
                LenghtOfLines[index] = Math.Floor(value);
                NumberOfAmendments += Amendments.getNumberOfAmendments(Math.Round(LenghtOfLines[index] + constantParts), this.AmendmentLength);
                index++;
            }
        }
        public override double GetTotalLength()
        {
            double total = 0.0;
            for (int index = 0; index < LenghtOfLines.Length; index++)
            {
                total += Math.Floor(LenghtOfLines[index] + constantParts);
            }
            total += NumberOfAmendments * AmendmentLength;
            return total;
        }
        public override ResultBuffer ExportResultBuffer()
        {
            int variableState = IsVariable ? 1 : 0;
            return new ResultBuffer(
                new TypedValue(1001, "STRUCTCS"),
                new TypedValue(1000, $"{Id}"),
                new TypedValue(1000, $"{Length}"),
                new TypedValue(1000, $"{Gauge}"),
                new TypedValue(1000, $"{Spacing}"),
                new TypedValue(1000, $"{Quantity}"),
                new TypedValue(1000, $"{MarkHandle}"),
                new TypedValue(1000, $"{variableState}"),
                new TypedValue(1000, $"{NumberOfAmendments}"),
                new TypedValue(1000, $"{AmendmentLength}"),
                new TypedValue(1000, $"{constantParts}"),
                new TypedValue(1000, EncryptLengthToDatabase(LenghtOfLines))
            );
        }
        private static string EncryptLengthToDatabase(double[] VARLength)
        {
            string result = "";
            for (int index = 0; index < VARLength.Length; index++) { result += Math.Round(VARLength[index]).ToString("F0") + "|";}
            return result;
        }
        public static List<Line> OrderLines(List<Line> lines)
        {
            List<Line> sortedList = new List<Line>();
            if (lines.Count > 0)
            {
                Line firstLine = lines[0];
                double dy = Math.Abs(firstLine.EndPoint.Y - firstLine.StartPoint.Y);
                double dx = Math.Abs(firstLine.EndPoint.X - firstLine.StartPoint.X);

                if (dy > dx)
                    sortedList = lines.OrderBy(line => line.StartPoint.X).ToList();
                else
                    sortedList = lines.OrderBy(line => line.StartPoint.Y).ToList();
            }
            return sortedList;
        }


    }
}
