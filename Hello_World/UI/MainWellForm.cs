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
            
            Well.Misla = new Misla();
            MislaForm = new MislaForm(Well.Misla);

            Well.Bottom = new Bottom();
            EdgeForm = new EdgeForm(Well.Bottom);

            ToopForm = new TopForm(Well.Top);
            InitializeComponent();
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
            Point3d BasePoint = DrawingUtilities.GetPointFromUser("Insira um ponto para desenho:");

            Point3d bluePrintCenter = new Point3d(BasePoint.X + Well.Bottom.Diameter * 0.5 + BluePrintShiftness, BasePoint.Y - Well.Bottom.Diameter * 0.5 - BluePrintShiftness, 0);
            Point3d AAStartCut = new Point3d(BasePoint.X + Well.Bottom.Diameter + 270.0 + 300, BasePoint.Y - 170.0, 0);
            Point3d upperViewCenter = new Point3d(BasePoint.X + 2.5 * Well.Bottom.Diameter + 700.0 + BluePrintShiftness, BasePoint.Y - Well.Bottom.Diameter * 0.5 - BluePrintShiftness, 0);

            Well.DrawWellBluePrint(bluePrintCenter);
            Well.DrawWellAACut(AAStartCut);
            Well.DrawWellUpperView(upperViewCenter);
        }
    }
}
