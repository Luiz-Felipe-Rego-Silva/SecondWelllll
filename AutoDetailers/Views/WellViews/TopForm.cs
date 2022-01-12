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

namespace AutoDetailers.UI
{
    public partial class TopForm : Form
    {
        public double HorizontalGap { get; private set; }
        public double VerticalGap { get; private set; }
        public double InternalHoodDiameter { get; private set; }
        private Top TopWell;
        public TopForm(Top top)
        {
            InitializeComponent();
            TopWell = top;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            InternalHoodDiameter = Math.Round(Convert.ToDouble(msktxt_insideDiameter.Text), 3);
            HorizontalGap = Math.Round(Convert.ToDouble(msktxt_horizontalGap.Text), 3);
            VerticalGap = Math.Round(Convert.ToDouble(msktxt_verticalGap.Text), 3);
            double posX = Convert.ToDouble(msktxt_posX.Text);
            double posY = Convert.ToDouble(msktxt_posY.Text);
            Point3d Position = new Point3d(posX, posY, 0);
            TopWell.Hood = new CircularHood(HorizontalGap, VerticalGap, Position, InternalHoodDiameter);
            this.Hide();
        }
    }
}
