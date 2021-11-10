using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detailing
{
    class StandardDistribuction
    {
        public int Id { get; private set; }
        public ObjectId DrawingID { get; private set; }
        public Polyline BarLine { get; private set; }
        public double Gauge { get; private set; }
        public double Spacing { get; private set; }
        public double Length { get; set; }
        public double Weigth { get; private set; }
        public double Quantity { get; set; }
        public bool IsVariable { get; set; }
        public int NumberOfAmendments { get; private set; } = 0;
        public double AmendmentLength { get; private set;}
        public long MarkHandle { get; set; }

        public StandardDistribuction(double gauge, double length)
        {
            Gauge = gauge;
            Length = length;
        }
        public StandardDistribuction(int id, Polyline line, double gauge, double spacing, int quantity, int numberOfAmendments, double amendmentLength)
        {
            Id = id;
            Gauge = gauge;
            Spacing = spacing;
            Length = 0.0;
            BarLine = line;
            for (int i = 0; i < line.NumberOfVertices - 1; i++)
            {
                Length += Math.Round(Structures.Utilities.DrawingShapes.Distance(line.GetPoint3dAt(i), line.GetPoint3dAt(i + 1)), 2);
            }
            Quantity = quantity;
            Weigth = Quantity * Math.Round(Math.Round(Length, 2) * GetNominalSteelDensity(), 1);
            NumberOfAmendments = numberOfAmendments;
            AmendmentLength = amendmentLength;
        }
        private double GetNominalSteelDensity()
        {
            int gaugeForswitch = (int)(10 * this.Gauge);
            double nominalSteelDensity = 0.0;
            switch (gaugeForswitch)
            {
                case 50:
                    nominalSteelDensity = 0.154;
                    break;
                case 63:
                    nominalSteelDensity = 0.245;
                    break;
                case 80:
                    nominalSteelDensity = 0.395;
                    break;
                case 100:
                    nominalSteelDensity = 0.617;
                    break;
                case 125:
                    nominalSteelDensity = 0.963;
                    break;
                case 160:
                    nominalSteelDensity = 1.578;
                    break;
                case 200:
                    nominalSteelDensity = 2.466;
                    break;
                case 250:
                    nominalSteelDensity = 3.853;
                    break;
            }
            return nominalSteelDensity;
        }
        public ResultBuffer ExportResultBuffer()
        {
            var variableState = IsVariable ? 1 : 0;

            return new ResultBuffer(
                new TypedValue(1001, "STRUCTCS"),
                new TypedValue(1000, $"{Id}"),
                new TypedValue(1000, $"{Length}"),
                new TypedValue(1000, $"{Gauge}"),
                new TypedValue(1000, $"{Spacing}"),
                new TypedValue(1000, $"{Quantity}"),
                new TypedValue(1000, $"{MarkHandle}"),
                new TypedValue(1000, $"{variableState}")
            );
        }
        public void SetVariable()
        {
            IsVariable = true;
        }
        public void Draw(/*Point3d basePoint, Point3d lastDescriptionPoint, AttachmentPoint attachment*/) 
        {
            DrawingID = Structures.Utilities.DrawingUtilities.DrawObject(BarLine);
            LineToBar();
            //printDescriptionText(basePoint, lastDescriptionPoint, attachment);
            //Structures.Utilities.DrawingShapes.AddQuotesInPolylines(BarLine, 0, 0);
        }
        private void LineToBar()
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            DocumentLock doclock = document.LockDocument();
            Transaction transaction = document.TransactionManager.StartTransaction();
            Entity bar = (Entity) transaction.GetObject(DrawingID, OpenMode.ForWrite);
            if (bar.GetType() == typeof(Polyline))
            {
                AddRegAppTableRecord("STRUCTCS");
                ResultBuffer ResultBuffer =
                    new ResultBuffer(
                        new TypedValue(1001, "STRUCTCS"),
                        new TypedValue(1000, $"{Id}"),
                        new TypedValue(1000, $"{Length}"),
                        new TypedValue(1000, $"{Gauge * 10}"),
                        new TypedValue(1000, $"{Spacing}"),
                        new TypedValue(1000, $"{Quantity}"),
                        new TypedValue(1000, $"{0}"),
                        new TypedValue(1000, $"{0}")
                    );
                bar.XData = ResultBuffer;
                ResultBuffer.Dispose();
                transaction.Commit();
                doclock.Dispose();
            }
        }
        private static void AddRegAppTableRecord(string RegAppName)
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;
            using (Transaction transaction = document.TransactionManager.StartTransaction())
            {
                RegAppTable appTable = (RegAppTable)transaction.GetObject(database.RegAppTableId, OpenMode.ForRead, false);
                if (!appTable.Has(RegAppName))
                {
                    appTable.UpgradeOpen();
                    var appTableRecord = new RegAppTableRecord { Name = RegAppName };
                    appTable.Add(appTableRecord);
                    transaction.AddNewlyCreatedDBObject(appTableRecord, true);
                }
                transaction.Commit();
            }
        }
        private string GetDescriptionText()
        {
            if (NumberOfAmendments < 1) 
            {
                return $"{Quantity} N{Id} φ{Gauge} c.{Spacing} - {Convert.ToString(Math.Round(Length))}";
            }
            else if(NumberOfAmendments == 1)
            {
                return $"{Quantity} N{Id} φ{Gauge} c.{Spacing} - {Convert.ToString(Math.Round(Length))} ({NumberOfAmendments} EMENDA DE {AmendmentLength}CM)";
            }
            else 
            {
                return $"{Quantity} N{Id} φ{Gauge} c.{Spacing} - {Convert.ToString(Math.Round(Length))} ({NumberOfAmendments} EMENDAS DE {AmendmentLength}CM)";
            }
            
        }
        private void printDescriptionText(Point3d startPoint, Point3d endPoint, AttachmentPoint attachmentPoint) 
        {
            Point3d middlePoint = Structures.Utilities.DrawingShapes.MiddlePoint(startPoint, endPoint);
            DBText descriptionText = new DBText
            {
                Height = 10,
                Layer = "3",
                TextString = GetDescriptionText(),
                Justify = attachmentPoint
            };
            descriptionText.AlignmentPoint = middlePoint;
            descriptionText.Rotation = Structures.Utilities.DrawingShapes.AngleWith(startPoint, endPoint);
            MarkHandle = Structures.Utilities.DrawingUtilities.DrawObjectMark(descriptionText);
        }
    }
}
