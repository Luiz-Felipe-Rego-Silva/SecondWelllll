
namespace Hello_World.UI
{
    partial class SteelForm
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
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.grpBox_TopX = new System.Windows.Forms.GroupBox();
            this.lbl_TopGaugeUnitX = new System.Windows.Forms.Label();
            this.cmbBox_TopGaugeX = new System.Windows.Forms.ComboBox();
            this.msktxt_TopSpacingX = new System.Windows.Forms.MaskedTextBox();
            this.lbl_TopSpacingX = new System.Windows.Forms.Label();
            this.lbl_topGaugeX = new System.Windows.Forms.Label();
            this.grpBox_TopY = new System.Windows.Forms.GroupBox();
            this.lbl_TopGaugeUnitY = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.msktxt_TopSpacingY = new System.Windows.Forms.MaskedTextBox();
            this.lbl_TopSpacingY = new System.Windows.Forms.Label();
            this.lbl_TopGaugeY = new System.Windows.Forms.Label();
            this.grpBox_Top = new System.Windows.Forms.GroupBox();
            this.grpBox_Wall = new System.Windows.Forms.GroupBox();
            this.grpBox_WallX = new System.Windows.Forms.GroupBox();
            this.lbl_WallGaugeUnitX = new System.Windows.Forms.Label();
            this.cmbBox_WallGaugeX = new System.Windows.Forms.ComboBox();
            this.msktxt_WallSpacingX = new System.Windows.Forms.MaskedTextBox();
            this.lbl_WallSpacingX = new System.Windows.Forms.Label();
            this.lbl_wallGaugeX = new System.Windows.Forms.Label();
            this.grpBox_WallY = new System.Windows.Forms.GroupBox();
            this.lbl_WallGaugeUnitY = new System.Windows.Forms.Label();
            this.cmbBox_WallGaugeY = new System.Windows.Forms.ComboBox();
            this.msktxt_WallSpacingY = new System.Windows.Forms.MaskedTextBox();
            this.lbl_WallSpacingY = new System.Windows.Forms.Label();
            this.lbl_wallGaugeY = new System.Windows.Forms.Label();
            this.grpBox_Bottom = new System.Windows.Forms.GroupBox();
            this.grpBox_BottomX = new System.Windows.Forms.GroupBox();
            this.lbl_BottomGaugeUnitX = new System.Windows.Forms.Label();
            this.cmbBox_BottomGaugeX = new System.Windows.Forms.ComboBox();
            this.msktxt_BottomSpacingX = new System.Windows.Forms.MaskedTextBox();
            this.lbl_BottomSpacingX = new System.Windows.Forms.Label();
            this.lbl_bottomGaugeX = new System.Windows.Forms.Label();
            this.grpBox_BottomY = new System.Windows.Forms.GroupBox();
            this.lbl_BottomGaugeUnitY = new System.Windows.Forms.Label();
            this.cmbBox_BottomGaugeY = new System.Windows.Forms.ComboBox();
            this.msktxt_BottomSpacingY = new System.Windows.Forms.MaskedTextBox();
            this.lbl_BottomSpacingY = new System.Windows.Forms.Label();
            this.lbl_bottomGaugeY = new System.Windows.Forms.Label();
            this.btn_manual = new System.Windows.Forms.Button();
            this.chc_isTopArmed = new System.Windows.Forms.CheckBox();
            this.msktxt_cover = new System.Windows.Forms.MaskedTextBox();
            this.lbl_cover = new System.Windows.Forms.Label();
            this.lbl_gaugeFactor = new System.Windows.Forms.Label();
            this.msktxt_gaugeFactor = new System.Windows.Forms.MaskedTextBox();
            this.grpBox_TopX.SuspendLayout();
            this.grpBox_TopY.SuspendLayout();
            this.grpBox_Top.SuspendLayout();
            this.grpBox_Wall.SuspendLayout();
            this.grpBox_WallX.SuspendLayout();
            this.grpBox_WallY.SuspendLayout();
            this.grpBox_Bottom.SuspendLayout();
            this.grpBox_BottomX.SuspendLayout();
            this.grpBox_BottomY.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            this.btn_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_save.Location = new System.Drawing.Point(583, 373);
            this.btn_save.Margin = new System.Windows.Forms.Padding(4);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(112, 34);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "Salvar";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.Location = new System.Drawing.Point(583, 331);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(112, 34);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "Cancelar";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // grpBox_TopX
            // 
            this.grpBox_TopX.Controls.Add(this.lbl_TopGaugeUnitX);
            this.grpBox_TopX.Controls.Add(this.cmbBox_TopGaugeX);
            this.grpBox_TopX.Controls.Add(this.msktxt_TopSpacingX);
            this.grpBox_TopX.Controls.Add(this.lbl_TopSpacingX);
            this.grpBox_TopX.Controls.Add(this.lbl_topGaugeX);
            this.grpBox_TopX.Location = new System.Drawing.Point(7, 26);
            this.grpBox_TopX.Margin = new System.Windows.Forms.Padding(4);
            this.grpBox_TopX.Name = "grpBox_TopX";
            this.grpBox_TopX.Padding = new System.Windows.Forms.Padding(4);
            this.grpBox_TopX.Size = new System.Drawing.Size(263, 84);
            this.grpBox_TopX.TabIndex = 2;
            this.grpBox_TopX.TabStop = false;
            this.grpBox_TopX.Text = "Horizontal";
            // 
            // lbl_TopGaugeUnitX
            // 
            this.lbl_TopGaugeUnitX.AutoSize = true;
            this.lbl_TopGaugeUnitX.Location = new System.Drawing.Point(88, 24);
            this.lbl_TopGaugeUnitX.Name = "lbl_TopGaugeUnitX";
            this.lbl_TopGaugeUnitX.Size = new System.Drawing.Size(41, 19);
            this.lbl_TopGaugeUnitX.TabIndex = 4;
            this.lbl_TopGaugeUnitX.Text = "(mm)";
            // 
            // cmbBox_TopGaugeX
            // 
            this.cmbBox_TopGaugeX.DropDownWidth = 52;
            this.cmbBox_TopGaugeX.FormattingEnabled = true;
            this.cmbBox_TopGaugeX.ItemHeight = 19;
            this.cmbBox_TopGaugeX.Items.AddRange(new object[] {
            "5.0",
            "6.3",
            "8.0",
            "10.0",
            "12.5",
            "16.0",
            "20.0",
            "25.0",
            "32.0"});
            this.cmbBox_TopGaugeX.Location = new System.Drawing.Point(139, 21);
            this.cmbBox_TopGaugeX.Name = "cmbBox_TopGaugeX";
            this.cmbBox_TopGaugeX.Size = new System.Drawing.Size(100, 27);
            this.cmbBox_TopGaugeX.TabIndex = 3;
            this.cmbBox_TopGaugeX.Text = "5.0";
            // 
            // msktxt_TopSpacingX
            // 
            this.msktxt_TopSpacingX.Location = new System.Drawing.Point(139, 51);
            this.msktxt_TopSpacingX.Name = "msktxt_TopSpacingX";
            this.msktxt_TopSpacingX.Size = new System.Drawing.Size(100, 26);
            this.msktxt_TopSpacingX.TabIndex = 2;
            // 
            // lbl_TopSpacingX
            // 
            this.lbl_TopSpacingX.AutoSize = true;
            this.lbl_TopSpacingX.Location = new System.Drawing.Point(10, 54);
            this.lbl_TopSpacingX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_TopSpacingX.Name = "lbl_TopSpacingX";
            this.lbl_TopSpacingX.Size = new System.Drawing.Size(122, 19);
            this.lbl_TopSpacingX.TabIndex = 1;
            this.lbl_TopSpacingX.Text = "Espaçamento (cm)";
            // 
            // lbl_topGaugeX
            // 
            this.lbl_topGaugeX.AutoSize = true;
            this.lbl_topGaugeX.Font = new System.Drawing.Font("Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl_topGaugeX.Location = new System.Drawing.Point(63, 23);
            this.lbl_topGaugeX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_topGaugeX.Name = "lbl_topGaugeX";
            this.lbl_topGaugeX.Size = new System.Drawing.Size(18, 20);
            this.lbl_topGaugeX.TabIndex = 0;
            this.lbl_topGaugeX.Text = "f";
            // 
            // grpBox_TopY
            // 
            this.grpBox_TopY.Controls.Add(this.lbl_TopGaugeUnitY);
            this.grpBox_TopY.Controls.Add(this.comboBox1);
            this.grpBox_TopY.Controls.Add(this.msktxt_TopSpacingY);
            this.grpBox_TopY.Controls.Add(this.lbl_TopSpacingY);
            this.grpBox_TopY.Controls.Add(this.lbl_TopGaugeY);
            this.grpBox_TopY.Location = new System.Drawing.Point(278, 26);
            this.grpBox_TopY.Margin = new System.Windows.Forms.Padding(4);
            this.grpBox_TopY.Name = "grpBox_TopY";
            this.grpBox_TopY.Padding = new System.Windows.Forms.Padding(4);
            this.grpBox_TopY.Size = new System.Drawing.Size(263, 84);
            this.grpBox_TopY.TabIndex = 5;
            this.grpBox_TopY.TabStop = false;
            this.grpBox_TopY.Text = "Vertical";
            // 
            // lbl_TopGaugeUnitY
            // 
            this.lbl_TopGaugeUnitY.AutoSize = true;
            this.lbl_TopGaugeUnitY.Location = new System.Drawing.Point(88, 24);
            this.lbl_TopGaugeUnitY.Name = "lbl_TopGaugeUnitY";
            this.lbl_TopGaugeUnitY.Size = new System.Drawing.Size(41, 19);
            this.lbl_TopGaugeUnitY.TabIndex = 4;
            this.lbl_TopGaugeUnitY.Text = "(mm)";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownWidth = 52;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ItemHeight = 19;
            this.comboBox1.Items.AddRange(new object[] {
            "5.0",
            "6.3",
            "8.0",
            "10.0",
            "12.5",
            "16.0",
            "20.0",
            "25.0",
            "32.0"});
            this.comboBox1.Location = new System.Drawing.Point(139, 21);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 27);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.Text = "5.0";
            // 
            // msktxt_TopSpacingY
            // 
            this.msktxt_TopSpacingY.Location = new System.Drawing.Point(139, 51);
            this.msktxt_TopSpacingY.Name = "msktxt_TopSpacingY";
            this.msktxt_TopSpacingY.Size = new System.Drawing.Size(100, 26);
            this.msktxt_TopSpacingY.TabIndex = 2;
            // 
            // lbl_TopSpacingY
            // 
            this.lbl_TopSpacingY.AutoSize = true;
            this.lbl_TopSpacingY.Location = new System.Drawing.Point(10, 54);
            this.lbl_TopSpacingY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_TopSpacingY.Name = "lbl_TopSpacingY";
            this.lbl_TopSpacingY.Size = new System.Drawing.Size(122, 19);
            this.lbl_TopSpacingY.TabIndex = 1;
            this.lbl_TopSpacingY.Text = "Espaçamento (cm)";
            // 
            // lbl_TopGaugeY
            // 
            this.lbl_TopGaugeY.AutoSize = true;
            this.lbl_TopGaugeY.Font = new System.Drawing.Font("Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl_TopGaugeY.Location = new System.Drawing.Point(63, 23);
            this.lbl_TopGaugeY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_TopGaugeY.Name = "lbl_TopGaugeY";
            this.lbl_TopGaugeY.Size = new System.Drawing.Size(18, 20);
            this.lbl_TopGaugeY.TabIndex = 0;
            this.lbl_TopGaugeY.Text = "f";
            // 
            // grpBox_Top
            // 
            this.grpBox_Top.Controls.Add(this.grpBox_TopX);
            this.grpBox_Top.Controls.Add(this.grpBox_TopY);
            this.grpBox_Top.Location = new System.Drawing.Point(12, 12);
            this.grpBox_Top.Name = "grpBox_Top";
            this.grpBox_Top.Size = new System.Drawing.Size(553, 128);
            this.grpBox_Top.TabIndex = 6;
            this.grpBox_Top.TabStop = false;
            this.grpBox_Top.Text = "Laje";
            // 
            // grpBox_Wall
            // 
            this.grpBox_Wall.Controls.Add(this.grpBox_WallX);
            this.grpBox_Wall.Controls.Add(this.grpBox_WallY);
            this.grpBox_Wall.Location = new System.Drawing.Point(12, 146);
            this.grpBox_Wall.Name = "grpBox_Wall";
            this.grpBox_Wall.Size = new System.Drawing.Size(553, 128);
            this.grpBox_Wall.TabIndex = 7;
            this.grpBox_Wall.TabStop = false;
            this.grpBox_Wall.Text = "Parede";
            this.grpBox_Wall.Enter += new System.EventHandler(this.grpBox_Wall_Enter);
            // 
            // grpBox_WallX
            // 
            this.grpBox_WallX.Controls.Add(this.lbl_WallGaugeUnitX);
            this.grpBox_WallX.Controls.Add(this.cmbBox_WallGaugeX);
            this.grpBox_WallX.Controls.Add(this.msktxt_WallSpacingX);
            this.grpBox_WallX.Controls.Add(this.lbl_WallSpacingX);
            this.grpBox_WallX.Controls.Add(this.lbl_wallGaugeX);
            this.grpBox_WallX.Location = new System.Drawing.Point(7, 26);
            this.grpBox_WallX.Margin = new System.Windows.Forms.Padding(4);
            this.grpBox_WallX.Name = "grpBox_WallX";
            this.grpBox_WallX.Padding = new System.Windows.Forms.Padding(4);
            this.grpBox_WallX.Size = new System.Drawing.Size(263, 84);
            this.grpBox_WallX.TabIndex = 2;
            this.grpBox_WallX.TabStop = false;
            this.grpBox_WallX.Text = "Horizontal";
            // 
            // lbl_WallGaugeUnitX
            // 
            this.lbl_WallGaugeUnitX.AutoSize = true;
            this.lbl_WallGaugeUnitX.Location = new System.Drawing.Point(88, 24);
            this.lbl_WallGaugeUnitX.Name = "lbl_WallGaugeUnitX";
            this.lbl_WallGaugeUnitX.Size = new System.Drawing.Size(41, 19);
            this.lbl_WallGaugeUnitX.TabIndex = 4;
            this.lbl_WallGaugeUnitX.Text = "(mm)";
            // 
            // cmbBox_WallGaugeX
            // 
            this.cmbBox_WallGaugeX.DropDownWidth = 52;
            this.cmbBox_WallGaugeX.FormattingEnabled = true;
            this.cmbBox_WallGaugeX.ItemHeight = 19;
            this.cmbBox_WallGaugeX.Items.AddRange(new object[] {
            "5.0",
            "6.3",
            "8.0",
            "10.0",
            "12.5",
            "16.0",
            "20.0",
            "25.0",
            "32.0"});
            this.cmbBox_WallGaugeX.Location = new System.Drawing.Point(139, 21);
            this.cmbBox_WallGaugeX.Name = "cmbBox_WallGaugeX";
            this.cmbBox_WallGaugeX.Size = new System.Drawing.Size(100, 27);
            this.cmbBox_WallGaugeX.TabIndex = 3;
            this.cmbBox_WallGaugeX.Text = "5.0";
            // 
            // msktxt_WallSpacingX
            // 
            this.msktxt_WallSpacingX.Location = new System.Drawing.Point(139, 51);
            this.msktxt_WallSpacingX.Name = "msktxt_WallSpacingX";
            this.msktxt_WallSpacingX.Size = new System.Drawing.Size(100, 26);
            this.msktxt_WallSpacingX.TabIndex = 2;
            // 
            // lbl_WallSpacingX
            // 
            this.lbl_WallSpacingX.AutoSize = true;
            this.lbl_WallSpacingX.Location = new System.Drawing.Point(10, 54);
            this.lbl_WallSpacingX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_WallSpacingX.Name = "lbl_WallSpacingX";
            this.lbl_WallSpacingX.Size = new System.Drawing.Size(122, 19);
            this.lbl_WallSpacingX.TabIndex = 1;
            this.lbl_WallSpacingX.Text = "Espaçamento (cm)";
            // 
            // lbl_wallGaugeX
            // 
            this.lbl_wallGaugeX.AutoSize = true;
            this.lbl_wallGaugeX.Font = new System.Drawing.Font("Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl_wallGaugeX.Location = new System.Drawing.Point(63, 23);
            this.lbl_wallGaugeX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_wallGaugeX.Name = "lbl_wallGaugeX";
            this.lbl_wallGaugeX.Size = new System.Drawing.Size(18, 20);
            this.lbl_wallGaugeX.TabIndex = 0;
            this.lbl_wallGaugeX.Text = "f";
            // 
            // grpBox_WallY
            // 
            this.grpBox_WallY.Controls.Add(this.lbl_WallGaugeUnitY);
            this.grpBox_WallY.Controls.Add(this.cmbBox_WallGaugeY);
            this.grpBox_WallY.Controls.Add(this.msktxt_WallSpacingY);
            this.grpBox_WallY.Controls.Add(this.lbl_WallSpacingY);
            this.grpBox_WallY.Controls.Add(this.lbl_wallGaugeY);
            this.grpBox_WallY.Location = new System.Drawing.Point(278, 26);
            this.grpBox_WallY.Margin = new System.Windows.Forms.Padding(4);
            this.grpBox_WallY.Name = "grpBox_WallY";
            this.grpBox_WallY.Padding = new System.Windows.Forms.Padding(4);
            this.grpBox_WallY.Size = new System.Drawing.Size(263, 84);
            this.grpBox_WallY.TabIndex = 5;
            this.grpBox_WallY.TabStop = false;
            this.grpBox_WallY.Text = "Vertical";
            // 
            // lbl_WallGaugeUnitY
            // 
            this.lbl_WallGaugeUnitY.AutoSize = true;
            this.lbl_WallGaugeUnitY.Location = new System.Drawing.Point(88, 24);
            this.lbl_WallGaugeUnitY.Name = "lbl_WallGaugeUnitY";
            this.lbl_WallGaugeUnitY.Size = new System.Drawing.Size(41, 19);
            this.lbl_WallGaugeUnitY.TabIndex = 4;
            this.lbl_WallGaugeUnitY.Text = "(mm)";
            // 
            // cmbBox_WallGaugeY
            // 
            this.cmbBox_WallGaugeY.DropDownWidth = 52;
            this.cmbBox_WallGaugeY.FormattingEnabled = true;
            this.cmbBox_WallGaugeY.ItemHeight = 19;
            this.cmbBox_WallGaugeY.Items.AddRange(new object[] {
            "5.0",
            "6.3",
            "8.0",
            "10.0",
            "12.5",
            "16.0",
            "20.0",
            "25.0",
            "32.0"});
            this.cmbBox_WallGaugeY.Location = new System.Drawing.Point(139, 21);
            this.cmbBox_WallGaugeY.Name = "cmbBox_WallGaugeY";
            this.cmbBox_WallGaugeY.Size = new System.Drawing.Size(100, 27);
            this.cmbBox_WallGaugeY.TabIndex = 3;
            this.cmbBox_WallGaugeY.Text = "5.0";
            // 
            // msktxt_WallSpacingY
            // 
            this.msktxt_WallSpacingY.Location = new System.Drawing.Point(139, 51);
            this.msktxt_WallSpacingY.Name = "msktxt_WallSpacingY";
            this.msktxt_WallSpacingY.Size = new System.Drawing.Size(100, 26);
            this.msktxt_WallSpacingY.TabIndex = 2;
            // 
            // lbl_WallSpacingY
            // 
            this.lbl_WallSpacingY.AutoSize = true;
            this.lbl_WallSpacingY.Location = new System.Drawing.Point(10, 54);
            this.lbl_WallSpacingY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_WallSpacingY.Name = "lbl_WallSpacingY";
            this.lbl_WallSpacingY.Size = new System.Drawing.Size(122, 19);
            this.lbl_WallSpacingY.TabIndex = 1;
            this.lbl_WallSpacingY.Text = "Espaçamento (cm)";
            // 
            // lbl_wallGaugeY
            // 
            this.lbl_wallGaugeY.AutoSize = true;
            this.lbl_wallGaugeY.Font = new System.Drawing.Font("Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl_wallGaugeY.Location = new System.Drawing.Point(62, 23);
            this.lbl_wallGaugeY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_wallGaugeY.Name = "lbl_wallGaugeY";
            this.lbl_wallGaugeY.Size = new System.Drawing.Size(18, 20);
            this.lbl_wallGaugeY.TabIndex = 0;
            this.lbl_wallGaugeY.Text = "f";
            // 
            // grpBox_Bottom
            // 
            this.grpBox_Bottom.Controls.Add(this.grpBox_BottomX);
            this.grpBox_Bottom.Controls.Add(this.grpBox_BottomY);
            this.grpBox_Bottom.Location = new System.Drawing.Point(12, 280);
            this.grpBox_Bottom.Name = "grpBox_Bottom";
            this.grpBox_Bottom.Size = new System.Drawing.Size(553, 128);
            this.grpBox_Bottom.TabIndex = 8;
            this.grpBox_Bottom.TabStop = false;
            this.grpBox_Bottom.Text = "Fundo";
            // 
            // grpBox_BottomX
            // 
            this.grpBox_BottomX.Controls.Add(this.lbl_BottomGaugeUnitX);
            this.grpBox_BottomX.Controls.Add(this.cmbBox_BottomGaugeX);
            this.grpBox_BottomX.Controls.Add(this.msktxt_BottomSpacingX);
            this.grpBox_BottomX.Controls.Add(this.lbl_BottomSpacingX);
            this.grpBox_BottomX.Controls.Add(this.lbl_bottomGaugeX);
            this.grpBox_BottomX.Location = new System.Drawing.Point(7, 26);
            this.grpBox_BottomX.Margin = new System.Windows.Forms.Padding(4);
            this.grpBox_BottomX.Name = "grpBox_BottomX";
            this.grpBox_BottomX.Padding = new System.Windows.Forms.Padding(4);
            this.grpBox_BottomX.Size = new System.Drawing.Size(263, 84);
            this.grpBox_BottomX.TabIndex = 2;
            this.grpBox_BottomX.TabStop = false;
            this.grpBox_BottomX.Text = "Horizontal";
            // 
            // lbl_BottomGaugeUnitX
            // 
            this.lbl_BottomGaugeUnitX.AutoSize = true;
            this.lbl_BottomGaugeUnitX.Location = new System.Drawing.Point(88, 24);
            this.lbl_BottomGaugeUnitX.Name = "lbl_BottomGaugeUnitX";
            this.lbl_BottomGaugeUnitX.Size = new System.Drawing.Size(41, 19);
            this.lbl_BottomGaugeUnitX.TabIndex = 4;
            this.lbl_BottomGaugeUnitX.Text = "(mm)";
            // 
            // cmbBox_BottomGaugeX
            // 
            this.cmbBox_BottomGaugeX.DropDownWidth = 52;
            this.cmbBox_BottomGaugeX.FormattingEnabled = true;
            this.cmbBox_BottomGaugeX.ItemHeight = 19;
            this.cmbBox_BottomGaugeX.Items.AddRange(new object[] {
            "5.0",
            "6.3",
            "8.0",
            "10.0",
            "12.5",
            "16.0",
            "20.0",
            "25.0",
            "32.0"});
            this.cmbBox_BottomGaugeX.Location = new System.Drawing.Point(139, 21);
            this.cmbBox_BottomGaugeX.Name = "cmbBox_BottomGaugeX";
            this.cmbBox_BottomGaugeX.Size = new System.Drawing.Size(100, 27);
            this.cmbBox_BottomGaugeX.TabIndex = 3;
            this.cmbBox_BottomGaugeX.Text = "5.0";
            // 
            // msktxt_BottomSpacingX
            // 
            this.msktxt_BottomSpacingX.Location = new System.Drawing.Point(139, 51);
            this.msktxt_BottomSpacingX.Name = "msktxt_BottomSpacingX";
            this.msktxt_BottomSpacingX.Size = new System.Drawing.Size(100, 26);
            this.msktxt_BottomSpacingX.TabIndex = 2;
            // 
            // lbl_BottomSpacingX
            // 
            this.lbl_BottomSpacingX.AutoSize = true;
            this.lbl_BottomSpacingX.Location = new System.Drawing.Point(10, 54);
            this.lbl_BottomSpacingX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_BottomSpacingX.Name = "lbl_BottomSpacingX";
            this.lbl_BottomSpacingX.Size = new System.Drawing.Size(122, 19);
            this.lbl_BottomSpacingX.TabIndex = 1;
            this.lbl_BottomSpacingX.Text = "Espaçamento (cm)";
            // 
            // lbl_bottomGaugeX
            // 
            this.lbl_bottomGaugeX.AutoSize = true;
            this.lbl_bottomGaugeX.Font = new System.Drawing.Font("Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl_bottomGaugeX.Location = new System.Drawing.Point(63, 23);
            this.lbl_bottomGaugeX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_bottomGaugeX.Name = "lbl_bottomGaugeX";
            this.lbl_bottomGaugeX.Size = new System.Drawing.Size(18, 20);
            this.lbl_bottomGaugeX.TabIndex = 0;
            this.lbl_bottomGaugeX.Text = "f";
            // 
            // grpBox_BottomY
            // 
            this.grpBox_BottomY.Controls.Add(this.lbl_BottomGaugeUnitY);
            this.grpBox_BottomY.Controls.Add(this.cmbBox_BottomGaugeY);
            this.grpBox_BottomY.Controls.Add(this.msktxt_BottomSpacingY);
            this.grpBox_BottomY.Controls.Add(this.lbl_BottomSpacingY);
            this.grpBox_BottomY.Controls.Add(this.lbl_bottomGaugeY);
            this.grpBox_BottomY.Location = new System.Drawing.Point(278, 26);
            this.grpBox_BottomY.Margin = new System.Windows.Forms.Padding(4);
            this.grpBox_BottomY.Name = "grpBox_BottomY";
            this.grpBox_BottomY.Padding = new System.Windows.Forms.Padding(4);
            this.grpBox_BottomY.Size = new System.Drawing.Size(263, 84);
            this.grpBox_BottomY.TabIndex = 5;
            this.grpBox_BottomY.TabStop = false;
            this.grpBox_BottomY.Text = "Vertical";
            // 
            // lbl_BottomGaugeUnitY
            // 
            this.lbl_BottomGaugeUnitY.AutoSize = true;
            this.lbl_BottomGaugeUnitY.Location = new System.Drawing.Point(88, 24);
            this.lbl_BottomGaugeUnitY.Name = "lbl_BottomGaugeUnitY";
            this.lbl_BottomGaugeUnitY.Size = new System.Drawing.Size(41, 19);
            this.lbl_BottomGaugeUnitY.TabIndex = 4;
            this.lbl_BottomGaugeUnitY.Text = "(mm)";
            // 
            // cmbBox_BottomGaugeY
            // 
            this.cmbBox_BottomGaugeY.DropDownWidth = 52;
            this.cmbBox_BottomGaugeY.FormattingEnabled = true;
            this.cmbBox_BottomGaugeY.ItemHeight = 19;
            this.cmbBox_BottomGaugeY.Items.AddRange(new object[] {
            "5.0",
            "6.3",
            "8.0",
            "10.0",
            "12.5",
            "16.0",
            "20.0",
            "25.0",
            "32.0"});
            this.cmbBox_BottomGaugeY.Location = new System.Drawing.Point(139, 21);
            this.cmbBox_BottomGaugeY.Name = "cmbBox_BottomGaugeY";
            this.cmbBox_BottomGaugeY.Size = new System.Drawing.Size(100, 27);
            this.cmbBox_BottomGaugeY.TabIndex = 3;
            this.cmbBox_BottomGaugeY.Text = "5.0";
            // 
            // msktxt_BottomSpacingY
            // 
            this.msktxt_BottomSpacingY.Location = new System.Drawing.Point(139, 51);
            this.msktxt_BottomSpacingY.Name = "msktxt_BottomSpacingY";
            this.msktxt_BottomSpacingY.Size = new System.Drawing.Size(100, 26);
            this.msktxt_BottomSpacingY.TabIndex = 2;
            // 
            // lbl_BottomSpacingY
            // 
            this.lbl_BottomSpacingY.AutoSize = true;
            this.lbl_BottomSpacingY.Location = new System.Drawing.Point(10, 54);
            this.lbl_BottomSpacingY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_BottomSpacingY.Name = "lbl_BottomSpacingY";
            this.lbl_BottomSpacingY.Size = new System.Drawing.Size(122, 19);
            this.lbl_BottomSpacingY.TabIndex = 1;
            this.lbl_BottomSpacingY.Text = "Espaçamento (cm)";
            // 
            // lbl_bottomGaugeY
            // 
            this.lbl_bottomGaugeY.AutoSize = true;
            this.lbl_bottomGaugeY.Font = new System.Drawing.Font("Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lbl_bottomGaugeY.Location = new System.Drawing.Point(63, 23);
            this.lbl_bottomGaugeY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_bottomGaugeY.Name = "lbl_bottomGaugeY";
            this.lbl_bottomGaugeY.Size = new System.Drawing.Size(18, 20);
            this.lbl_bottomGaugeY.TabIndex = 0;
            this.lbl_bottomGaugeY.Text = "f";
            // 
            // btn_manual
            // 
            this.btn_manual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_manual.ForeColor = System.Drawing.Color.ForestGreen;
            this.btn_manual.Location = new System.Drawing.Point(583, 266);
            this.btn_manual.Margin = new System.Windows.Forms.Padding(4);
            this.btn_manual.Name = "btn_manual";
            this.btn_manual.Size = new System.Drawing.Size(112, 57);
            this.btn_manual.TabIndex = 10;
            this.btn_manual.Text = "Vinculações Manuais";
            this.btn_manual.UseVisualStyleBackColor = true;
            // 
            // chc_isTopArmed
            // 
            this.chc_isTopArmed.AutoSize = true;
            this.chc_isTopArmed.Location = new System.Drawing.Point(571, 23);
            this.chc_isTopArmed.Name = "chc_isTopArmed";
            this.chc_isTopArmed.Size = new System.Drawing.Size(107, 23);
            this.chc_isTopArmed.TabIndex = 11;
            this.chc_isTopArmed.Text = "Laje Armada";
            this.chc_isTopArmed.UseVisualStyleBackColor = true;
            // 
            // msktxt_cover
            // 
            this.msktxt_cover.Location = new System.Drawing.Point(575, 89);
            this.msktxt_cover.Name = "msktxt_cover";
            this.msktxt_cover.Size = new System.Drawing.Size(100, 26);
            this.msktxt_cover.TabIndex = 12;
            this.msktxt_cover.Text = "5";
            this.msktxt_cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_cover
            // 
            this.lbl_cover.AutoSize = true;
            this.lbl_cover.Location = new System.Drawing.Point(571, 67);
            this.lbl_cover.Name = "lbl_cover";
            this.lbl_cover.Size = new System.Drawing.Size(113, 19);
            this.lbl_cover.TabIndex = 14;
            this.lbl_cover.Text = "Cobrimento (cm)";
            // 
            // lbl_gaugeFactor
            // 
            this.lbl_gaugeFactor.AutoSize = true;
            this.lbl_gaugeFactor.Location = new System.Drawing.Point(573, 132);
            this.lbl_gaugeFactor.Name = "lbl_gaugeFactor";
            this.lbl_gaugeFactor.Size = new System.Drawing.Size(134, 19);
            this.lbl_gaugeFactor.TabIndex = 15;
            this.lbl_gaugeFactor.Text = "Fator de Ancoragem";
            // 
            // msktxt_gaugeFactor
            // 
            this.msktxt_gaugeFactor.Location = new System.Drawing.Point(577, 155);
            this.msktxt_gaugeFactor.Name = "msktxt_gaugeFactor";
            this.msktxt_gaugeFactor.Size = new System.Drawing.Size(100, 26);
            this.msktxt_gaugeFactor.TabIndex = 16;
            this.msktxt_gaugeFactor.Text = "34";
            this.msktxt_gaugeFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SteelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 420);
            this.Controls.Add(this.msktxt_gaugeFactor);
            this.Controls.Add(this.lbl_gaugeFactor);
            this.Controls.Add(this.lbl_cover);
            this.Controls.Add(this.msktxt_cover);
            this.Controls.Add(this.chc_isTopArmed);
            this.Controls.Add(this.btn_manual);
            this.Controls.Add(this.grpBox_Bottom);
            this.Controls.Add(this.grpBox_Wall);
            this.Controls.Add(this.grpBox_Top);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_save);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SteelForm";
            this.Text = "SteelForm";
            this.Load += new System.EventHandler(this.SteelForm_Load);
            this.grpBox_TopX.ResumeLayout(false);
            this.grpBox_TopX.PerformLayout();
            this.grpBox_TopY.ResumeLayout(false);
            this.grpBox_TopY.PerformLayout();
            this.grpBox_Top.ResumeLayout(false);
            this.grpBox_Wall.ResumeLayout(false);
            this.grpBox_WallX.ResumeLayout(false);
            this.grpBox_WallX.PerformLayout();
            this.grpBox_WallY.ResumeLayout(false);
            this.grpBox_WallY.PerformLayout();
            this.grpBox_Bottom.ResumeLayout(false);
            this.grpBox_BottomX.ResumeLayout(false);
            this.grpBox_BottomX.PerformLayout();
            this.grpBox_BottomY.ResumeLayout(false);
            this.grpBox_BottomY.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.GroupBox grpBox_TopX;
        private System.Windows.Forms.MaskedTextBox msktxt_TopSpacingX;
        private System.Windows.Forms.Label lbl_TopSpacingX;
        private System.Windows.Forms.Label lbl_topGaugeX;
        private System.Windows.Forms.Label lbl_TopGaugeUnitX;
        private System.Windows.Forms.ComboBox cmbBox_TopGaugeX;
        private System.Windows.Forms.GroupBox grpBox_TopY;
        private System.Windows.Forms.Label lbl_TopGaugeUnitY;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.MaskedTextBox msktxt_TopSpacingY;
        private System.Windows.Forms.Label lbl_TopSpacingY;
        private System.Windows.Forms.Label lbl_TopGaugeY;
        private System.Windows.Forms.GroupBox grpBox_Top;
        private System.Windows.Forms.GroupBox grpBox_Wall;
        private System.Windows.Forms.GroupBox grpBox_WallX;
        private System.Windows.Forms.Label lbl_WallGaugeUnitX;
        private System.Windows.Forms.ComboBox cmbBox_WallGaugeX;
        private System.Windows.Forms.MaskedTextBox msktxt_WallSpacingX;
        private System.Windows.Forms.Label lbl_WallSpacingX;
        private System.Windows.Forms.Label lbl_wallGaugeX;
        private System.Windows.Forms.GroupBox grpBox_WallY;
        private System.Windows.Forms.Label lbl_WallGaugeUnitY;
        private System.Windows.Forms.ComboBox cmbBox_WallGaugeY;
        private System.Windows.Forms.MaskedTextBox msktxt_WallSpacingY;
        private System.Windows.Forms.Label lbl_WallSpacingY;
        private System.Windows.Forms.Label lbl_wallGaugeY;
        private System.Windows.Forms.GroupBox grpBox_Bottom;
        private System.Windows.Forms.GroupBox grpBox_BottomX;
        private System.Windows.Forms.Label lbl_BottomGaugeUnitX;
        private System.Windows.Forms.ComboBox cmbBox_BottomGaugeX;
        private System.Windows.Forms.MaskedTextBox msktxt_BottomSpacingX;
        private System.Windows.Forms.Label lbl_BottomSpacingX;
        private System.Windows.Forms.Label lbl_bottomGaugeX;
        private System.Windows.Forms.GroupBox grpBox_BottomY;
        private System.Windows.Forms.Label lbl_BottomGaugeUnitY;
        private System.Windows.Forms.ComboBox cmbBox_BottomGaugeY;
        private System.Windows.Forms.MaskedTextBox msktxt_BottomSpacingY;
        private System.Windows.Forms.Label lbl_BottomSpacingY;
        private System.Windows.Forms.Label lbl_bottomGaugeY;
        private System.Windows.Forms.Button btn_manual;
        private System.Windows.Forms.CheckBox chc_isTopArmed;
        private System.Windows.Forms.MaskedTextBox msktxt_cover;
        private System.Windows.Forms.Label lbl_cover;
        private System.Windows.Forms.Label lbl_gaugeFactor;
        private System.Windows.Forms.MaskedTextBox msktxt_gaugeFactor;
    }
}