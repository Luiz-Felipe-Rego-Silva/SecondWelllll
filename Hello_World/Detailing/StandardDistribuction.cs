using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

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
        public double AmendmentLength { get; private set; }
        public long MarkHandle { get; set; }
        public static double AnchorFactor { get; set; } = 34.0;
        public StandardDistribuction(double gauge, double length)
        {
            Gauge = gauge;
            Length = length;
        }
        public StandardDistribuction(int id, Polyline line, double gauge, double spacing, int quantity, int numberOfAmendments, double amendmentLength)
        {
            Id = id;
            Gauge = 10 * gauge;
            Spacing = spacing;
            Length = 0.0;
            BarLine = line;
            DrawingID = line.ObjectId;
            Length += Math.Round(BarLine.Length, 2);
            Quantity = quantity;
            Weigth = Quantity * Math.Round(Math.Round(Length, 2) * GetNominalSteelDensity(), 1);
            NumberOfAmendments = numberOfAmendments;
            AmendmentLength = amendmentLength;
        }
        public static double getAnchorLength(double gauge)
        {
            return Math.Ceiling(gauge * AnchorFactor);
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
            int variableState = IsVariable ? 1 : 0;

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
        public void Draw(/*Point3d basePoint, Point3d lastDescriptionPoint, AttachmentPoint attachment*/)
        {
            DrawingID = Structures.Utilities.DrawingUtilities.DrawObject(BarLine);
            LineToBar();
        }
        private void LineToBar()
        {
            Document document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            DocumentLock documentLock = document.LockDocument();
            Transaction transaction = document.TransactionManager.StartTransaction();
            Entity bar = (Entity)transaction.GetObject(DrawingID, OpenMode.ForWrite);
            if (bar.GetType() == typeof(Polyline))
            {
                AddRegAppTableRecord("STRUCTCS");
                ResultBuffer ResultBuffer =
                    new ResultBuffer(
                        new TypedValue(1001, "STRUCTCS"),
                        new TypedValue(1000, $"{Id}"),
                        new TypedValue(1000, $"{Length}"),
                        new TypedValue(1000, $"{Gauge}"),
                        new TypedValue(1000, $"{Spacing}"),
                        new TypedValue(1000, $"{Quantity}"),
                        new TypedValue(1000, $"{0}"),
                        new TypedValue(1000, $"{0}")
                    );
                bar.XData = ResultBuffer;
                ResultBuffer.Dispose();
                transaction.Commit();
                documentLock.Dispose();
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
        public string GetDescriptionText()
        {
            if (NumberOfAmendments < 1)
            {
                return $"{Quantity} N{Id} φ{Gauge} c.{Spacing} - {Convert.ToString(Math.Round(Length))}";
            }
            else if (NumberOfAmendments == 1)
            {
                return $"{Quantity} N{Id} φ{Gauge} c.{Spacing} - {Convert.ToString(Math.Round(Length))} ({NumberOfAmendments} EMENDA DE {AmendmentLength}CM)";
            }
            else
            {
                return $"{Quantity} N{Id} φ{Gauge} c.{Spacing} - {Convert.ToString(Math.Round(Length))} ({NumberOfAmendments} EMENDAS DE {AmendmentLength}CM)";
            }

        }
        public void PrintDescriptionText(Point3d middlePoint, double orientation)
        {
            DBText descriptionText = new DBText
            {
                Height = 10,
                Layer = "3",
                TextString = GetDescriptionText(),
                Justify = AttachmentPoint.MiddleCenter,
                Rotation = orientation,
                AlignmentPoint = middlePoint
            };
            MarkHandle = Structures.Utilities.DrawingUtilities.DrawObjectMark(descriptionText);
        }
        public void PrintDescriptionText(Point3d middlePoint, double orientation, string content)
        {
            DBText descriptionText = new DBText
            {
                Height = 10,
                Layer = "3",
                TextString = content,
                Justify = AttachmentPoint.MiddleCenter,
                Rotation = orientation,
                AlignmentPoint = middlePoint
            };
            MarkHandle = Structures.Utilities.DrawingUtilities.DrawObjectMark(descriptionText);
        }
        public static void UpdateBars(List<StandardDistribuction> list)
        {
            Document document = Application.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            Database database = document.Database;
            DocumentLock documentLock = document.LockDocument();
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                foreach (StandardDistribuction bar in list)
                {
                    Polyline barPolyline = (Polyline)transaction.GetObject(bar.DrawingID, OpenMode.ForWrite);
                    barPolyline.XData = bar.ExportResultBuffer();
                    //Tentando incorporar a mudança do texto do ferro
                    long _handle = Convert.ToInt64(barPolyline.XData.AsArray()[6].Value.ToString());
                    if (_handle != 0)
                    {
                        Handle handle = new Handle(_handle);
                        ObjectId textObjId = database.GetObjectId(false, handle, 0);
                        DBText barText = (DBText)transaction.GetObject(textObjId, OpenMode.ForWrite);
                        if (!barText.IsErased)
                            barText.TextString = bar.GetDescriptionText();
                        else
                            editor.WriteMessage("Alguma barra selecionada possui identificação nula.");
                    }
                }
                documentLock.Dispose();
                transaction.Commit();
            }
        }
        public static List<StandardDistribuction> CreateReIndexedBarsList(List<StandardDistribuction> list)
        {
            Document document = Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;

            List<StandardDistribuction> result = new List<StandardDistribuction>();

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                List<Polyline> listOfPolys = new List<Polyline>();
                foreach (StandardDistribuction bar in list)
                {
                    Polyline barPolyline = (Polyline)transaction.GetObject(bar.DrawingID, OpenMode.ForRead);
                    listOfPolys.Add(barPolyline);
                }
                //lista de listas -> cada lista possui uma lista de ferros iguais
                List<List<StandardDistribuction>> sepList = new List<List<StandardDistribuction>>();
                // index ja incluido na lista de listas
                List<int> includeds = new List<int>();
                //Loop de comparação
                for (int i = 0; i < listOfPolys.Count; i++)
                {
                    Polyline firstLine = listOfPolys[i];
                    sepList.Add(new List<StandardDistribuction>());

                    if (!includeds.Contains(i)) { sepList[i].Add(list[i]); }

                    for (int j = i + 1; j < listOfPolys.Count; j++)
                    {
                        Polyline secondLine = listOfPolys[j];

                        if (EqualDimensions(firstLine, secondLine) && list[i].Gauge == list[j].Gauge)
                            if (!includeds.Contains(i))
                            {
                                sepList[i].Add(list[j]);
                                includeds.Add(j);
                            }
                    }
                }
                int current_id = 1;
                foreach (List<StandardDistribuction> onelist in sepList)
                {
                    bool canpass = false;
                    foreach (StandardDistribuction item in onelist)
                    {
                        StandardDistribuction newbar = item;
                        newbar.Id = current_id;
                        result.Add(newbar);
                        canpass = true;
                    }
                    if (canpass) current_id++;
                }
            }

            return result;
        }
        public static bool EqualDimensions(Polyline firstLine, Polyline secondLine)
        {
            List<double> firstDimensions = GetPolylineDimensions(firstLine);
            List<double> secondDimensions = GetPolylineDimensions(secondLine);
            bool result = false;

            if (firstDimensions.Count == secondDimensions.Count)
            {
                int secondNumberSegments = secondDimensions.Count;
                bool inequalityFound1 = false;
                bool inequalityFound2 = false;

                for (int i = 0; i < secondNumberSegments; i++)
                    if (firstDimensions[i] != secondDimensions[i])
                        inequalityFound1 = true;

                secondDimensions.Reverse();
                for (int i = 0; i < secondNumberSegments; i++)
                    if (firstDimensions[i] != secondDimensions[i])
                        inequalityFound2 = true;

                if (!inequalityFound1 || !inequalityFound2) { result = true; }
            }
            return result;
        }
        private static List<double> GetPolylineDimensions(Polyline polyline)
        {
            List<double> result = new List<double>();
            int seg_num = polyline.NumberOfVertices - 1;
            for (int i = 0; i < seg_num; i++)
                result.Add(Math.Round(polyline.GetLineSegment2dAt(i).Length,2));
            return result;
        }
        public static void AddDimension(string dimStyleName, Point3d startPoint, Point3d endPoint, double xPadding, double yPadding, string textContent)
        {
            Document document = Application.DocumentManager.MdiActiveDocument;
            Editor editor = document.Editor;
            Database database = document.Database;
            DocumentLock documentLock = document.LockDocument();
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
                BlockTableRecord blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                //Configurando os estilos de cota (dimensionStyle)
                DimStyleTable dimensionStyleTable = (DimStyleTable)transaction.GetObject(database.DimStyleTableId, OpenMode.ForWrite);
                DimStyleTableRecord dstr = new DimStyleTableRecord();
                ObjectId dimStyleId = ObjectId.Null;

                if (dimensionStyleTable.Has(dimStyleName))
                    dimStyleId = dimensionStyleTable[dimStyleName];
                else
                    editor.WriteMessage("Dimension Styles não configuradas.");
                //Setando o texto
                if (textContent == null) textContent = Convert.ToString(Math.Round(Structures.Utilities.DrawingShapes.Distance(startPoint, endPoint), 2));

                //Criando a cota, propriamente dita.
                AlignedDimension cota = new AlignedDimension(
                    startPoint,
                    endPoint,
                    new Point3d((startPoint.X + endPoint.X) / 2 + xPadding, (startPoint.Y + endPoint.Y) / 2 + yPadding, 0),
                    textContent,
                    dimStyleId
                );
                cota.Layer = "1";
                //Colocando-a no desenho
                blockTableRecord.AppendEntity(cota);
                transaction.AddNewlyCreatedDBObject(cota, true);
                transaction.Commit();
                documentLock.Dispose();
            }
        }

    }
}
