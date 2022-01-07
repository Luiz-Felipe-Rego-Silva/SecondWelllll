using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Model.Detailing.Entities.SteelBars;
using Structures.Utilities;
using System;
using System.Collections.Generic;

namespace Model.Detailing.Entities.Tables
{
    class VarTable
    {
        private Table varTable;
        private readonly double ConstantPart;
        private readonly int Id;
        private double[] VARLength;
        private readonly int _amendmentLength;
        private bool hasAmendment = false;
        private const int RowHeight = 30;
        public VarTable(double[] VARLength, int barId, int constantPart, int amendmentLength)
        {
            _amendmentLength = amendmentLength;
            this.VARLength = VARLength;
            Id = barId;
            ConstantPart = constantPart;
        }
        public void GenerateTable(Point3d position)
        {
            DoStructureOfTable();
            SetCollumns();
            FillTable();
            DrawTable(position);
            varTable.Dispose();
        }
        private void DoStructureOfTable()
        {
            int numberOfCollumns = 3;
            varTable = new Table
            {
                TableStyle = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database.Tablestyle
            };
            if (HasAmendment())
                numberOfCollumns++;
            varTable.SetSize(VARLength.Length + 2, numberOfCollumns);
            varTable.SetRowHeight(RowHeight);
            varTable.Columns[0].Width = 60.0;
            varTable.Columns[1].Width = 60.0;
            varTable.Columns[2].Width = 100.0;
            if (hasAmendment)
                varTable.Columns[3].Width = 120.0;
            varTable.Layer = "3";
        }
        private void SetCollumns()
        {
            varTable.Cells[0, 0].TextString = "Tabela de Comprimentos Variáveis";
            varTable.Cells[1, 0].TextString = "N";
            varTable.Cells[1, 1].TextString = "VAR (cm)";
            varTable.Cells[1, 2].TextString = "Comprimento (m)";
            if (hasAmendment)
                varTable.Cells[1, 3].TextString = "Emendas";
        }
        private void FillTable()
        {
            for (int index = 2; index < VARLength.Length + 2; index++)
            {
                int lineIndex = index - 2;
                varTable.Cells[index, 0].TextString = $"{Id}.{lineIndex + 1}";

                int numberOfAmendments = Amendments.GetNumberOfAmendments(VARLength[lineIndex] + ConstantPart, _amendmentLength);
                varTable.Cells[index, 1].TextString = Math.Round(VARLength[lineIndex], 0).ToString();
                varTable.Cells[index, 2].TextString = (Math.Round((VARLength[lineIndex] + ConstantPart + numberOfAmendments * _amendmentLength) / 100.0, 2)).ToString("F2");
                if (hasAmendment)
                {
                    if (numberOfAmendments * _amendmentLength > 0)
                    {
                        varTable.Cells[index, 3].TextString += $" {_amendmentLength}cm (x{numberOfAmendments})";
                    }
                    else
                    {
                        varTable.Cells[index, 3].TextString += " - ";
                    }
                }
            }
        }
        private bool HasAmendment()
        {
            for (int index = 0; index < VARLength.Length; index++)
            {
                int numberOfAmendments = Amendments.GetNumberOfAmendments(VARLength[index] + ConstantPart, _amendmentLength);
                if (numberOfAmendments * _amendmentLength > 0) { hasAmendment = true; return true; }
            }
            return false;
        }
        private void DrawTable(Point3d position)
        {
            varTable.Position = position;
            varTable.GenerateLayout();
            DrawingUtilities.AddToDrawing(varTable);
        }
        public static void PrintGenericComents(Point3d basePoint, bool hasAmendment)
        {
            DBText commentsObs = new DBText
            {
                Position = basePoint,
                Height = 10.0,
                Layer = "3"
            };
            string contentObs = "Obs.:";
            commentsObs.TextString = contentObs;
            DrawingUtilities.AddToDrawing(commentsObs);


            DBText comments_1 = new DBText
            {
                Position = new Point3d(basePoint.X, basePoint.Y - 17.0, 0),
                Height = 10.0,
                Layer = "3"
            };
            string content_1 = "1. Os valores da coluna VAR indicam o comprimento do trecho variável.";
            comments_1.TextString = content_1;
            DrawingUtilities.AddToDrawing(comments_1);

            DBText comments_2 = new DBText
            {
                Position = new Point3d(basePoint.X, basePoint.Y - 2 * 17.0, 0),
                Height = 10.0,
                Layer = "3"
            };
            string content_2 = "2. Os valores da coluna Comprimento indicam o comprimento total da barra, já contabilizadas as dobras" + (hasAmendment ? " e emendas." : ".");
            comments_2.TextString = content_2;
            DrawingUtilities.AddToDrawing(comments_2);

            DBText comments_3 = new DBText
            {
                Position = new Point3d(basePoint.X, basePoint.Y - 3 * 17.0, 0),
                Height = 10.0,
                Layer = "3"
            };
            string content_3 = "3. Para quantificação no Quadro de Aço, o valor de VAR equivale à média dos comprimentos.";
            comments_3.TextString = content_3;
            DrawingUtilities.AddToDrawing(comments_3);

            if (hasAmendment)
            {
                DBText comments_4 = new DBText
                {
                    Position = new Point3d(basePoint.X, basePoint.Y - 4 * 17.0, 0),
                    Height = 10.0,
                    Layer = "3"
                };
                string content_4 = "4. Emendar evitando o congestionamento de armaduras.";
                comments_4.TextString = content_4;
                DrawingUtilities.AddToDrawing(comments_4);
            }
        }
    }
}
