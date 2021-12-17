using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Structures.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detailing
{
    class VarTable
    {
        private Table varTable;
        private readonly double ConstantPart;
        private readonly int Id;
        private List<Line> lines;
        private double[] VARLength;
        private int _amendmentLength;
        private bool hasAmendment = false;
        private const int RowHeight = 30;
        private const int CollumnWidth = 90;
        public VarTable(List<Line> lines, int barId, int constantPart, int amendmentLength)
        {
            this.lines = lines;
            Id = barId;
            ConstantPart = constantPart;
            _amendmentLength = amendmentLength;
            VARLength = new double[lines.Count];
            for (int index = 0; index < lines.Count; index++) { VARLength[index] = Math.Floor(lines[index].Length); }
        }
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
            Point3d textPosition = new Point3d(position.X, position.Y - 20.0 - varTable.Height, 0);
            PrintComments(textPosition);

            varTable.Dispose();
        }
        private void DoStructureOfTable()
        {
            varTable = new Table();
            varTable.TableStyle = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database.Tablestyle;
            varTable.SetSize(VARLength.Length + 2, 3);
            varTable.SetRowHeight(RowHeight);
            varTable.SetColumnWidth(120.0);
            varTable.Layer = "3";
        }
        private void SetCollumns()
        {
            varTable.Cells[0, 0].TextString = "Tabela de Comprimentos Variáveis";
            varTable.Cells[1, 0].TextString = "N";
            varTable.Cells[1, 1].TextString = "VAR (cm)";
            varTable.Cells[1, 2].TextString = "Comprimento (m)";
        }
        private void FillTable()
        {
            for (int index = 2; index < VARLength.Length + 2; index++)
            {
                int lineIndex = index - 2;
                varTable.Cells[index, 0].TextString = $"{Id}.{lineIndex + 1}";

                int numberOfAmendments = Amendments.getNumberOfAmendments(VARLength[lineIndex] + ConstantPart, _amendmentLength);
                varTable.Cells[index, 1].TextString = Math.Round(VARLength[lineIndex], 0).ToString();
                varTable.Cells[index, 2].TextString = (Math.Round((VARLength[lineIndex] + ConstantPart + numberOfAmendments * _amendmentLength) / 100.0, 2)).ToString("F2");
                if (numberOfAmendments * _amendmentLength > 0) { varTable.Cells[index, 2].TextString += $" | *{numberOfAmendments}"; hasAmendment = true; }
            }
        }
        private void DrawTable(Point3d position)
        {
            varTable.Position = position;
            varTable.GenerateLayout();
            DrawingUtilities.AddToDrawing(varTable);
        }
        public void PrintComments(Point3d basePoint)
        {
            if (hasAmendment)
            {
                DBText comments_5 = new DBText();
                comments_5.Position = new Point3d(basePoint.X, basePoint.Y, 0);
                comments_5.Height = 10.0;
                comments_5.Layer = "3";
                string content_5 = "EMENDAS DE " + _amendmentLength + " CM.";
                comments_5.TextString = content_5;
                DrawingUtilities.AddToDrawing(comments_5);
            }
        }
        public static void PrintGenericComents(Point3d basePoint, bool hasAmendment)
        {
            DBText commentsObs = new DBText();
            commentsObs.Position = basePoint;
            commentsObs.Height = 10.0;
            commentsObs.Layer = "3";
            string contentObs = "Obs.:";
            commentsObs.TextString = contentObs;
            DrawingUtilities.AddToDrawing(commentsObs);


            DBText comments_1 = new DBText();
            comments_1.Position = new Point3d(basePoint.X, basePoint.Y - 17.0, 0);
            comments_1.Height = 10.0;
            comments_1.Layer = "3";
            string content_1 = "1. Os valores da coluna VAR indicam o comprimento do trecho variável.";
            comments_1.TextString = content_1;
            DrawingUtilities.AddToDrawing(comments_1);

            DBText comments_2 = new DBText();
            comments_2.Position = new Point3d(basePoint.X, basePoint.Y - 2 * 17.0, 0);
            comments_2.Height = 10.0;
            comments_2.Layer = "3";
            string content_2 = "2. Os valores da coluna Comprimento indicam o comprimento total da barra, já contabilizadas as dobras" + (hasAmendment ? " e emendas." : ".");
            comments_2.TextString = content_2;
            DrawingUtilities.AddToDrawing(comments_2);

            DBText comments_3 = new DBText();
            comments_3.Position = new Point3d(basePoint.X, basePoint.Y - 3 * 17.0, 0);
            comments_3.Height = 10.0;
            comments_3.Layer = "3";
            string content_3 = "3. Para quantificação no Quadro de Aço, o valor de VAR equivale à média dos comprimentos.";
            comments_3.TextString = content_3;
            DrawingUtilities.AddToDrawing(comments_3);

            if (hasAmendment)
            {
                DBText comments_4 = new DBText();
                comments_4.Position = new Point3d(basePoint.X, basePoint.Y - 4 * 17.0, 0);
                comments_4.Height = 10.0;
                comments_4.Layer = "3";
                string content_4 = "4. Emendar evitando o congestionamento de armaduras.";
                comments_4.TextString = content_4;
                DrawingUtilities.AddToDrawing(comments_4); DBText comments_5 = new DBText();
                comments_5.Position = new Point3d(basePoint.X, basePoint.Y - 5 * 17.0, 0);
                comments_5.Height = 10.0;
                comments_5.Layer = "3";
                string content_5 = "5. *n: 'n' indica a quantidade de emendas no trecho";
                comments_5.TextString = content_5;
                DrawingUtilities.AddToDrawing(comments_5);
            }
        }
    }
}
