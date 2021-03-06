using Autodesk.AutoCAD.Geometry;
using Controllers;
using Structures.Utilities;
using System;
using System.Windows.Forms;

namespace Views.DetailersViews
{
    public partial class CircularBottomForm : Form
    {
        private CircularBottomDetailing _bottomDetailing;
        public CircularBottomForm()
        {
            InitializeComponent();

        }
        private void Btn_detail_Click(object sender, EventArgs e)
        {
            try
            {
                _bottomDetailing = new CircularBottomDetailing
                {
                    Title = rchtxt_title.Text
                };
                SetArmorBottom();
                SetBorderCondition();
                SetBorderShape();
                SetCutInfo();
                _bottomDetailing.Multiplier = Convert.ToInt32(msktxt_multiplier.Text);
                Point3d BasePoint = DrawingUtilities.GetPointFromUser("Insira um ponto para o desenho:");
                _bottomDetailing.DrawDetailment(BasePoint);
            }
            catch (Exception) { }
        }
        private void SetArmorBottom()
        {
            double gaugeX, gaugeY, spacingX, spacingY, cover, anchorFactor;
            gaugeX = Convert.ToDouble(cmbbx_horizontalBottomGauge.Text);
            gaugeY = Convert.ToDouble(cmbbx_verticalBottomGauge.Text);
            spacingX = Convert.ToDouble(msktxt_horizontalSpacing.Text);
            spacingY = Convert.ToDouble(msktxt_verticalSpacing.Text);
            cover = Convert.ToDouble(msktxt_cover.Text);
            anchorFactor = Convert.ToDouble(msktxt_anchorFactor.Text);
            _bottomDetailing.SetArmorInfo(gaugeX, gaugeY, spacingX, spacingY, anchorFactor, cover);

        }
        private void SetBorderCondition()
        {
            bool[] verticalConditions = new bool[4] { chcBx_NegativeUpEngaged.Checked, chcBx_NegativeDownEngaged.Checked, chcBx_PositiveUpEngaged.Checked, chcBx_PositiveDownEngaged.Checked };
            _bottomDetailing.SetEngagedVerticalConditions(verticalConditions);
            bool[] horizontalConditions = new bool[4] { chcBx_NegativeLeftEngaged.Checked, chcBx_NegativaRightEngaged.Checked, chcBx_PositiveLeftEngaged.Checked, chcBx_PositiveRigthEngaged.Checked };
            _bottomDetailing.SetEngagedHorizontalConditions(horizontalConditions);
        }
        private void SetBorderShape()
        {
            double supWallThickness, infWallThickness;
            supWallThickness = Convert.ToDouble(msktxt_TopThickness.Text);
            infWallThickness = Convert.ToDouble(msktxt_InfThickness.Text);
            //if(Convert.ToDouble(msktxt_TopThickness.Text) < 1) { supWallThickness = 20.0; }
            //if (Convert.ToDouble(msktxt_InfThickness.Text) < 1) { infWallThickness = 20.0; }
            _bottomDetailing.SetBorderThickness(supWallThickness, infWallThickness);
            double diameter, thickness, edgeLength;
            diameter = Convert.ToDouble(msktxt_externalDiameter.Text);
            thickness = Convert.ToDouble(msktxt_bottomThickness.Text);
            edgeLength = Convert.ToDouble(msktxt_edgeLength.Text);
            _bottomDetailing.SetGeometryInfo(diameter, thickness, edgeLength);
        }
        private void SetCutInfo()
        {
            bool[] conditions = new bool[4];
            conditions[0] = chcBx_VerticalUpLeft.Checked;
            conditions[1] = chcBx_VerticalUpRigth.Checked;
            conditions[2] = chcBx_VerticalDownLeft.Checked;
            conditions[3] = chcBx_VerticalDownRigth.Checked;
            _bottomDetailing.SetBorderShape(conditions);
        }

        private void ChcBx_VerticalUpLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (chcBx_VerticalUpLeft.Checked) 
            {
                chcBx_NegativeUpEngaged.Enabled = true;
                chcBx_NegativeUpEngaged.Checked = true;

                chcBx_PositiveRigthEngaged.Enabled = true;
                chcBx_PositiveRigthEngaged.Checked = true;
                msktxt_InfThickness.Enabled = true; msktxt_InfThickness.Text = "20"; 
            }
            else
            {
                chcBx_NegativeUpEngaged.Enabled = false;
                chcBx_NegativeUpEngaged.Checked = false;

                chcBx_PositiveRigthEngaged.Enabled = false;
                chcBx_PositiveRigthEngaged.Checked = false;
                if (!chcBx_VerticalDownLeft.Checked) { msktxt_InfThickness.Enabled = false; msktxt_InfThickness.Text = "0"; }
            }
        }

        private void ChcBx_VerticalUpRigth_CheckedChanged(object sender, EventArgs e)
        {
            if (chcBx_VerticalUpRigth.Checked)
            {
                chcBx_PositiveUpEngaged.Enabled = true;
                chcBx_PositiveUpEngaged.Checked = true;

                chcBx_NegativaRightEngaged.Enabled = true;
                chcBx_NegativaRightEngaged.Checked = true;
                msktxt_TopThickness.Enabled = true; msktxt_TopThickness.Text = "20";
            }
            else
            {
                chcBx_PositiveUpEngaged.Enabled = false;
                chcBx_PositiveUpEngaged.Checked = false;

                chcBx_NegativaRightEngaged.Enabled = false;
                chcBx_NegativaRightEngaged.Checked = false;
                if (!chcBx_VerticalDownRigth.Checked) { msktxt_TopThickness.Enabled = false; msktxt_TopThickness.Text = "0"; }
            }
        }

        private void ChcBx_VerticalDownLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (chcBx_VerticalDownLeft.Checked)
            {
                chcBx_NegativeDownEngaged.Enabled = true;
                chcBx_NegativeDownEngaged.Checked = true;

                chcBx_PositiveLeftEngaged.Enabled = true;
                chcBx_PositiveLeftEngaged.Checked = true;
                msktxt_InfThickness.Enabled = true; msktxt_InfThickness.Text = "20";
            }
            else
            {
                chcBx_PositiveLeftEngaged.Enabled = false;
                chcBx_PositiveLeftEngaged.Checked = false;
                

                chcBx_NegativeDownEngaged.Enabled = false;
                chcBx_NegativeDownEngaged.Checked = false;
                if (!chcBx_VerticalUpLeft.Checked) { msktxt_InfThickness.Enabled = false; msktxt_InfThickness.Text = "0"; }
            }
        }

        private void ChcBx_VerticalDownRigth_CheckedChanged(object sender, EventArgs e)
        {
            if (chcBx_VerticalDownRigth.Checked)
            {
                chcBx_PositiveDownEngaged.Enabled = true;
                chcBx_PositiveDownEngaged.Checked = true;

                chcBx_NegativeLeftEngaged.Enabled = true;
                chcBx_NegativeLeftEngaged.Checked = true;
                msktxt_TopThickness.Enabled = true; msktxt_TopThickness.Text = "20"; 
            }
            else
            {
                chcBx_PositiveDownEngaged.Enabled = false;
                chcBx_PositiveDownEngaged.Checked = false;

                chcBx_NegativeLeftEngaged.Enabled = false;
                chcBx_NegativeLeftEngaged.Checked = false;
                if (!chcBx_VerticalUpRigth.Checked) { msktxt_TopThickness.Enabled = false; msktxt_TopThickness.Text = "0"; }
            }
        }

        private void Msktxt_TopThickness_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
        }
        private void Msktxt_InfThickness_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
