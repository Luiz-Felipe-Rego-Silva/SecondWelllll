using Autodesk.AutoCAD.Geometry;
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
using WellDetailing.WellDetailer.Entities;

namespace Hello_World.UI
{
    public partial class TopForm : Form
    {
        public double HorizontalGap { get; private set; }
        public double VerticalGap { get; private set; }
        public double InternalHoodDiameter { get; private set; }
        private Top Top;
        public TopForm(Top top)
        {
            InitializeComponent();
            Top = top;
            Top.Hood = new CircularHood();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            InternalHoodDiameter = Math.Round(Convert.ToDouble(msktxt_insideDiameter.Text), 3);
            Top.Hood.InternalShiftness = InternalHoodDiameter;
            HorizontalGap = Math.Round(Convert.ToDouble(msktxt_horizontalGap.Text), 3);
            Top.Hood.HorizontalGap = HorizontalGap;
            VerticalGap = Math.Round(Convert.ToDouble(msktxt_verticalGap.Text), 3);
            Top.Hood.VerticalGap = VerticalGap;

            double posX = Convert.ToDouble(msktxt_posX.Text);
            double posY = Convert.ToDouble(msktxt_posY.Text);
            Top.Hood.Position = new Point3d(posX, posY, 0);

            this.Hide();
        }
    }
}
