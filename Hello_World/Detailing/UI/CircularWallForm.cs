using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
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
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Hello_World.Detailing.UI
{
    public partial class CircularWallForm : Form
    {
        public CircularWallForm()
        {
            InitializeComponent();
        }

        private void chcbx_externalTop_CheckedChanged(object sender, EventArgs e)
        {
            if (chcbx_externalTop.Checked)
                chcbx_externalTopEngaged.Checked = true;
            else if (!chcbx_externalTop.Checked)
                chcbx_externalTopEngaged.Checked = false;
        }

        private void chcbx_internalTop_CheckedChanged(object sender, EventArgs e)
        {
            if (chcbx_internalTop.Checked)
                chcbx_internalTopEngaged.Checked = true;
            else if (!chcbx_internalTop.Checked)
                chcbx_internalTopEngaged.Checked = false;

        }

        private void chcbx_externalBottom_CheckedChanged(object sender, EventArgs e)
        {
            if (chcbx_externalBottom.Checked)
                chcbx_externalBottomEngaged.Checked = true;
            else if (!chcbx_externalBottom.Checked)
                chcbx_externalBottomEngaged.Checked = false;
        }

        private void chcbx_internalBottom_CheckedChanged(object sender, EventArgs e)
        {
            if (chcbx_internalBottom.Checked)
                chcbx_internalBottomEngaged.Checked = true;
            else if (!chcbx_internalBottom.Checked)
                chcbx_internalBottomEngaged.Checked = false;

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_detail_Click(object sender, EventArgs e)
        {
            ValidateInput();
            CircularWallDetailment wallDetailment = new CircularWallDetailment();

            //Cadastro das armaduras
            double spacingX = Convert.ToDouble(msktxt_horizontalSpacing.Text);
            double gaugeX = Convert.ToDouble(cmbbx_horizontalWallGauge.Text) / 10.0;
            double spacingY = Convert.ToDouble(msktxt_verticalSpacing.Text);
            double gaugeY = Convert.ToDouble(cmbbx_verticalWallGauge.Text) / 10.0;
            double cover = Convert.ToDouble(msktxt_cover.Text);
            double anchorFactor = Convert.ToDouble(msktxt_anchorFactor.Text);
            wallDetailment.SetSteelInfo(spacingX, spacingY, gaugeX, gaugeY, anchorFactor, cover);
            //Compatibilização com as adjacenciazinhas
            double topOffset = (Convert.ToDouble(cmbbx_horizontalTopGauge.Text) + Convert.ToDouble(cmbbx_verticalTopGauge.Text)) / 10.0;
            double bottomOffset = (Convert.ToDouble(cmbbx_horizontalBottomGauge.Text) + Convert.ToDouble(cmbbx_verticalBottomGauge.Text)) / 10.0;
            wallDetailment.SetOffsets(topOffset, bottomOffset);
            //Cadastro da Geometria
            double externalHeigth = Convert.ToDouble(msktxt_externalHeigth.Text);
            double externalDiameter = Convert.ToDouble(msktxt_externalDiameter.Text);
            double wallThickness = Convert.ToDouble(msktxt_wallThickness.Text);
            double topThickness = Convert.ToDouble(msktxt_topThickness.Text);
            double bottomThickness = Convert.ToDouble(msktxt_bottomThickness.Text);
            CircularWall circWall = new CircularWall(externalDiameter - 2 * wallThickness, wallThickness, externalHeigth - topThickness - bottomThickness);
            wallDetailment.SetGeometry(circWall, topThickness, bottomThickness);
            //Extra
            int multiplier = Convert.ToInt32(msktxt_multiplier.Text);
            wallDetailment.setAnnotations(rchtxt_title.Text, multiplier);
            //Cadastro das condições de contorno
            wallDetailment.SetBorderConditions(chcbx_internalTopEngaged.Checked, chcbx_externalTopEngaged.Checked, chcbx_internalBottomEngaged.Checked, chcbx_externalBottomEngaged.Checked);

            Point3d BasePoint = DrawingUtilities.GetPointFromUser("Insira um ponto para desenho:");
            wallDetailment.DrawDistribuctions(BasePoint);


        }
        private void ValidateInput() { }

        private void CircularWallForm_Load(object sender, EventArgs e)
        {

        }
    }
}
