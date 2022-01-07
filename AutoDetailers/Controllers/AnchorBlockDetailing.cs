using Autodesk.AutoCAD.Geometry;
using Model.Detailing.Entities.SteelBars;
using Structures.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    class AnchorBlockDetailing
    {
        public double NominalDiameter { get; set; }
        public double Base { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double SmallLength { get; set; }
        public double Heigth { get; set; }
        public double SmallestHeigth { get; set; }
        public double ConcreteHeigth { get; set; }
        public double Cover { get; set; }
        public int Multiplier { get; set; }
        public double Spacing { get; set; }
        public double Gauge { get; set; }
        public double AnchorFactor { get; set; } = 34.0;
        private List<StandardDistribuction> distribuctions = new List<StandardDistribuction>();
        public AnchorBlockDetailing(double nominalDiameter, double largestBase, double length,
            double heigth, double smallestHeigth, double cover,
            int multiplier, double spacing, double gauge)
        {
            NominalDiameter = nominalDiameter;
            Base = largestBase;
            Length = length;
            Width = Math.Ceiling(100 * NominalDiameter * 1.5) / 100.0;
            Heigth = heigth;
            SmallestHeigth = smallestHeigth;
            ConcreteHeigth = Math.Ceiling(100.0 * (NominalDiameter * 1.25)) / 100.0;
            Cover = cover;
            Multiplier = multiplier;
            Spacing = spacing;
            Gauge = gauge;
        }
        public void CreateBigDistribuctions(Point3d startPoint)
        {
            double extension1, extension2, extension3, extension4, extension5, extension6;
            extension1 = Length - 2 * Cover;
            extension2 = Base - 2 * Cover;
            extension3 = extension1 - Math.Ceiling(2 * Gauge / 10.0);
            extension4 = Heigth - 2 * Cover - Math.Ceiling(2 * Gauge / 10.0);
            extension5 = Base - 2 * Cover - Math.Ceiling(2 * Gauge / 10.0);
            extension6 = Heigth - 2 * Cover;
            Point3d mirrorPoint = new Point3d(startPoint.X, startPoint.Y - 60.0 - extension2, 0.0);
            Point3d verticalStartPoint = new Point3d(startPoint.X + 30.0, startPoint.Y - 30.0, 0.0);
            Point3d verticalMirrorStartPoint = new Point3d(verticalStartPoint.X + extension1 - 60.0, verticalStartPoint.Y, 0.0);
            AddHorizontalDistribuction(startPoint, mirrorPoint, extension1, 1);
            AddVerticalDistribuction(verticalStartPoint, verticalMirrorStartPoint, extension2, 2);
            startPoint.Add(new Vector3d(extension1 + 30.0, 0.0, 0.0));
            mirrorPoint = new Point3d(startPoint.X, startPoint.Y - 60.0 - extension4, 0.0);
            verticalStartPoint = new Point3d(startPoint.X + 30.0, startPoint.Y - 30.0, 0.0);
            verticalMirrorStartPoint = new Point3d(verticalStartPoint.X + extension3 - 60.0, verticalStartPoint.Y, 0.0);
            AddHorizontalDistribuction(startPoint, mirrorPoint, extension3, 3);
            AddVerticalDistribuction(verticalStartPoint, verticalMirrorStartPoint, extension4, 4);
            startPoint.Add(new Vector3d(extension3 + 30.0, 0.0, 0.0));
            mirrorPoint = new Point3d(startPoint.X, startPoint.Y - 60.0 - extension6, 0.0);
            verticalStartPoint = new Point3d(startPoint.X + 30.0, startPoint.Y - 30.0, 0.0);
            verticalMirrorStartPoint = new Point3d(verticalStartPoint.X + extension5 - 60.0, verticalStartPoint.Y, 0.0);
            AddHorizontalDistribuction(startPoint, mirrorPoint, extension5, 5);
            AddVerticalDistribuction(verticalStartPoint, verticalMirrorStartPoint, extension6, 6);
        }
        public void AddHorizontalDistribuction(Point3d basePoint, Point3d mirrorPoint, double extension, int id)
        {
            double[] hookLengths = new double[4] { 0.0, AnchorFactor * Gauge, AnchorFactor * Gauge, 0.0 };
            HorizontalBar bar = new HorizontalBar()
            {
                Id = id,
                IsVariable = false,
                AmendmentLength = (int)Math.Ceiling(2 * Gauge * AnchorFactor / 10.0),
                BarDir = -1,
                HookLengths = hookLengths,
                Gauge = Gauge
            };
            bar.DrawBar(basePoint, extension);
            distribuctions.Add(bar);
            HorizontalBar mirrorBar = new HorizontalBar()
            {
                Id = id,
                IsVariable = false,
                AmendmentLength = (int)Math.Ceiling(2 * Gauge * AnchorFactor / 10.0),
                BarDir = 1,
                HookLengths = hookLengths,
                Gauge = Gauge
            };
            mirrorBar.DrawBar(mirrorPoint, extension);
            distribuctions.Add(mirrorBar);
        }
        public void AddVerticalDistribuction(Point3d basePoint, Point3d mirrorPoint, double extension, int id)
        {
            double[] hookLengths = new double[4] { 0.0, AnchorFactor * Gauge, AnchorFactor * Gauge, 0.0 };
            VerticalBar bar = new VerticalBar()
            {
                Id = id,
                IsVariable = false,
                AmendmentLength = (int)Math.Ceiling(2 * Gauge * AnchorFactor / 10.0),
                BarDir = 1,
                HookLengths = hookLengths,
                Gauge = Gauge
            };
            bar.DrawBar(basePoint, extension);
            distribuctions.Add(bar);
            VerticalBar mirrorBar = new VerticalBar()
            {
                Id = id,
                IsVariable = false,
                AmendmentLength = (int)Math.Ceiling(2 * Gauge * AnchorFactor / 10.0),
                BarDir = -1,
                HookLengths = hookLengths,
                Gauge = Gauge
            };
            mirrorBar.DrawBar(mirrorPoint, extension);
            distribuctions.Add(mirrorBar);
        }
        public void CreateSmallDistribuctions(Point3d startPoint)
        {
            double extension7, extension8, extension9;
            extension7 = Width - 2 * Cover;
            extension8 = SmallestHeigth - 2 * Cover - Math.Ceiling(2 * Gauge / 10.0);
            extension9 = Width - 2 * Cover - Math.Ceiling(2 * Gauge / 10.0);
            double hook = Width - Cover + StandardDistribuction.GetAnchorLength(Gauge / 10.0, AnchorFactor);
            double[] hookLengths = new double[4] { 0.0, hook, hook, 0.0 };
            VerticalBar bar = new VerticalBar()
            {
                Id = 7,
                IsVariable = false,
                AmendmentLength = (int)Math.Ceiling(2 * Gauge * AnchorFactor / 10.0),
                BarDir = -1,
                HookLengths = hookLengths,
                Gauge = Gauge
            };
            bar.DrawBar(startPoint, extension7);
            distribuctions.Add(bar);
            hook = Width - Cover + StandardDistribuction.GetAnchorLength(Gauge / 10.0, AnchorFactor - 1);
            hookLengths[1] = hook;
            hookLengths[2] = hook;
            startPoint.Add(new Vector3d(55.0 + hook,0.0,0.0));
            bar = new VerticalBar()
            {
                Id = 8,
                IsVariable = false,
                AmendmentLength = (int)Math.Ceiling(2 * Gauge * AnchorFactor / 10.0),
                BarDir = -1,
                HookLengths = hookLengths,
                Gauge = Gauge
            };
            bar.DrawBar(startPoint, extension8);
            distribuctions.Add(bar);

            hook = SmallestHeigth - 2 * Cover;
            hookLengths[1] = hook;
            hookLengths[2] = hook;
            startPoint.Add(new Vector3d(60.0, 0.0, 0.0));
            Point3d mirrorPoint = new Point3d(startPoint.X + 30.0, startPoint.Y - 30.0, 0.0);
            AddVerticalDistribuction(startPoint, mirrorPoint, extension9, 9);
        }
    }
}
