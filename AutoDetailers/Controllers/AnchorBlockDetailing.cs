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
        public double NominalDiameter { get; set; } = 50;
        public double BiggestBase { get; set; } = 400.0;
        public double SmallestBase { get; set; } = 400.0;
        public double Length { get; set; } = 200.0;
        public double Width { get; set; } 
        public double SmallLength { get; set; } = 80.0;
        public double Heigth { get; set; } = 200.0;
        public double SmallestHeigth { get; set; } = 50.0;
        public double ConcreteHeigth { get; set; }
        public double Cover { get; set; } = 5.0;
        public int Multiplier { get; set; } = 1;
        public double Spacing { get; set; } = 10.0;
        public double Gauge { get; set; } = 8.0;
        public double AnchorFactor { get; set; } = 34.0;
        private List<StandardDistribuction> _distribuctions = new List<StandardDistribuction>();
        public AnchorBlockDetailing(double nominalDiameter, double largestBase, double length,
            double heigth, double smallestHeigth, double cover,
            int multiplier, double spacing, double gauge)
        {
            NominalDiameter = nominalDiameter;
            BiggestBase = largestBase;
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
        public AnchorBlockDetailing()
        {
        }
        public void DrawDetailment() 
        {
            Point3d BasePoint = DrawingUtilities.GetPointFromUser("Insira um ponto para desenho:");
            //CreateBigDistribuctions(BasePoint);
            double verticalShifftnes = Math.Max(Length, Math.Max(Heigth, BiggestBase));
            BasePoint = BasePoint.Add(new Vector3d(0.0, -verticalShifftnes -150.0, 0.0));
            CreateSmallDistribuctions(BasePoint);
        }
        //public void CreateBigDistribuctions(Point3d startPoint)
        //{
        //    double extension1, extension2, extension3, extension4, extension5, extension6;
        //    extension1 = Length - 2 * Cover;
        //    extension2 = Base - 2 * Cover;
        //    extension3 = extension1 - Math.Ceiling(2 * Gauge / 10.0);
        //    extension4 = Heigth - 2 * Cover - Math.Ceiling(2 * Gauge / 10.0);
        //    extension5 = Base - 2 * Cover - Math.Ceiling(2 * Gauge / 10.0);
        //    extension6 = Heigth - 2 * Cover;
        //    Point3d mirrorPoint = new Point3d(startPoint.X, startPoint.Y - 60.0 - extension2, 0.0);
        //    Point3d verticalStartPoint = new Point3d(startPoint.X + 30.0, startPoint.Y - 30.0, 0.0);
        //    Point3d verticalMirrorStartPoint = new Point3d(verticalStartPoint.X + extension1 - 60.0, verticalStartPoint.Y, 0.0);
        //    AddHorizontalDistribuction(startPoint, mirrorPoint, extension1, 1);
        //    AddVerticalDistribuction(verticalStartPoint, verticalMirrorStartPoint, extension2, 2);
        //    startPoint = startPoint.Add(new Vector3d(extension1 + 55.0, 0.0, 0.0));
        //    mirrorPoint = new Point3d(startPoint.X, startPoint.Y - 60.0 - extension4, 0.0);
        //    verticalStartPoint = new Point3d(startPoint.X + 30.0, startPoint.Y - 30.0, 0.0);
        //    verticalMirrorStartPoint = new Point3d(verticalStartPoint.X + extension3 - 60.0, verticalStartPoint.Y, 0.0);
        //    AddHorizontalDistribuction(startPoint, mirrorPoint, extension3, 3);
        //    AddVerticalDistribuction(verticalStartPoint, verticalMirrorStartPoint, extension4, 4);
        //    startPoint = startPoint.Add(new Vector3d(extension3 + 55.0, 0.0, 0.0));
        //    mirrorPoint = new Point3d(startPoint.X, startPoint.Y - 60.0 - extension6, 0.0);
        //    verticalStartPoint = new Point3d(startPoint.X + 30.0, startPoint.Y - 30.0, 0.0);
        //    verticalMirrorStartPoint = new Point3d(verticalStartPoint.X + extension5 - 60.0, verticalStartPoint.Y, 0.0);
        //    AddHorizontalDistribuction(startPoint, mirrorPoint, extension5, 5);
        //    AddVerticalDistribuction(verticalStartPoint, verticalMirrorStartPoint, extension6, 6);
        //}
        public void AddHorizontalDistribuction(Point3d basePoint, Point3d mirrorPoint, double extension, int id)
        {
            double[] hookLengths = new double[4] { 0.0, StandardDistribuction.GetAnchorLength(Gauge/10.0, AnchorFactor), StandardDistribuction.GetAnchorLength(Gauge/10.0, AnchorFactor), 0.0 };
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
            _distribuctions.Add(bar);
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
            _distribuctions.Add(mirrorBar);
        }
        public void AddVerticalDistribuction(Point3d basePoint, Point3d mirrorPoint, double extension, int id)
        {
            double[] hookLengths = new double[4] { 0.0, StandardDistribuction.GetAnchorLength(Gauge/10.0, AnchorFactor), StandardDistribuction.GetAnchorLength(Gauge/10.0, AnchorFactor), 0.0 };
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
            _distribuctions.Add(bar);
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
            _distribuctions.Add(mirrorBar);
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
                Id = 10,
                IsVariable = false,
                AmendmentLength = (int)Math.Ceiling(2 * Gauge * AnchorFactor / 10.0),
                BarDir = -1,
                HookLengths = hookLengths,
                Gauge = Gauge
            };
            bar.DrawBar(startPoint, extension7);
            _distribuctions.Add(bar);
            hook = Width - Cover + StandardDistribuction.GetAnchorLength(Gauge / 10.0, AnchorFactor - 1);
            hookLengths[1] = hook;
            hookLengths[2] = hook;
            startPoint = startPoint.Add(new Vector3d(55.0 + hook,0.0,0.0));
            bar = new VerticalBar()
            {
                Id = 11,
                IsVariable = false,
                AmendmentLength = (int)Math.Ceiling(2 * Gauge * AnchorFactor / 10.0),
                BarDir = -1,
                HookLengths = hookLengths,
                Gauge = Gauge
            };
            bar.DrawBar(startPoint, extension8);
            _distribuctions.Add(bar);

            hook = SmallestHeigth - 2 * Cover;
            hookLengths[1] = hook;
            hookLengths[2] = hook;
            startPoint = startPoint.Add(new Vector3d(60.0, 0.0, 0.0));
            Point3d mirrorPoint = new Point3d(startPoint.X + 30.0, startPoint.Y - 30.0, 0.0);
            AddVerticalDistribuction(startPoint, mirrorPoint, extension9, 12);
        }
        private int GetQuantity(double extensionDist) 
        {
            int quantity = 1 + (int) (extensionDist/Spacing);
            return quantity;
        }
        private void VerticalNewDistribuction(int id, double extension, int dir, double heigth, double offset) 
        {
            int quantity = GetQuantity(extension);
            double[] hookLength = new double[4] { 0.0, StandardDistribuction.GetAnchorLength(Gauge/10.0, AnchorFactor), StandardDistribuction.GetAnchorLength(Gauge / 10.0, AnchorFactor), 0.0};
            VerticalVarBar bar = new VerticalVarBar(new StandardDistribuction()
            {
                Id = id,
                Gauge = this.Gauge,
                Spacing = this.Spacing,
                AmendmentLength = 2 * (int)StandardDistribuction.GetAnchorLength(Gauge / 10.0, AnchorFactor),
                Quantity = quantity,
                IsVariable = true
            },  2 * StandardDistribuction.GetAnchorLength(Gauge / 10.0, AnchorFactor))
            { 
                BarDir = dir,
                HookLengths = hookLength
            };
            bar.SetLengths(GenerateVarLengths(extension, offset, heigth));
            _distribuctions.Add(bar);
        }
        private void HorizontalNewDistribuction(int id, double extension, int dir, double heigth, double offset)
        {
            int quantity = GetQuantity(extension);
            double[] hookLength = new double[4] { 0.0, StandardDistribuction.GetAnchorLength(Gauge / 10.0, AnchorFactor), StandardDistribuction.GetAnchorLength(Gauge / 10.0, AnchorFactor), 0.0 };
            HorizontalVarBar bar = new HorizontalVarBar(new StandardDistribuction()
            {
                Id = id,
                Gauge = this.Gauge,
                Spacing = this.Spacing,
                AmendmentLength = 2 * (int)StandardDistribuction.GetAnchorLength(Gauge / 10.0, AnchorFactor),
                Quantity = quantity,
                IsVariable = true
            }, 2 * StandardDistribuction.GetAnchorLength(Gauge / 10.0, AnchorFactor))
            {
                BarDir = dir,
                HookLengths = hookLength
            };
            bar.SetLengths(GenerateVarLengths(extension, offset, heigth));
        }
        private double[] GenerateVarLengths(double extension, double shiftness, double heigth) 
        {
            int quantity = GetQuantity(extension);
            double[] varLengths = new double[quantity];
            double realSpacing = extension / (quantity - 1);
            for (int index = 0; index < quantity; index++) { varLengths[index] = heigth + index * realSpacing - shiftness; }
            return varLengths;
        }
        private void SetStandardDistribuctions() 
        {
            //id 5
            StandardDistribuction bar = new StandardDistribuction()
            {
                Id = 5,
                Gauge = this.Gauge,
                Spacing = Spacing,
                IsVariable = false,
                Quantity = GetQuantity(Length -  2 * Cover - Math.Ceiling(2 * Gauge/10.0))
            };
        }
        public void AddHorizontalDistribuction(double extension, int id, int dir, double heigth)
        {
            double[] hookLengths = new double[4] { 0.0, StandardDistribuction.GetAnchorLength(Gauge / 10.0, AnchorFactor), StandardDistribuction.GetAnchorLength(Gauge / 10.0, AnchorFactor), 0.0 };
            HorizontalBar bar = new HorizontalBar()
            {
                Id = id,
                IsVariable = false,
                AmendmentLength = (int)Math.Ceiling(2 * Gauge * AnchorFactor / 10.0),
                BarDir = dir,
                HookLengths = hookLengths,
                Gauge = Gauge,
                Spacing = this.Spacing,
                Quantity = GetQuantity(extension),
                HookLength = hookLengths
            };
            _distribuctions.Add(bar);
        }
        public void AddVerticalDistribuction(double extension, int id, int dir, double heigth)
        {
            double[] hookLengths = new double[4] { 0.0, StandardDistribuction.GetAnchorLength(Gauge / 10.0, AnchorFactor), StandardDistribuction.GetAnchorLength(Gauge / 10.0, AnchorFactor), 0.0 };
            VerticalBar bar = new VerticalBar()
            {
                Id = id,
                IsVariable = false,
                AmendmentLength = (int)Math.Ceiling(2 * Gauge * AnchorFactor / 10.0),
                BarDir = dir,
                HookLengths = hookLengths,
                Gauge = Gauge,
                Spacing = this.Spacing,
                Quantity = GetQuantity(extension),
                HookLength = hookLengths
            };
            _distribuctions.Add(bar);
        }
    }
}
