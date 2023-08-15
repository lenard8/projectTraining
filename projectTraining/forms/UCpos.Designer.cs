
namespace projectTraining.forms
{
    partial class UCpos
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picturebox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.lblid = new System.Windows.Forms.Label();
            this.lbldes = new System.Windows.Forms.Label();
            this.lblprice = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picturebox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picturebox1
            // 
            this.picturebox1.ImageRotate = 0F;
            this.picturebox1.Location = new System.Drawing.Point(3, 3);
            this.picturebox1.Name = "picturebox1";
            this.picturebox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.picturebox1.Size = new System.Drawing.Size(191, 181);
            this.picturebox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picturebox1.TabIndex = 0;
            this.picturebox1.TabStop = false;
            // 
            // lblid
            // 
            this.lblid.AutoSize = true;
            this.lblid.Font = new System.Drawing.Font("Dubai", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblid.Location = new System.Drawing.Point(3, 197);
            this.lblid.Name = "lblid";
            this.lblid.Size = new System.Drawing.Size(53, 24);
            this.lblid.TabIndex = 1;
            this.lblid.Text = "PCODE";
            // 
            // lbldes
            // 
            this.lbldes.AutoSize = true;
            this.lbldes.Font = new System.Drawing.Font("Dubai", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lbldes.Location = new System.Drawing.Point(2, 219);
            this.lbldes.Name = "lbldes";
            this.lbldes.Size = new System.Drawing.Size(44, 29);
            this.lbldes.TabIndex = 2;
            this.lbldes.Text = "DES";
            // 
            // lblprice
            // 
            this.lblprice.AutoSize = true;
            this.lblprice.Font = new System.Drawing.Font("Dubai", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblprice.Location = new System.Drawing.Point(3, 248);
            this.lblprice.Name = "lblprice";
            this.lblprice.Size = new System.Drawing.Size(57, 29);
            this.lblprice.TabIndex = 3;
            this.lblprice.Text = "PRICE";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.picturebox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(213, 189);
            this.panel1.TabIndex = 4;
            // 
            // UCpos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblprice);
            this.Controls.Add(this.lbldes);
            this.Controls.Add(this.lblid);
            this.Name = "UCpos";
            this.Size = new System.Drawing.Size(213, 280);
            this.Load += new System.EventHandler(this.UCpos_Load);
            this.MouseEnter += new System.EventHandler(this.UCpos_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.UCpos_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.picturebox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        public Guna.UI2.WinForms.Guna2CirclePictureBox picturebox1;
        public System.Windows.Forms.Label lblid;
        public System.Windows.Forms.Label lbldes;
        public System.Windows.Forms.Label lblprice;
    }
}
