using Detailing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello_World.Detailing
{
    class SteelTable
    {
        public SteelTable(List<StandardDistribuction> rebars, int multiplier)
        {
            barsList = CleanList(rebars);
            this.multiplier = multiplier;
        }

        public List<StandardDistribuction> barsList { get; set; }
        public int multiplier { get; set; }
        private double massCA50 = 0.0;
        private double massCA60 = 0.0;
        private List<SteelBar> CleanList(List<SteelBar> dirtyList)
        {
            var numOfSelected = dirtyList.Count;
            var result = new List<SteelBar>();


            for (int i = 0; i < numOfSelected + 1; i++)
            {
                bool found = false;

                foreach (SteelBar bar in dirtyList)
                    if (bar.Id == i)
                    {
                        if (!found)
                        {
                            found = true;
                            result.Add(bar);
                        }
                        else
                        {
                            result[i - 1].Quantity += bar.Quantity;
                        }
                    }
            }

            return result;
        }

        public double CreateBarsTable(Point3d position)
        {
            var list = barsList;

            var tb = new Table
            {
                TableStyle = Application.DocumentManager.MdiActiveDocument.Database.Tablestyle
            };
            tb.SetSize(list.Count + 2, 5);
            tb.SetRowHeight(TablesConfigs.RowHeight);
            tb.SetColumnWidth(TablesConfigs.ColumnWidth);
            tb.Layer = "3";

            tb.Position = position;
            if (multiplier > 1)
                tb.Cells[0, 0].TextString = $"Quadro de Aço (x{multiplier})";
            else
                tb.Cells[0, 0].TextString = "Quadro de Aço";

            tb.Cells[1, 0].TextString = "N";
            tb.Cells[1, 1].TextString = "φ (mm)";
            tb.Cells[1, 2].TextString = "Quant.";
            tb.Cells[1, 3].TextString = "C. unit.(m)";
            tb.Cells[1, 4].TextString = "C. total(m)";

            foreach (var rebar in list)
            {
                var lengthText = (Math.Round(rebar.Length) / 100.0).ToString("F2");
                if (rebar.IsVariable) lengthText = "VAR.";

                var index = list.IndexOf(rebar) + 2;
                tb.Cells[index, 0].TextString = Convert.ToString(rebar.Id);
                tb.Cells[index, 1].TextString = Convert.ToString(rebar.Gauge);
                tb.Cells[index, 2].TextString = Convert.ToString(rebar.Quantity * multiplier); // x multi
                tb.Cells[index, 3].TextString = lengthText;
                tb.Cells[index, 4].TextString = (rebar.Quantity * multiplier * (Math.Round(rebar.Length) / 100.0)).ToString("F2");
            }

            tb.GenerateLayout();
            var height = tb.Height;
            Tools.AddToDrawing(tb);
            tb.Dispose();

            return height;
        }

        private double CalculateMass(SteelBar bar)
        {
            //todas as medidas em CM -> resposta em Kg
            double mass = (Math.Round(bar.Length) / 100.0) * GetNominalSteelDensity(bar.Gauge);
            return Math.Round(mass, 1);
        }
        public double GetNominalSteelDensity(double gauge)
        {
            int gaugeForswitch = (int)(10 * gauge);
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

        private List<SteelBar> GetMappedGauges()
        {
            var packs = new List<SteelBar>();

            foreach (var rebar in barsList)
            {
                var foundIndex = -1;
                foreach (var pack in packs)
                    if (pack.Gauge == rebar.Gauge)
                        foundIndex = packs.IndexOf(pack);

                if (foundIndex == -1)
                {
                    var st = new SteelBar
                    {
                        Gauge = rebar.Gauge,
                        Length = (Math.Round(rebar.Length)) * rebar.Quantity
                    };
                    packs.Add(st);
                }
                else
                {
                    packs[foundIndex].Length += rebar.Quantity * (Math.Round(rebar.Length));
                }
            }

            return packs;
        }

        public double CreateGaugesTable(Point3d position)
        {
            var list = GetMappedGauges();

            var tb = new Table();
            tb.TableStyle = Application.DocumentManager.MdiActiveDocument.Database.Tablestyle;
            tb.SetSize(list.Count + 2, 4);
            tb.SetRowHeight(TablesConfigs.RowHeight);
            tb.SetColumnWidth(TablesConfigs.ColumnWidth * 5 / 4);
            tb.Layer = "3";

            tb.Position = position;

            tb.Cells[0, 0].TextString = "Resumo";
            tb.Cells[1, 0].TextString = "φ (mm)";
            tb.Cells[1, 1].TextString = "Tipo";
            tb.Cells[1, 2].TextString = "C. total(m)";
            tb.Cells[1, 3].TextString = "Peso(kg)";


            foreach (var rebar in list)
            {
                var index = list.IndexOf(rebar) + 2;
                tb.Cells[index, 0].TextString = Convert.ToString(rebar.Gauge);

                if (rebar.Gauge > 5.0)
                    tb.Cells[index, 1].TextString = Convert.ToString("CA - 50");
                else
                    tb.Cells[index, 1].TextString = Convert.ToString("CA - 60");


                tb.Cells[index, 2].TextString = (multiplier * ((Math.Round(rebar.Length)) / 100.0)).ToString("F2"); // x multi
                tb.Cells[index, 3].TextString = (multiplier * CalculateMass(rebar)).ToString("F1"); // x multi

                if (rebar.Gauge < 6)
                    massCA60 += Math.Round(multiplier * CalculateMass(rebar), 1);
                else
                    massCA50 += Math.Round(multiplier * CalculateMass(rebar), 1);
            }

            tb.GenerateLayout();
            Tools.AddToDrawing(tb);
            var height = tb.Height;
            tb.Dispose();

            return height;
        }

        public void GenerateFullTable(Point3d position)
        {
            var h = CreateBarsTable(position);
            h += CreateGaugesTable(new Point3d(position.X, position.Y - h, 0));
            GenerateBriefTable(new Point3d(position.X, position.Y - h, 0));
        }

        public double GenerateBriefTable(Point3d position)
        {
            double mass_50 = 0;
            double mass_60 = 0;

            foreach (var bar in barsList)
                if (bar.Gauge < 6.0)
                    mass_60 += CalculateMass(bar) * bar.Quantity;
                else
                    mass_50 += CalculateMass(bar) * bar.Quantity;
            var NumTypes = 0;

            if (mass_50 > 0) NumTypes++;

            if (mass_60 > 0) NumTypes++;

            var tb = new Table
            {
                TableStyle = Application.DocumentManager.MdiActiveDocument
                    .Database.Tablestyle
            };
            tb.SetSize(1 + NumTypes, 2);
            tb.SetRowHeight(TablesConfigs.RowHeight);
            tb.SetColumnWidth(TablesConfigs.ColumnWidth * 5 / 2);
            tb.Layer = "3";

            tb.Cells[0, 0].TextString = "Total (Kg)";

            var putted = 1;

            if (mass_50 > 0 && NumTypes > 0)
            {
                tb.Cells[putted, 0].TextString = "CA-50";
                tb.Cells[putted, 1].TextString = massCA50.ToString("F1");
                putted++;
                NumTypes--;
            }

            if (mass_60 > 0 && NumTypes > 0)
            {
                tb.Cells[putted, 0].TextString = "CA-60";
                tb.Cells[putted, 1].TextString = massCA60.ToString("F1");
                putted++;
                NumTypes--;
            }

            tb.Position = position;
            tb.GenerateLayout();
            Tools.AddToDrawing(tb);
            var height = tb.Height;
            tb.Dispose();
            return height;
        }

    }
}
