namespace Views.DetailersViews
{
    partial class CircularBottomForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CircularBottomForm));
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_detail = new System.Windows.Forms.Button();
            this.rchtxt_title = new System.Windows.Forms.RichTextBox();
            this.msktxt_multiplier = new System.Windows.Forms.MaskedTextBox();
            this.lbl_title = new System.Windows.Forms.Label();
            this.lbl_multiplier = new System.Windows.Forms.Label();
            this.lbl_unitAlert = new System.Windows.Forms.Label();
            this.grp_AdjacentElements = new System.Windows.Forms.GroupBox();
            this.msktxt_InfThickness = new System.Windows.Forms.MaskedTextBox();
            this.lbl_elementInf = new System.Windows.Forms.Label();
            this.msktxt_TopThickness = new System.Windows.Forms.MaskedTextBox();
            this.lbl_elementTop = new System.Windows.Forms.Label();
            this.grp_geometry = new System.Windows.Forms.GroupBox();
            this.msktxt_bottomThickness = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_edgeLength = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_externalDiameter = new System.Windows.Forms.MaskedTextBox();
            this.lbl_edgeLength = new System.Windows.Forms.Label();
            this.lbl_bottomThickness = new System.Windows.Forms.Label();
            this.lbl_externalDiameter = new System.Windows.Forms.Label();
            this.grp_steel = new System.Windows.Forms.GroupBox();
            this.cmbbx_horizontalBottomGauge = new System.Windows.Forms.ComboBox();
            this.cmbbx_verticalBottomGauge = new System.Windows.Forms.ComboBox();
            this.msktxt_horizontalSpacing = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_verticalSpacing = new System.Windows.Forms.MaskedTextBox();
            this.lbl_horizontalSpacing = new System.Windows.Forms.Label();
            this.lbl_horizontalGauge = new System.Windows.Forms.Label();
            this.lbl_verticalSpacing = new System.Windows.Forms.Label();
            this.lbl_verticalGauge = new System.Windows.Forms.Label();
            this.grp_Factors = new System.Windows.Forms.GroupBox();
            this.msktxt_anchorFactor = new System.Windows.Forms.MaskedTextBox();
            this.msktxt_cover = new System.Windows.Forms.MaskedTextBox();
            this.lbl_anchorFactor = new System.Windows.Forms.Label();
            this.lbl_cover = new System.Windows.Forms.Label();
            this.grp_sideShapes = new System.Windows.Forms.GroupBox();
            this.lbl_horizontalEngaged = new System.Windows.Forms.Label();
            this.grpBx_horizontalConditions = new System.Windows.Forms.GroupBox();
            this.chcBx_PositiveRigthEngaged = new System.Windows.Forms.CheckBox();
            this.chcBx_NegativaRightEngaged = new System.Windows.Forms.CheckBox();
            this.lbl_horizontalPositiveEngagedConditions = new System.Windows.Forms.Label();
            this.lbl_horizontalNegativeEngagedConditions = new System.Windows.Forms.Label();
            this.chcBx_PositiveLeftEngaged = new System.Windows.Forms.CheckBox();
            this.chcBx_NegativeLeftEngaged = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chcBx_VerticalUpLeft = new System.Windows.Forms.CheckBox();
            this.chcBx_VerticalDownLeft = new System.Windows.Forms.CheckBox();
            this.chcBx_VerticalDownRigth = new System.Windows.Forms.CheckBox();
            this.chcBx_VerticalUpRigth = new System.Windows.Forms.CheckBox();
            this.lbl_verticalPositiveEngagedConditions = new System.Windows.Forms.Label();
            this.lbl_verticalNegativeEngagedConditions = new System.Windows.Forms.Label();
            this.chcBx_PositiveUpEngaged = new System.Windows.Forms.CheckBox();
            this.chcBx_PositiveDownEngaged = new System.Windows.Forms.CheckBox();
            this.chcBx_NegativeDownEngaged = new System.Windows.Forms.CheckBox();
            this.chcBx_NegativeUpEngaged = new System.Windows.Forms.CheckBox();
            this.grpBx_verticalConditions = new System.Windows.Forms.GroupBox();
            this.grp_AdjacentElements.SuspendLayout();
            this.grp_geometry.SuspendLayout();
            this.grp_steel.SuspendLayout();
            this.grp_Factors.SuspendLayout();
            this.grp_sideShapes.SuspendLayout();
            this.grpBx_horizontalConditions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpBx_verticalConditions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(307, 446);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(82, 36);
            this.btn_cancel.TabIndex = 7;
            this.btn_cancel.Text = "Cancelar";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // btn_detail
            // 
            this.btn_detail.Location = new System.Drawing.Point(395, 446);
            this.btn_detail.Name = "btn_detail";
            this.btn_detail.Size = new System.Drawing.Size(74, 36);
            this.btn_detail.TabIndex = 6;
            this.btn_detail.Text = "Detalhar";
            this.btn_detail.UseVisualStyleBackColor = true;
            this.btn_detail.Click += new System.EventHandler(this.Btn_detail_Click);
            // 
            // rchtxt_title
            // 
            this.rchtxt_title.Location = new System.Drawing.Point(53, 26);
            this.rchtxt_title.Multiline = false;
            this.rchtxt_title.Name = "rchtxt_title";
            this.rchtxt_title.ShowSelectionMargin = true;
            this.rchtxt_title.Size = new System.Drawing.Size(248, 16);
            this.rchtxt_title.TabIndex = 0;
            this.rchtxt_title.Text = "";
            // 
            // msktxt_multiplier
            // 
            this.msktxt_multiplier.Location = new System.Drawing.Point(386, 22);
            this.msktxt_multiplier.Name = "msktxt_multiplier";
            this.msktxt_multiplier.Size = new System.Drawing.Size(90, 20);
            this.msktxt_multiplier.TabIndex = 1;
            this.msktxt_multiplier.Text = "1";
            this.msktxt_multiplier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Location = new System.Drawing.Point(12, 29);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(35, 13);
            this.lbl_title.TabIndex = 26;
            this.lbl_title.Text = "Título";
            // 
            // lbl_multiplier
            // 
            this.lbl_multiplier.AutoSize = true;
            this.lbl_multiplier.Location = new System.Drawing.Point(314, 25);
            this.lbl_multiplier.Name = "lbl_multiplier";
            this.lbl_multiplier.Size = new System.Drawing.Size(66, 13);
            this.lbl_multiplier.TabIndex = 24;
            this.lbl_multiplier.Text = "Multiplicador";
            // 
            // lbl_unitAlert
            // 
            this.lbl_unitAlert.AutoSize = true;
            this.lbl_unitAlert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_unitAlert.ForeColor = System.Drawing.Color.Red;
            this.lbl_unitAlert.Location = new System.Drawing.Point(12, 7);
            this.lbl_unitAlert.Name = "lbl_unitAlert";
            this.lbl_unitAlert.Size = new System.Drawing.Size(284, 13);
            this.lbl_unitAlert.TabIndex = 22;
            this.lbl_unitAlert.Text = "Atenção: Todos os campos devem ser preenchidos em cm";
            // 
            // grp_AdjacentElements
            // 
            this.grp_AdjacentElements.Controls.Add(this.msktxt_InfThickness);
            this.grp_AdjacentElements.Controls.Add(this.lbl_elementInf);
            this.grp_AdjacentElements.Controls.Add(this.msktxt_TopThickness);
            this.grp_AdjacentElements.Controls.Add(this.lbl_elementTop);
            this.grp_AdjacentElements.Location = new System.Drawing.Point(307, 360);
            this.grp_AdjacentElements.Name = "grp_AdjacentElements";
            this.grp_AdjacentElements.Size = new System.Drawing.Size(169, 80);
            this.grp_AdjacentElements.TabIndex = 5;
            this.grp_AdjacentElements.TabStop = false;
            this.grp_AdjacentElements.Text = "Elementos Adjacentes";
            // 
            // msktxt_InfThickness
            // 
            this.msktxt_InfThickness.Location = new System.Drawing.Point(118, 49);
            this.msktxt_InfThickness.Name = "msktxt_InfThickness";
            this.msktxt_InfThickness.Size = new System.Drawing.Size(44, 20);
            this.msktxt_InfThickness.TabIndex = 2;
            this.msktxt_InfThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.msktxt_InfThickness.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.Msktxt_InfThickness_MaskInputRejected);
            // 
            // lbl_elementInf
            // 
            this.lbl_elementInf.AutoSize = true;
            this.lbl_elementInf.Location = new System.Drawing.Point(4, 53);
            this.lbl_elementInf.Name = "lbl_elementInf";
            this.lbl_elementInf.Size = new System.Drawing.Size(90, 13);
            this.lbl_elementInf.TabIndex = 11;
            this.lbl_elementInf.Text = "Espessura inferior";
            // 
            // msktxt_TopThickness
            // 
            this.msktxt_TopThickness.Location = new System.Drawing.Point(118, 23);
            this.msktxt_TopThickness.Name = "msktxt_TopThickness";
            this.msktxt_TopThickness.Size = new System.Drawing.Size(44, 20);
            this.msktxt_TopThickness.TabIndex = 1;
            this.msktxt_TopThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.msktxt_TopThickness.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.Msktxt_TopThickness_MaskInputRejected);
            // 
            // lbl_elementTop
            // 
            this.lbl_elementTop.AutoSize = true;
            this.lbl_elementTop.Location = new System.Drawing.Point(3, 26);
            this.lbl_elementTop.Name = "lbl_elementTop";
            this.lbl_elementTop.Size = new System.Drawing.Size(96, 13);
            this.lbl_elementTop.TabIndex = 9;
            this.lbl_elementTop.Text = "Espessura superior";
            // 
            // grp_geometry
            // 
            this.grp_geometry.Controls.Add(this.msktxt_bottomThickness);
            this.grp_geometry.Controls.Add(this.msktxt_edgeLength);
            this.grp_geometry.Controls.Add(this.msktxt_externalDiameter);
            this.grp_geometry.Controls.Add(this.lbl_edgeLength);
            this.grp_geometry.Controls.Add(this.lbl_bottomThickness);
            this.grp_geometry.Controls.Add(this.lbl_externalDiameter);
            this.grp_geometry.Location = new System.Drawing.Point(307, 257);
            this.grp_geometry.Name = "grp_geometry";
            this.grp_geometry.Size = new System.Drawing.Size(169, 97);
            this.grp_geometry.TabIndex = 4;
            this.grp_geometry.TabStop = false;
            this.grp_geometry.Text = "Geometria";
            // 
            // msktxt_bottomThickness
            // 
            this.msktxt_bottomThickness.Location = new System.Drawing.Point(112, 43);
            this.msktxt_bottomThickness.Name = "msktxt_bottomThickness";
            this.msktxt_bottomThickness.Size = new System.Drawing.Size(44, 20);
            this.msktxt_bottomThickness.TabIndex = 2;
            this.msktxt_bottomThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // msktxt_edgeLength
            // 
            this.msktxt_edgeLength.Location = new System.Drawing.Point(112, 73);
            this.msktxt_edgeLength.Name = "msktxt_edgeLength";
            this.msktxt_edgeLength.Size = new System.Drawing.Size(44, 20);
            this.msktxt_edgeLength.TabIndex = 3;
            this.msktxt_edgeLength.Text = "0";
            this.msktxt_edgeLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.msktxt_edgeLength.Visible = false;
            // 
            // msktxt_externalDiameter
            // 
            this.msktxt_externalDiameter.Location = new System.Drawing.Point(112, 13);
            this.msktxt_externalDiameter.Name = "msktxt_externalDiameter";
            this.msktxt_externalDiameter.Size = new System.Drawing.Size(44, 20);
            this.msktxt_externalDiameter.TabIndex = 1;
            this.msktxt_externalDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_edgeLength
            // 
            this.lbl_edgeLength.AutoSize = true;
            this.lbl_edgeLength.Location = new System.Drawing.Point(6, 73);
            this.lbl_edgeLength.Name = "lbl_edgeLength";
            this.lbl_edgeLength.Size = new System.Drawing.Size(31, 13);
            this.lbl_edgeLength.TabIndex = 12;
            this.lbl_edgeLength.Text = "Abas";
            this.lbl_edgeLength.Visible = false;
            // 
            // lbl_bottomThickness
            // 
            this.lbl_bottomThickness.AutoSize = true;
            this.lbl_bottomThickness.Location = new System.Drawing.Point(6, 46);
            this.lbl_bottomThickness.Name = "lbl_bottomThickness";
            this.lbl_bottomThickness.Size = new System.Drawing.Size(56, 13);
            this.lbl_bottomThickness.TabIndex = 14;
            this.lbl_bottomThickness.Text = "Espessura";
            // 
            // lbl_externalDiameter
            // 
            this.lbl_externalDiameter.AutoSize = true;
            this.lbl_externalDiameter.Location = new System.Drawing.Point(6, 16);
            this.lbl_externalDiameter.Name = "lbl_externalDiameter";
            this.lbl_externalDiameter.Size = new System.Drawing.Size(88, 13);
            this.lbl_externalDiameter.TabIndex = 11;
            this.lbl_externalDiameter.Text = "Diâmetro Externo";
            // 
            // grp_steel
            // 
            this.grp_steel.Controls.Add(this.cmbbx_horizontalBottomGauge);
            this.grp_steel.Controls.Add(this.cmbbx_verticalBottomGauge);
            this.grp_steel.Controls.Add(this.msktxt_horizontalSpacing);
            this.grp_steel.Controls.Add(this.msktxt_verticalSpacing);
            this.grp_steel.Controls.Add(this.lbl_horizontalSpacing);
            this.grp_steel.Controls.Add(this.lbl_horizontalGauge);
            this.grp_steel.Controls.Add(this.lbl_verticalSpacing);
            this.grp_steel.Controls.Add(this.lbl_verticalGauge);
            this.grp_steel.Location = new System.Drawing.Point(307, 130);
            this.grp_steel.Name = "grp_steel";
            this.grp_steel.Size = new System.Drawing.Size(169, 121);
            this.grp_steel.TabIndex = 3;
            this.grp_steel.TabStop = false;
            this.grp_steel.Text = "Armadura";
            // 
            // cmbbx_horizontalBottomGauge
            // 
            this.cmbbx_horizontalBottomGauge.FormattingEnabled = true;
            this.cmbbx_horizontalBottomGauge.Items.AddRange(new object[] {
            "5.0",
            "6.3",
            "8.0",
            "10.0",
            "12.5",
            "16.0",
            "20.0",
            "25.0",
            "32.0",
            "40.0"});
            this.cmbbx_horizontalBottomGauge.Location = new System.Drawing.Point(96, 75);
            this.cmbbx_horizontalBottomGauge.Name = "cmbbx_horizontalBottomGauge";
            this.cmbbx_horizontalBottomGauge.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbbx_horizontalBottomGauge.Size = new System.Drawing.Size(60, 21);
            this.cmbbx_horizontalBottomGauge.TabIndex = 3;
            this.cmbbx_horizontalBottomGauge.Text = "5.0";
            // 
            // cmbbx_verticalBottomGauge
            // 
            this.cmbbx_verticalBottomGauge.FormattingEnabled = true;
            this.cmbbx_verticalBottomGauge.Items.AddRange(new object[] {
            "5.0",
            "6.3",
            "8.0",
            "10.0",
            "12.5",
            "16.0",
            "20.0",
            "25.0",
            "32.0",
            "40.0"});
            this.cmbbx_verticalBottomGauge.Location = new System.Drawing.Point(96, 17);
            this.cmbbx_verticalBottomGauge.Name = "cmbbx_verticalBottomGauge";
            this.cmbbx_verticalBottomGauge.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbbx_verticalBottomGauge.Size = new System.Drawing.Size(60, 21);
            this.cmbbx_verticalBottomGauge.TabIndex = 1;
            this.cmbbx_verticalBottomGauge.Text = "5.0";
            // 
            // msktxt_horizontalSpacing
            // 
            this.msktxt_horizontalSpacing.Location = new System.Drawing.Point(96, 97);
            this.msktxt_horizontalSpacing.Name = "msktxt_horizontalSpacing";
            this.msktxt_horizontalSpacing.Size = new System.Drawing.Size(60, 20);
            this.msktxt_horizontalSpacing.TabIndex = 4;
            this.msktxt_horizontalSpacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // msktxt_verticalSpacing
            // 
            this.msktxt_verticalSpacing.Location = new System.Drawing.Point(96, 39);
            this.msktxt_verticalSpacing.Name = "msktxt_verticalSpacing";
            this.msktxt_verticalSpacing.Size = new System.Drawing.Size(60, 20);
            this.msktxt_verticalSpacing.TabIndex = 2;
            this.msktxt_verticalSpacing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_horizontalSpacing
            // 
            this.lbl_horizontalSpacing.AutoSize = true;
            this.lbl_horizontalSpacing.Location = new System.Drawing.Point(6, 100);
            this.lbl_horizontalSpacing.Name = "lbl_horizontalSpacing";
            this.lbl_horizontalSpacing.Size = new System.Drawing.Size(72, 13);
            this.lbl_horizontalSpacing.TabIndex = 10;
            this.lbl_horizontalSpacing.Text = "Espaçamento";
            // 
            // lbl_horizontalGauge
            // 
            this.lbl_horizontalGauge.AutoSize = true;
            this.lbl_horizontalGauge.Location = new System.Drawing.Point(6, 78);
            this.lbl_horizontalGauge.Name = "lbl_horizontalGauge";
            this.lbl_horizontalGauge.Size = new System.Drawing.Size(54, 13);
            this.lbl_horizontalGauge.TabIndex = 9;
            this.lbl_horizontalGauge.Text = "Horizontal";
            // 
            // lbl_verticalSpacing
            // 
            this.lbl_verticalSpacing.AutoSize = true;
            this.lbl_verticalSpacing.Location = new System.Drawing.Point(6, 42);
            this.lbl_verticalSpacing.Name = "lbl_verticalSpacing";
            this.lbl_verticalSpacing.Size = new System.Drawing.Size(72, 13);
            this.lbl_verticalSpacing.TabIndex = 8;
            this.lbl_verticalSpacing.Text = "Espaçamento";
            // 
            // lbl_verticalGauge
            // 
            this.lbl_verticalGauge.AutoSize = true;
            this.lbl_verticalGauge.Location = new System.Drawing.Point(6, 20);
            this.lbl_verticalGauge.Name = "lbl_verticalGauge";
            this.lbl_verticalGauge.Size = new System.Drawing.Size(42, 13);
            this.lbl_verticalGauge.TabIndex = 7;
            this.lbl_verticalGauge.Text = "Vertical";
            // 
            // grp_Factors
            // 
            this.grp_Factors.Controls.Add(this.msktxt_anchorFactor);
            this.grp_Factors.Controls.Add(this.msktxt_cover);
            this.grp_Factors.Controls.Add(this.lbl_anchorFactor);
            this.grp_Factors.Controls.Add(this.lbl_cover);
            this.grp_Factors.Location = new System.Drawing.Point(307, 48);
            this.grp_Factors.Name = "grp_Factors";
            this.grp_Factors.Size = new System.Drawing.Size(169, 66);
            this.grp_Factors.TabIndex = 2;
            this.grp_Factors.TabStop = false;
            // 
            // msktxt_anchorFactor
            // 
            this.msktxt_anchorFactor.Location = new System.Drawing.Point(127, 37);
            this.msktxt_anchorFactor.Name = "msktxt_anchorFactor";
            this.msktxt_anchorFactor.Size = new System.Drawing.Size(29, 20);
            this.msktxt_anchorFactor.TabIndex = 2;
            this.msktxt_anchorFactor.Text = "34";
            this.msktxt_anchorFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // msktxt_cover
            // 
            this.msktxt_cover.Location = new System.Drawing.Point(127, 13);
            this.msktxt_cover.Name = "msktxt_cover";
            this.msktxt_cover.Size = new System.Drawing.Size(29, 20);
            this.msktxt_cover.TabIndex = 1;
            this.msktxt_cover.Text = "5";
            this.msktxt_cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_anchorFactor
            // 
            this.lbl_anchorFactor.AutoSize = true;
            this.lbl_anchorFactor.Location = new System.Drawing.Point(6, 40);
            this.lbl_anchorFactor.Name = "lbl_anchorFactor";
            this.lbl_anchorFactor.Size = new System.Drawing.Size(103, 13);
            this.lbl_anchorFactor.TabIndex = 2;
            this.lbl_anchorFactor.Text = "Fator de Ancoragem";
            // 
            // lbl_cover
            // 
            this.lbl_cover.AutoSize = true;
            this.lbl_cover.Location = new System.Drawing.Point(6, 16);
            this.lbl_cover.Name = "lbl_cover";
            this.lbl_cover.Size = new System.Drawing.Size(60, 13);
            this.lbl_cover.TabIndex = 7;
            this.lbl_cover.Text = "Cobrimento";
            // 
            // grp_sideShapes
            // 
            this.grp_sideShapes.Controls.Add(this.lbl_horizontalEngaged);
            this.grp_sideShapes.Controls.Add(this.grpBx_horizontalConditions);
            this.grp_sideShapes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_sideShapes.Location = new System.Drawing.Point(15, 328);
            this.grp_sideShapes.Name = "grp_sideShapes";
            this.grp_sideShapes.Size = new System.Drawing.Size(286, 164);
            this.grp_sideShapes.TabIndex = 32;
            this.grp_sideShapes.TabStop = false;
            this.grp_sideShapes.Text = "Engaste";
            // 
            // lbl_horizontalEngaged
            // 
            this.lbl_horizontalEngaged.AutoSize = true;
            this.lbl_horizontalEngaged.Location = new System.Drawing.Point(6, 16);
            this.lbl_horizontalEngaged.Name = "lbl_horizontalEngaged";
            this.lbl_horizontalEngaged.Size = new System.Drawing.Size(280, 13);
            this.lbl_horizontalEngaged.TabIndex = 0;
            this.lbl_horizontalEngaged.Text = "Marque, nas áreas abaixo, as regiões em que há engaste;";
            // 
            // grpBx_horizontalConditions
            // 
            this.grpBx_horizontalConditions.Controls.Add(this.chcBx_PositiveRigthEngaged);
            this.grpBx_horizontalConditions.Controls.Add(this.chcBx_NegativaRightEngaged);
            this.grpBx_horizontalConditions.Controls.Add(this.lbl_horizontalPositiveEngagedConditions);
            this.grpBx_horizontalConditions.Controls.Add(this.lbl_horizontalNegativeEngagedConditions);
            this.grpBx_horizontalConditions.Controls.Add(this.chcBx_PositiveLeftEngaged);
            this.grpBx_horizontalConditions.Controls.Add(this.chcBx_NegativeLeftEngaged);
            this.grpBx_horizontalConditions.Location = new System.Drawing.Point(9, 98);
            this.grpBx_horizontalConditions.Name = "grpBx_horizontalConditions";
            this.grpBx_horizontalConditions.Size = new System.Drawing.Size(271, 60);
            this.grpBx_horizontalConditions.TabIndex = 1;
            this.grpBx_horizontalConditions.TabStop = false;
            this.grpBx_horizontalConditions.Text = "Em x:";
            // 
            // chcBx_PositiveRigthEngaged
            // 
            this.chcBx_PositiveRigthEngaged.AutoSize = true;
            this.chcBx_PositiveRigthEngaged.Checked = true;
            this.chcBx_PositiveRigthEngaged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chcBx_PositiveRigthEngaged.Location = new System.Drawing.Point(193, 20);
            this.chcBx_PositiveRigthEngaged.Name = "chcBx_PositiveRigthEngaged";
            this.chcBx_PositiveRigthEngaged.Size = new System.Drawing.Size(56, 17);
            this.chcBx_PositiveRigthEngaged.TabIndex = 12;
            this.chcBx_PositiveRigthEngaged.Text = "Direita";
            this.chcBx_PositiveRigthEngaged.UseVisualStyleBackColor = true;
            // 
            // chcBx_NegativaRightEngaged
            // 
            this.chcBx_NegativaRightEngaged.AutoSize = true;
            this.chcBx_NegativaRightEngaged.Checked = true;
            this.chcBx_NegativaRightEngaged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chcBx_NegativaRightEngaged.Location = new System.Drawing.Point(193, 39);
            this.chcBx_NegativaRightEngaged.Name = "chcBx_NegativaRightEngaged";
            this.chcBx_NegativaRightEngaged.Size = new System.Drawing.Size(56, 17);
            this.chcBx_NegativaRightEngaged.TabIndex = 14;
            this.chcBx_NegativaRightEngaged.Text = "Direita";
            this.chcBx_NegativaRightEngaged.UseVisualStyleBackColor = true;
            // 
            // lbl_horizontalPositiveEngagedConditions
            // 
            this.lbl_horizontalPositiveEngagedConditions.AutoSize = true;
            this.lbl_horizontalPositiveEngagedConditions.Location = new System.Drawing.Point(6, 20);
            this.lbl_horizontalPositiveEngagedConditions.Name = "lbl_horizontalPositiveEngagedConditions";
            this.lbl_horizontalPositiveEngagedConditions.Size = new System.Drawing.Size(33, 13);
            this.lbl_horizontalPositiveEngagedConditions.TabIndex = 5;
            this.lbl_horizontalPositiveEngagedConditions.Text = "Cima:";
            // 
            // lbl_horizontalNegativeEngagedConditions
            // 
            this.lbl_horizontalNegativeEngagedConditions.AutoSize = true;
            this.lbl_horizontalNegativeEngagedConditions.Location = new System.Drawing.Point(6, 39);
            this.lbl_horizontalNegativeEngagedConditions.Name = "lbl_horizontalNegativeEngagedConditions";
            this.lbl_horizontalNegativeEngagedConditions.Size = new System.Drawing.Size(36, 13);
            this.lbl_horizontalNegativeEngagedConditions.TabIndex = 6;
            this.lbl_horizontalNegativeEngagedConditions.Text = "Baixo:";
            // 
            // chcBx_PositiveLeftEngaged
            // 
            this.chcBx_PositiveLeftEngaged.AutoSize = true;
            this.chcBx_PositiveLeftEngaged.Checked = true;
            this.chcBx_PositiveLeftEngaged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chcBx_PositiveLeftEngaged.Location = new System.Drawing.Point(98, 20);
            this.chcBx_PositiveLeftEngaged.Name = "chcBx_PositiveLeftEngaged";
            this.chcBx_PositiveLeftEngaged.Size = new System.Drawing.Size(71, 17);
            this.chcBx_PositiveLeftEngaged.TabIndex = 11;
            this.chcBx_PositiveLeftEngaged.Text = "Esquerda";
            this.chcBx_PositiveLeftEngaged.UseVisualStyleBackColor = true;
            // 
            // chcBx_NegativeLeftEngaged
            // 
            this.chcBx_NegativeLeftEngaged.AutoSize = true;
            this.chcBx_NegativeLeftEngaged.Checked = true;
            this.chcBx_NegativeLeftEngaged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chcBx_NegativeLeftEngaged.Location = new System.Drawing.Point(98, 39);
            this.chcBx_NegativeLeftEngaged.Name = "chcBx_NegativeLeftEngaged";
            this.chcBx_NegativeLeftEngaged.Size = new System.Drawing.Size(71, 17);
            this.chcBx_NegativeLeftEngaged.TabIndex = 13;
            this.chcBx_NegativeLeftEngaged.Text = "Esquerda";
            this.chcBx_NegativeLeftEngaged.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(289, 274);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 33;
            this.pictureBox1.TabStop = false;
            // 
            // chcBx_VerticalUpLeft
            // 
            this.chcBx_VerticalUpLeft.AutoSize = true;
            this.chcBx_VerticalUpLeft.Checked = true;
            this.chcBx_VerticalUpLeft.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chcBx_VerticalUpLeft.Location = new System.Drawing.Point(249, 120);
            this.chcBx_VerticalUpLeft.Name = "chcBx_VerticalUpLeft";
            this.chcBx_VerticalUpLeft.Size = new System.Drawing.Size(15, 14);
            this.chcBx_VerticalUpLeft.TabIndex = 38;
            this.chcBx_VerticalUpLeft.UseVisualStyleBackColor = true;
            this.chcBx_VerticalUpLeft.CheckedChanged += new System.EventHandler(this.ChcBx_VerticalUpLeft_CheckedChanged);
            // 
            // chcBx_VerticalDownLeft
            // 
            this.chcBx_VerticalDownLeft.AutoSize = true;
            this.chcBx_VerticalDownLeft.Checked = true;
            this.chcBx_VerticalDownLeft.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chcBx_VerticalDownLeft.Location = new System.Drawing.Point(249, 284);
            this.chcBx_VerticalDownLeft.Name = "chcBx_VerticalDownLeft";
            this.chcBx_VerticalDownLeft.Size = new System.Drawing.Size(15, 14);
            this.chcBx_VerticalDownLeft.TabIndex = 39;
            this.chcBx_VerticalDownLeft.UseVisualStyleBackColor = true;
            this.chcBx_VerticalDownLeft.CheckedChanged += new System.EventHandler(this.ChcBx_VerticalDownLeft_CheckedChanged);
            // 
            // chcBx_VerticalDownRigth
            // 
            this.chcBx_VerticalDownRigth.AutoSize = true;
            this.chcBx_VerticalDownRigth.Checked = true;
            this.chcBx_VerticalDownRigth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chcBx_VerticalDownRigth.Location = new System.Drawing.Point(278, 285);
            this.chcBx_VerticalDownRigth.Name = "chcBx_VerticalDownRigth";
            this.chcBx_VerticalDownRigth.Size = new System.Drawing.Size(15, 14);
            this.chcBx_VerticalDownRigth.TabIndex = 41;
            this.chcBx_VerticalDownRigth.UseVisualStyleBackColor = true;
            this.chcBx_VerticalDownRigth.CheckedChanged += new System.EventHandler(this.ChcBx_VerticalDownRigth_CheckedChanged);
            // 
            // chcBx_VerticalUpRigth
            // 
            this.chcBx_VerticalUpRigth.AutoSize = true;
            this.chcBx_VerticalUpRigth.Checked = true;
            this.chcBx_VerticalUpRigth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chcBx_VerticalUpRigth.Location = new System.Drawing.Point(278, 120);
            this.chcBx_VerticalUpRigth.Name = "chcBx_VerticalUpRigth";
            this.chcBx_VerticalUpRigth.Size = new System.Drawing.Size(15, 14);
            this.chcBx_VerticalUpRigth.TabIndex = 40;
            this.chcBx_VerticalUpRigth.UseVisualStyleBackColor = true;
            this.chcBx_VerticalUpRigth.CheckedChanged += new System.EventHandler(this.ChcBx_VerticalUpRigth_CheckedChanged);
            // 
            // lbl_verticalPositiveEngagedConditions
            // 
            this.lbl_verticalPositiveEngagedConditions.AutoSize = true;
            this.lbl_verticalPositiveEngagedConditions.Location = new System.Drawing.Point(6, 23);
            this.lbl_verticalPositiveEngagedConditions.Name = "lbl_verticalPositiveEngagedConditions";
            this.lbl_verticalPositiveEngagedConditions.Size = new System.Drawing.Size(55, 13);
            this.lbl_verticalPositiveEngagedConditions.TabIndex = 3;
            this.lbl_verticalPositiveEngagedConditions.Text = "Esquerda:";
            // 
            // lbl_verticalNegativeEngagedConditions
            // 
            this.lbl_verticalNegativeEngagedConditions.AutoSize = true;
            this.lbl_verticalNegativeEngagedConditions.Location = new System.Drawing.Point(6, 43);
            this.lbl_verticalNegativeEngagedConditions.Name = "lbl_verticalNegativeEngagedConditions";
            this.lbl_verticalNegativeEngagedConditions.Size = new System.Drawing.Size(40, 13);
            this.lbl_verticalNegativeEngagedConditions.TabIndex = 4;
            this.lbl_verticalNegativeEngagedConditions.Text = "Direita:";
            // 
            // chcBx_PositiveUpEngaged
            // 
            this.chcBx_PositiveUpEngaged.AutoSize = true;
            this.chcBx_PositiveUpEngaged.Checked = true;
            this.chcBx_PositiveUpEngaged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chcBx_PositiveUpEngaged.Location = new System.Drawing.Point(98, 19);
            this.chcBx_PositiveUpEngaged.Name = "chcBx_PositiveUpEngaged";
            this.chcBx_PositiveUpEngaged.Size = new System.Drawing.Size(66, 17);
            this.chcBx_PositiveUpEngaged.TabIndex = 7;
            this.chcBx_PositiveUpEngaged.Text = "Em cima";
            this.chcBx_PositiveUpEngaged.UseVisualStyleBackColor = true;
            // 
            // chcBx_PositiveDownEngaged
            // 
            this.chcBx_PositiveDownEngaged.AutoSize = true;
            this.chcBx_PositiveDownEngaged.Checked = true;
            this.chcBx_PositiveDownEngaged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chcBx_PositiveDownEngaged.Location = new System.Drawing.Point(193, 19);
            this.chcBx_PositiveDownEngaged.Name = "chcBx_PositiveDownEngaged";
            this.chcBx_PositiveDownEngaged.Size = new System.Drawing.Size(66, 17);
            this.chcBx_PositiveDownEngaged.TabIndex = 8;
            this.chcBx_PositiveDownEngaged.Text = "Embaixo";
            this.chcBx_PositiveDownEngaged.UseVisualStyleBackColor = true;
            // 
            // chcBx_NegativeDownEngaged
            // 
            this.chcBx_NegativeDownEngaged.AutoSize = true;
            this.chcBx_NegativeDownEngaged.Checked = true;
            this.chcBx_NegativeDownEngaged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chcBx_NegativeDownEngaged.Location = new System.Drawing.Point(193, 43);
            this.chcBx_NegativeDownEngaged.Name = "chcBx_NegativeDownEngaged";
            this.chcBx_NegativeDownEngaged.Size = new System.Drawing.Size(66, 17);
            this.chcBx_NegativeDownEngaged.TabIndex = 10;
            this.chcBx_NegativeDownEngaged.Text = "Embaixo";
            this.chcBx_NegativeDownEngaged.UseVisualStyleBackColor = true;
            // 
            // chcBx_NegativeUpEngaged
            // 
            this.chcBx_NegativeUpEngaged.AutoSize = true;
            this.chcBx_NegativeUpEngaged.Checked = true;
            this.chcBx_NegativeUpEngaged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chcBx_NegativeUpEngaged.Location = new System.Drawing.Point(98, 43);
            this.chcBx_NegativeUpEngaged.Name = "chcBx_NegativeUpEngaged";
            this.chcBx_NegativeUpEngaged.Size = new System.Drawing.Size(66, 17);
            this.chcBx_NegativeUpEngaged.TabIndex = 9;
            this.chcBx_NegativeUpEngaged.Text = "Em cima";
            this.chcBx_NegativeUpEngaged.UseVisualStyleBackColor = true;
            // 
            // grpBx_verticalConditions
            // 
            this.grpBx_verticalConditions.Controls.Add(this.lbl_verticalPositiveEngagedConditions);
            this.grpBx_verticalConditions.Controls.Add(this.chcBx_NegativeDownEngaged);
            this.grpBx_verticalConditions.Controls.Add(this.lbl_verticalNegativeEngagedConditions);
            this.grpBx_verticalConditions.Controls.Add(this.chcBx_NegativeUpEngaged);
            this.grpBx_verticalConditions.Controls.Add(this.chcBx_PositiveUpEngaged);
            this.grpBx_verticalConditions.Controls.Add(this.chcBx_PositiveDownEngaged);
            this.grpBx_verticalConditions.Location = new System.Drawing.Point(24, 360);
            this.grpBx_verticalConditions.Name = "grpBx_verticalConditions";
            this.grpBx_verticalConditions.Size = new System.Drawing.Size(271, 66);
            this.grpBx_verticalConditions.TabIndex = 1;
            this.grpBx_verticalConditions.TabStop = false;
            this.grpBx_verticalConditions.Text = "Em y:";
            // 
            // CircularBottomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 496);
            this.Controls.Add(this.grpBx_verticalConditions);
            this.Controls.Add(this.chcBx_VerticalDownRigth);
            this.Controls.Add(this.chcBx_VerticalUpRigth);
            this.Controls.Add(this.chcBx_VerticalDownLeft);
            this.Controls.Add(this.chcBx_VerticalUpLeft);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.grp_sideShapes);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_detail);
            this.Controls.Add(this.rchtxt_title);
            this.Controls.Add(this.msktxt_multiplier);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.lbl_multiplier);
            this.Controls.Add(this.lbl_unitAlert);
            this.Controls.Add(this.grp_AdjacentElements);
            this.Controls.Add(this.grp_geometry);
            this.Controls.Add(this.grp_steel);
            this.Controls.Add(this.grp_Factors);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CircularBottomForm";
            this.Text = "Detalhador de Fundos Circulares";
            this.grp_AdjacentElements.ResumeLayout(false);
            this.grp_AdjacentElements.PerformLayout();
            this.grp_geometry.ResumeLayout(false);
            this.grp_geometry.PerformLayout();
            this.grp_steel.ResumeLayout(false);
            this.grp_steel.PerformLayout();
            this.grp_Factors.ResumeLayout(false);
            this.grp_Factors.PerformLayout();
            this.grp_sideShapes.ResumeLayout(false);
            this.grp_sideShapes.PerformLayout();
            this.grpBx_horizontalConditions.ResumeLayout(false);
            this.grpBx_horizontalConditions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpBx_verticalConditions.ResumeLayout(false);
            this.grpBx_verticalConditions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_detail;
        private System.Windows.Forms.RichTextBox rchtxt_title;
        private System.Windows.Forms.MaskedTextBox msktxt_multiplier;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Label lbl_multiplier;
        private System.Windows.Forms.Label lbl_unitAlert;
        private System.Windows.Forms.GroupBox grp_AdjacentElements;
        private System.Windows.Forms.MaskedTextBox msktxt_TopThickness;
        private System.Windows.Forms.Label lbl_elementTop;
        private System.Windows.Forms.GroupBox grp_geometry;
        private System.Windows.Forms.MaskedTextBox msktxt_bottomThickness;
        private System.Windows.Forms.MaskedTextBox msktxt_edgeLength;
        private System.Windows.Forms.MaskedTextBox msktxt_externalDiameter;
        private System.Windows.Forms.Label lbl_edgeLength;
        private System.Windows.Forms.Label lbl_bottomThickness;
        private System.Windows.Forms.Label lbl_externalDiameter;
        private System.Windows.Forms.GroupBox grp_steel;
        private System.Windows.Forms.ComboBox cmbbx_horizontalBottomGauge;
        private System.Windows.Forms.ComboBox cmbbx_verticalBottomGauge;
        private System.Windows.Forms.MaskedTextBox msktxt_horizontalSpacing;
        private System.Windows.Forms.MaskedTextBox msktxt_verticalSpacing;
        private System.Windows.Forms.Label lbl_horizontalSpacing;
        private System.Windows.Forms.Label lbl_horizontalGauge;
        private System.Windows.Forms.Label lbl_verticalSpacing;
        private System.Windows.Forms.Label lbl_verticalGauge;
        private System.Windows.Forms.GroupBox grp_Factors;
        private System.Windows.Forms.MaskedTextBox msktxt_anchorFactor;
        private System.Windows.Forms.MaskedTextBox msktxt_cover;
        private System.Windows.Forms.Label lbl_anchorFactor;
        private System.Windows.Forms.Label lbl_cover;
        private System.Windows.Forms.GroupBox grp_sideShapes;
        private System.Windows.Forms.Label lbl_horizontalEngaged;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox grpBx_horizontalConditions;
        private System.Windows.Forms.CheckBox chcBx_PositiveRigthEngaged;
        private System.Windows.Forms.CheckBox chcBx_NegativaRightEngaged;
        private System.Windows.Forms.Label lbl_horizontalPositiveEngagedConditions;
        private System.Windows.Forms.Label lbl_horizontalNegativeEngagedConditions;
        private System.Windows.Forms.CheckBox chcBx_PositiveLeftEngaged;
        private System.Windows.Forms.CheckBox chcBx_NegativeLeftEngaged;
        private System.Windows.Forms.CheckBox chcBx_VerticalUpLeft;
        private System.Windows.Forms.CheckBox chcBx_VerticalDownLeft;
        private System.Windows.Forms.CheckBox chcBx_VerticalDownRigth;
        private System.Windows.Forms.CheckBox chcBx_VerticalUpRigth;
        private System.Windows.Forms.Label lbl_verticalPositiveEngagedConditions;
        private System.Windows.Forms.Label lbl_verticalNegativeEngagedConditions;
        private System.Windows.Forms.CheckBox chcBx_PositiveUpEngaged;
        private System.Windows.Forms.CheckBox chcBx_PositiveDownEngaged;
        private System.Windows.Forms.CheckBox chcBx_NegativeDownEngaged;
        private System.Windows.Forms.CheckBox chcBx_NegativeUpEngaged;
        private System.Windows.Forms.GroupBox grpBx_verticalConditions;
        private System.Windows.Forms.MaskedTextBox msktxt_InfThickness;
        private System.Windows.Forms.Label lbl_elementInf;
    }
}