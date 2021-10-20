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
    public partial class MislaForm : Form
    {
        public double MislaLength { get; private set; }
        public double MislaHeigth { get; private set; }
        public Misla Misla { get; private set; }
        public MislaForm(Misla misla)
        {
            InitializeComponent();
            Misla = misla;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            MislaLength = Math.Round(Convert.ToDouble(mskdtxt_mislaLength.Text), 3);
            MislaHeigth = Math.Round(Convert.ToDouble(msktxt_mislaHeigth.Text), 3);
            Misla.Length = MislaLength;
            Misla.Heigth = MislaHeigth;
            this.Hide();
        }
    }
}
