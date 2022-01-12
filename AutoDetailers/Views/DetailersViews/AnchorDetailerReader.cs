using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Controllers;
using Autodesk.AutoCAD.DatabaseServices;
using Structures.Utilities;

namespace AutoDetailers.Views.DetailersViews
{
    class AnchorDetailerReader
    {
        private static readonly string SheetName = "Blocos";
        private List< AnchorBlockDetailing> _blocks;

        public void DataReaderController(string Path, AnchorBlockDetailing detailer)
        {


        }
        public void DataReader(string Path) 
        {
            AnchorBlockDetailing detailer = new AnchorBlockDetailing();
            Path = '@' + '"' + Path + '"';
            XLWorkbook workSheets = new XLWorkbook(Path);
            IXLWorksheet planilha = workSheets.Worksheets.First(w => w.Name == SheetName);
            int totalLinhas = planilha.Rows().Count();
            // cabecalho
            detailer.AnchorFactor = Math.Ceiling(double.Parse(planilha.Cell("O2").Value.ToString()));
            for (int l = 3; l <= totalLinhas; l++)
            {
                /*identificacao*/
                int blockNumber = int.Parse(planilha.Cell($"A{l}").Value.ToString());
                detailer.NominalDiameter = Math.Ceiling(double.Parse(planilha.Cell($"B{l}").Value.ToString()));
                
                /* Geometria */
                detailer.SmallLength = Math.Ceiling(double.Parse(planilha.Cell($"C{l}").Value.ToString()));
                detailer.SmallestHeigth = Math.Ceiling(double.Parse(planilha.Cell($"D{l}").Value.ToString()));
                detailer.Width = Math.Ceiling(double.Parse(planilha.Cell($"E{l}").Value.ToString()));

                detailer.BiggestBase = Math.Ceiling(double.Parse(planilha.Cell($"F{l}").Value.ToString()));
                detailer.SmallestBase = Math.Ceiling(double.Parse(planilha.Cell($"G{l}").Value.ToString()));
                detailer.Length = Math.Ceiling(double.Parse(planilha.Cell($"H{l}").Value.ToString()));
                detailer.Heigth = Math.Ceiling(double.Parse(planilha.Cell($"I{l}").Value.ToString()));

                /* Detalhamento */
                detailer.Cover = Math.Ceiling(double.Parse(planilha.Cell($"K{l}").Value.ToString()));
                detailer.Gauge = Math.Ceiling(double.Parse(planilha.Cell($"L{l}").Value.ToString()));
                detailer.Spacing = Math.Ceiling(double.Parse(planilha.Cell($"M{l}").Value.ToString()));
                
                _blocks.Add(detailer);
            }
        }
    }
}
