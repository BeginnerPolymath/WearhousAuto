namespace WarehouseAuto
{
    public partial class Number
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
            this.CountNumber2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CountNumber2
            // 
            this.CountNumber2.AutoSize = true;
            this.CountNumber2.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CountNumber2.Location = new System.Drawing.Point(12, 9);
            this.CountNumber2.Name = "CountNumber2";
            this.CountNumber2.Size = new System.Drawing.Size(58, 63);
            this.CountNumber2.TabIndex = 0;
            this.CountNumber2.Text = "0";
            this.CountNumber2.Click += new System.EventHandler(this.CountNumber2_Click);
            // 
            // Number
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(120, 74);
            this.Controls.Add(this.CountNumber2);
            this.Name = "Number";
            this.Text = "Number";
            this.Load += new System.EventHandler(this.Number_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label CountNumber2;
    }
}