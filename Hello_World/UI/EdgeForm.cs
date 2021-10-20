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
    public partial class EdgeForm : Form
    {
        public double EdgeLength { get; private set; }
        private Bottom Bottom;
        public EdgeForm(Bottom bottom)
        {
            InitializeComponent();
            Bottom = bottom;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            EdgeLength = Math.Round(Convert.ToDouble(msktxt_edgeLength.Text), 3);
            Bottom.EdgeLength = EdgeLength;
            this.Hide();
        }
    }
}
