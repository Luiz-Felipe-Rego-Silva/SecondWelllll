﻿using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Detailing;

namespace StructCS.Creator.Models
{
    class Quantitative
    {
        public Quantitative(List<StandardDistribuction> list, int multiplier, string title)
        {
            barsList = CleanList(list);
            this.multiplier = multiplier;
        }
        public List<StandardDistribuction> barsList { get; set; }
        public int multiplier { get; set; }
        private double massCA50 = 0.0;
        private double massCA60 = 0.0;
        private List<StandardDistribuction> CleanList(List<StandardDistribuction> dirtyList)
        {
            int numOfSelected = dirtyList.Count;
            List<StandardDistribuction> result = new List<StandardDistribuction>();

            for (int i = 0; i < numOfSelected + 1; i++)
            {
                bool found = false;
                foreach (StandardDistribuction bar in dirtyList)
                {
                    if (bar.Id == i)
                    {
                        if (!found)
                        {
                            found = true;
                            result.Add(bar);
                        }
                        else
                        {
                            if (!bar.IsVariable)
                                result[i - 1].Quantity += bar.Quantity;
                            else
                            {
                                result[i - 1].Quantity += bar.Quantity;
                                (result[i - 1] as VariableDistribuction).UpdateLengthTable((bar as VariableDistribuction).LenghtOfLines, (bar as VariableDistribuction).constantParts);
                            }
                        }
                    }
                }
            }
            return result;
        }
        public double CreateBarsTable(Point3d position)
        {
            List<StandardDistribuction> list = barsList;

            Table barSumaryTable = new Table
            {
                TableStyle = Application.DocumentManager.MdiActiveDocument.Database.Tablestyle
            };
            barSumaryTable.SetSize(list.Count + 2, 5);
            barSumaryTable.SetRowHeight(TablesConfigs.RowHeight);
            barSumaryTable.SetColumnWidth(TablesConfigs.ColumnWidth);
            barSumaryTable.Layer = "3";
            barSumaryTable.Position = position;

            if (multiplier > 1)
                barSumaryTable.Cells[0, 0].TextString = $"Quadro de Aço (x{multiplier})";
            else
                barSumaryTable.Cells[0, 0].TextString = "Quadro de Aço";

            barSumaryTable.Cells[1, 0].TextString = "N";
            barSumaryTable.Cells[1, 1].TextString = "φ (mm)";
            barSumaryTable.Cells[1, 2].TextString = "Quant.";
            barSumaryTable.Cells[1, 3].TextString = "C. unit.(m)";
            barSumaryTable.Cells[1, 4].TextString = "C. total(m)";

            foreach (StandardDistribuction rebar in list)
            {
                string lengthText = (Math.Round(rebar.Length + rebar.AmendmentLength * rebar.NumberOfAmendments) / 100.0).ToString("F2");
                if (rebar.IsVariable) lengthText = "VAR";

                int index = list.IndexOf(rebar) + 2;
                barSumaryTable.Cells[index, 0].TextString = Convert.ToString(rebar.Id);
                barSumaryTable.Cells[index, 1].TextString = Convert.ToString(rebar.Gauge);
                barSumaryTable.Cells[index, 2].TextString = Convert.ToString(rebar.Quantity * multiplier); // x multi
                barSumaryTable.Cells[index, 3].TextString = lengthText;
                barSumaryTable.Cells[index, 4].TextString = (multiplier * (Math.Round(rebar.GetTotalLength()) / 100.0)).ToString("F2");
            }

            barSumaryTable.GenerateLayout();
            var height = barSumaryTable.Height;
            Tools.DrawByHandle(barSumaryTable);
            barSumaryTable.Dispose();

            return height;
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
                    StandardDistribuction bar = new StandardDistribuction
                    {
                        Gauge = rebar.Gauge,
                        Length = (Math.Round(rebar.Length)) * rebar.Quantity
                    };
                    packs.Add(bar);
                }
                else { packs[foundIndex].Length += rebar.Quantity * (Math.Round(rebar.Length)); }
            }
            return packs;
        }
        public double CreateGaugesTable(Point3d position)
        {
            //List<SteelBar> barsList = GetMappedGauges();
            barsList = barsList.OrderBy(bar => bar.Gauge).ToList();
            List<double[]> gaugeMatriz = GaugeMatriz();

            Table gaugeSumaryTable = new Table();
            gaugeSumaryTable.TableStyle = Application.DocumentManager.MdiActiveDocument.Database.Tablestyle;
            gaugeSumaryTable.SetSize(gaugeMatriz.Count + 2, 4);
            gaugeSumaryTable.SetRowHeight(TablesConfigs.RowHeight);
            gaugeSumaryTable.SetColumnWidth(TablesConfigs.ColumnWidth * 5 / 4);
            gaugeSumaryTable.Layer = "3";

            gaugeSumaryTable.Position = position;

            gaugeSumaryTable.Cells[0, 0].TextString = "Resumo";
            gaugeSumaryTable.Cells[1, 0].TextString = "φ (mm)";
            gaugeSumaryTable.Cells[1, 1].TextString = "Tipo";
            gaugeSumaryTable.Cells[1, 2].TextString = "C. total(m)";
            gaugeSumaryTable.Cells[1, 3].TextString = "Peso(kg)";

            int index = 2;
            foreach (double[] value in gaugeMatriz)
            {
                gaugeSumaryTable.Cells[index, 0].TextString = Convert.ToString(gaugeMatriz[index - 2][0]);

                if (gaugeMatriz[index - 2][0] < 6.0)
                    gaugeSumaryTable.Cells[index, 1].TextString = Convert.ToString("CA - 60");
                else
                    gaugeSumaryTable.Cells[index, 1].TextString = Convert.ToString("CA - 50");


                gaugeSumaryTable.Cells[index, 2].TextString = (multiplier * ((Math.Round(gaugeMatriz[index - 2][1])) / 100.0)).ToString("F2"); // x multi
                gaugeSumaryTable.Cells[index, 3].TextString = (multiplier * gaugeMatriz[index - 2][2]).ToString("F1"); // x multi

                if (gaugeMatriz[index - 2][0] < 6)
                    massCA60 += Math.Round(multiplier * gaugeMatriz[index - 2][2], 1);
                else
                    massCA50 += Math.Round(multiplier * gaugeMatriz[index - 2][2], 1);

                index++;
            }

            gaugeSumaryTable.GenerateLayout();
            Tools.DrawByHandle(gaugeSumaryTable);
            double height = gaugeSumaryTable.Height;
            gaugeSumaryTable.Dispose();

            return height;
        }
        private List<double[]> GaugeMatriz()
        {
            List<double[]> gaugeTable = new List<double[]>();
            double nowGauge = 0.0;
            int nowLine = -1;

            for (int index = 0; index < barsList.Count; index++)
            {
                if (barsList[index].Gauge == nowGauge)
                {
                    gaugeTable[nowLine][1] += barsList[index].GetTotalLength();
                    gaugeTable[nowLine][2] += barsList[index].GetMass();
                }
                else
                {
                    nowLine += 1;
                    nowGauge = barsList[index].Gauge;
                    gaugeTable.Add(new double[3] { barsList[index].Gauge, barsList[index].GetTotalLength(), barsList[index].GetMass() });
                }
            }
            return gaugeTable;
        }

        public void GenerateFullTable(Point3d position)
        {
            var h = CreateBarsTable(position);
            h += CreateGaugesTable(new Point3d(position.X, position.Y - h, 0));
            GenerateBriefTable(new Point3d(position.X, position.Y - h, 0));
        }

        public double GenerateBriefTable(Point3d position)
        {
            double mass_50 = massCA50;
            double mass_60 = massCA60;

            int NumTypes = 0;

            if (mass_50 > 0) NumTypes++;
            if (mass_60 > 0) NumTypes++;

            Table totalTable = new Table { TableStyle = Application.DocumentManager.MdiActiveDocument.Database.Tablestyle };
            totalTable.SetSize(1 + NumTypes, 2);
            totalTable.SetRowHeight(TablesConfigs.RowHeight);
            totalTable.SetColumnWidth(TablesConfigs.ColumnWidth * 5 / 2);
            totalTable.Layer = "3";
            totalTable.Cells[0, 0].TextString = "Total (Kg)";

            int putted = 1;

            if (mass_50 > 0 && NumTypes > 0)
            {
                totalTable.Cells[putted, 0].TextString = "CA-50";
                totalTable.Cells[putted, 1].TextString = massCA50.ToString("F1");
                putted++;
                NumTypes--;
            }

            if (mass_60 > 0 && NumTypes > 0)
            {
                totalTable.Cells[putted, 0].TextString = "CA-60";
                totalTable.Cells[putted, 1].TextString = massCA60.ToString("F1");
                putted++;
                NumTypes--;
            }
            totalTable.Position = position;
            totalTable.GenerateLayout();
            Tools.DrawByHandle(totalTable);
            var height = totalTable.Height;
            totalTable.Dispose();
            return height;
        }
    }
}