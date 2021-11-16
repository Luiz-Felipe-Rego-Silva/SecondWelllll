using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Detailing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detailing
{
    class SteelTable
    {
        public List<StandardDistribuction> barsList { get; set; }
        public int multiplier { get; set; }
        private double massCA50 = 0.0;
        private double massCA60 = 0.0;
        private const double ROW_HEIGTH = 30.0;
        private const double COLUMN_WIDTH = 90.0;
        private string Title;
        public SteelTable(List<StandardDistribuction> rebars, int multiplier, string title)
        {
            barsList = CleanList(rebars);
            this.multiplier = multiplier;
            Title = title;
        }
        private List<StandardDistribuction> CleanList(List<StandardDistribuction> dirtyList)
        {
            int numOfSelected = dirtyList.Count;
            List<StandardDistribuction> result = new List<StandardDistribuction>();
            for (int i = 0; i < numOfSelected + 1; i++)
            {
                bool found = false;
                foreach (StandardDistribuction bar in dirtyList)
                    if (bar.Id == i)
                    {
                        if (!found)
                        {
                            found = true;
                            result.Add(bar);
                        }
                        else { result[i-1].Quantity += bar.Quantity; }
                    }
            }
            return result;
        }
        private double CreateBarsTable(Point3d position)
        {
            List<StandardDistribuction> list = barsList;
            Table lengthTable = new Table { TableStyle = Application.DocumentManager.MdiActiveDocument.Database.Tablestyle};
            lengthTable.SetSize(list.Count + 2, 5);
            lengthTable.SetRowHeight(ROW_HEIGTH);
            lengthTable.SetColumnWidth(COLUMN_WIDTH);
            lengthTable.Layer = "3";
            lengthTable.Position = position;

            if (multiplier > 1)
                lengthTable.Cells[0, 0].TextString = $"Quadro de Aço - {Title.ToUpper()} (x{multiplier})";
            else
                lengthTable.Cells[0, 0].TextString = $"Quadro de Aço - {Title.ToUpper()}";

            lengthTable.Cells[1, 0].TextString = "N";
            lengthTable.Cells[1, 1].TextString = "φ (mm)";
            lengthTable.Cells[1, 2].TextString = "Quant.";
            lengthTable.Cells[1, 3].TextString = "C. unit.(m)";
            lengthTable.Cells[1, 4].TextString = "C. total(m)";

            foreach (StandardDistribuction rebar in list)
            {
                int index = list.IndexOf(rebar) + 2;
                lengthTable.Cells[index, 0].TextString = Convert.ToString(rebar.Id);
                lengthTable.Cells[index, 1].TextString = Convert.ToString(rebar.Gauge * 10);
                lengthTable.Cells[index, 2].TextString = Convert.ToString(rebar.Quantity * multiplier); // x multi
                lengthTable.Cells[index, 3].TextString = (Math.Round(rebar.Length) / 100.0).ToString("F2");
                lengthTable.Cells[index, 4].TextString = (rebar.Quantity * multiplier * (Math.Round(rebar.Length) / 100.0)).ToString("F2");
            }
            lengthTable.GenerateLayout();
            double height = lengthTable.Height;
            Structures.Utilities.DrawingUtilities.AddToDrawing(lengthTable);
            lengthTable.Dispose();
            return height;
        }
        private double CalculateMass(StandardDistribuction bar)
        {
            double mass = (Math.Round(bar.Length) / 100.0) * GetNominalSteelDensity(bar.Gauge);
            return Math.Round(mass, 1);
        }
        private double GetNominalSteelDensity(double gauge)
        {
            int gaugeForswitch = (int)(100 * gauge);
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
        private List<StandardDistribuction> GetMappedGauges()
        {
            List<StandardDistribuction> packs = new List<StandardDistribuction>();
            foreach (StandardDistribuction rebar in barsList)
            {
                int foundIndex = -1;
                foreach (StandardDistribuction pack in packs)
                    if (pack.Gauge == rebar.Gauge)
                        foundIndex = packs.IndexOf(pack);

                if (foundIndex == -1)
                {
                    StandardDistribuction st = new StandardDistribuction(rebar.Gauge, (Math.Round(rebar.Length)) * rebar.Quantity);
                    packs.Add(st);
                }
                else
                {
                    packs[foundIndex].Length += rebar.Quantity * (Math.Round(rebar.Length));
                }
            }

            return packs;
        }
        private double CreateGaugesTable(Point3d position)
        {
            List<StandardDistribuction> list = GetMappedGauges();
            list = list.OrderBy(bar => bar.Gauge).ToList();

            Table gaugeSumaryTable = new Table();
            gaugeSumaryTable.TableStyle = Application.DocumentManager.MdiActiveDocument.Database.Tablestyle;
            gaugeSumaryTable.SetSize(list.Count + 2, 4);
            gaugeSumaryTable.SetRowHeight(ROW_HEIGTH);
            gaugeSumaryTable.SetColumnWidth(COLUMN_WIDTH * 5 / 4);
            gaugeSumaryTable.Layer = "3";

            gaugeSumaryTable.Position = position;

            gaugeSumaryTable.Cells[0, 0].TextString = "Resumo";
            gaugeSumaryTable.Cells[1, 0].TextString = "φ (mm)";
            gaugeSumaryTable.Cells[1, 1].TextString = "Tipo";
            gaugeSumaryTable.Cells[1, 2].TextString = "C. total(m)";
            gaugeSumaryTable.Cells[1, 3].TextString = "Peso(kg)";

            foreach (StandardDistribuction rebar in list)
            {
                int index = list.IndexOf(rebar) + 2;
                gaugeSumaryTable.Cells[index, 0].TextString = Convert.ToString(rebar.Gauge * 10.0);

                if (rebar.Gauge * 10.0 > 5.0)
                    gaugeSumaryTable.Cells[index, 1].TextString = Convert.ToString("CA - 50");
                else
                    gaugeSumaryTable.Cells[index, 1].TextString = Convert.ToString("CA - 60");

                gaugeSumaryTable.Cells[index, 2].TextString = (multiplier * ((Math.Round(rebar.Length)) / 100.0)).ToString("F2"); // x multi
                gaugeSumaryTable.Cells[index, 3].TextString = (multiplier * CalculateMass(rebar)).ToString("F1"); // x multi

                if (rebar.Gauge * 10.0 < 6)
                    massCA60 += Math.Round(multiplier * CalculateMass(rebar), 1);
                else
                    massCA50 += Math.Round(multiplier * CalculateMass(rebar), 1);
            }

            gaugeSumaryTable.GenerateLayout();
            Structures.Utilities.DrawingUtilities.AddToDrawing(gaugeSumaryTable);
            double height = gaugeSumaryTable.Height;
            gaugeSumaryTable.Dispose();

            return height;
        }
        public void GenerateFullTable(Point3d position)
        {
            double h = CreateBarsTable(position);
            h += CreateGaugesTable(new Point3d(position.X, position.Y - h, 0));
            GenerateBriefTable(new Point3d(position.X, position.Y - h, 0));
        }
        private double GenerateBriefTable(Point3d position)
        {
            double mass_50 = 0;
            double mass_60 = 0;

            foreach (StandardDistribuction bar in barsList)
                if (bar.Gauge * 10.0 < 6.0)
                    mass_60 += CalculateMass(bar) * bar.Quantity;
                else
                    mass_50 += CalculateMass(bar) * bar.Quantity;

            int NumTypes = 0;
            if (mass_50 > 0) NumTypes++;
            if (mass_60 > 0) NumTypes++;

            Table totalsTable = new Table { TableStyle = Application.DocumentManager.MdiActiveDocument.Database.Tablestyle};
            totalsTable.SetSize(1 + NumTypes, 2);
            totalsTable.SetRowHeight(ROW_HEIGTH);
            totalsTable.SetColumnWidth(COLUMN_WIDTH * 5 / 2);
            totalsTable.Layer = "3";

            totalsTable.Cells[0, 0].TextString = "Total (Kg)";
            int putted = 1;
            if (mass_50 > 0 && NumTypes > 0) 
            {
                totalsTable.Cells[putted, 0].TextString = "CA-50";
                totalsTable.Cells[putted, 1].TextString = massCA50.ToString("F1");
                putted++;
                NumTypes--;
            }
            if (mass_60 > 0 && NumTypes > 0)
            {
                totalsTable.Cells[putted, 0].TextString = "CA-60";
                totalsTable.Cells[putted, 1].TextString = massCA60.ToString("F1");
            }

            totalsTable.Position = position;
            totalsTable.GenerateLayout();
            Structures.Utilities.DrawingUtilities.AddToDrawing(totalsTable);
            double height = totalsTable.Height;
            totalsTable.Dispose();
            return height;
        }

    }
}
