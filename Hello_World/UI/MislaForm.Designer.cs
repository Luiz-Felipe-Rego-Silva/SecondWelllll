
namespace Hello_World.UI
{
    partial class MislaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MislaForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.msktxt_mislaHeigth = new System.Windows.Forms.MaskedTextBox();
            this.mskdtxt_mislaLength = new System.Windows.Forms.MaskedTextBox();
            this.lbl_instruction = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(16, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(163, 148);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(194, 164);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Salvar";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(194, 135);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Cancelar";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // msktxt_mislaHeigth
            // 
            this.msktxt_mislaHeigth.Location = new System.Drawing.Point(12, 100);
            this.msktxt_mislaHeigth.Name = "msktxt_mislaHeigth";
            this.msktxt_mislaHeigth.Size = new System.Drawing.Size(36, 20);
            this.msktxt_mislaHeigth.TabIndex = 3;
            this.msktxt_mislaHeigth.Text = "30";
            // 
            // mskdtxt_mislaLength
            // 
            this.mskdtxt_mislaLength.Location = new System.Drawing.Point(91, 169);
            this.mskdtxt_mislaLength.Name = "mskdtxt_mislaLength";
            this.mskdtxt_mislaLength.Size = new System.Drawing.Size(46, 20);
            this.mskdtxt_mislaLength.TabIndex = 4;
            this.mskdtxt_mislaLength.Text = "30";
            // 
            // lbl_instruction
            // 
            this.lbl_instruction.AutoSize = true;
            this.lbl_instruction.Location = new System.Drawing.Point(13, 13);
            this.lbl_instruction.Name = "lbl_instruction";
            this.lbl_instruction.Size = new System.Drawing.Size(183, 13);
            this.lbl_instruction.TabIndex = 5;
            this.lbl_instruction.Text = "Insira as dimensões da misula em cm:";
            // 
            // MislaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 199);
            this.Controls.Add(this.lbl_instruction);
            this.Controls.Add(this.mskdtxt_mislaLength);
            this.Controls.Add(this.msktxt_mislaHeigth);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MislaForm";
            this.Text = "Misula";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.MaskedTextBox msktxt_mislaHeigth;
        private System.Windows.Forms.MaskedTextBox mskdtxt_mislaLength;
        private System.Windows.Forms.Label lbl_instruction;
    }
}