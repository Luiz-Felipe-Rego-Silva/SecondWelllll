using Autodesk.AutoCAD.Geometry;
using Structures.Utilities;
using Structures.WellDetailer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hello_World.UI
{
    public partial class MainWellForm : Form
    {
        public MislaForm MislaForm { get; private set; }
        public EdgeForm EdgeForm { get; private set; }
        public TopForm ToopForm { get; private set; }
        public Well Well;
        private const double BluePrintShiftness = 120.0;
        private const double AACutXShiftness = 300.0;
        private const double AACutYShiftness = 170.0;
        public MainWellForm()
        {
            InitializeComponent();
            Well = new Well();
            Well.Misla = new Misla();
            MislaForm = new MislaForm(Well.Misla);

            Well.Bottom = new Bottom();
            EdgeForm = new EdgeForm(Well.Bottom);

            Well.Top = new Top();
            ToopForm = new TopForm(Well.Top);
        }

        private void btn_misla_Click(object sender, EventArgs e)
        {
            MislaForm.Show();
        }

        private void btn_edge_Click(object sender, EventArgs e)
        {
            EdgeForm.Show();
        }

        private void btn_top_Click(object sender, EventArgs e)
        {
            ToopForm.Show();
        }

        private void btn_steel_Click(object sender, EventArgs e)
        {

        }

        private void btn_doDetail_Click(object sender, EventArgs e)
        {
            //Construindo Parede
            double internalDiameter = Convert.ToDouble(msktxt_insideDiameter.Text);
            double wallThickness = Convert.ToDouble(msktxt_wallThickness.Text);
            double wallHeigth = Convert.ToDouble(msktxt_wallHeigth.Text);
            Well.Wall = new CircularWall(internalDiameter, wallThickness, wallHeigth);
            //Construindo Fundo
            double bottomThickness = Convert.ToDouble(msktxt_bottomThickness.Text);
            Well.Bottom = new Bottom(internalDiameter, wallThickness, bottomThickness, Well.Bottom.EdgeLength);

            //Construindo a Laje
            double topThickness = Convert.ToDouble(msktxt_topThickness.Text);
            double passarela = Convert.ToDouble(msktxt_passarela.Text);
            Well.Top = new Top(topThickness, internalDiameter, passarela, Well.Top.Hood, wallThickness);

            //Definição de Pontos
            Point3d BasePoint = DrawingUtilities.GetPointFromUser("Insira um ponto para desenho:");
            Point3d bluePrintCenter = new Point3d(BasePoint.X + Well.Bottom.Diameter * 0.5 + BluePrintShiftness, BasePoint.Y - Well.Bottom.Diameter * 0.5 - BluePrintShiftness, 0);
            Point3d upperViewCenter = new Point3d(bluePrintCenter.X + Well.Bottom.Diameter*0.5 + 300.0, bluePrintCenter.Y, 0);
            Point3d AAStartCut = new Point3d(upperViewCenter.X + Well.Bottom.Diameter*0.5 + 300, bluePrintCenter.Y, 0);
            

            Well.DrawWellBluePrint(bluePrintCenter);
            Well.DrawWellAACut(AAStartCut);
            Well.DrawWellUpperView(upperViewCenter);
        }
    }
}
