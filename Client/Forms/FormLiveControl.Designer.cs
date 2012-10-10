namespace Client.Forms
{
    partial class FormLiveControl
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
            this.gdiScreen1 = new Controls.LiveControl.GdiScreen();
            this.ButtonRequestScreenshot = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gdiScreen1
            // 
            this.gdiScreen1.BackColor = System.Drawing.Color.Black;
            this.gdiScreen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdiScreen1.Location = new System.Drawing.Point(0, 0);
            this.gdiScreen1.Name = "gdiScreen1";
            this.gdiScreen1.Size = new System.Drawing.Size(1366, 768);
            this.gdiScreen1.TabIndex = 2;
            this.gdiScreen1.Text = "gdiScreen1";
            // 
            // ButtonRequestScreenshot
            // 
            this.ButtonRequestScreenshot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonRequestScreenshot.Location = new System.Drawing.Point(1351, 0);
            this.ButtonRequestScreenshot.Name = "ButtonRequestScreenshot";
            this.ButtonRequestScreenshot.Size = new System.Drawing.Size(15, 15);
            this.ButtonRequestScreenshot.TabIndex = 3;
            this.ButtonRequestScreenshot.UseVisualStyleBackColor = true;
            this.ButtonRequestScreenshot.Click += new System.EventHandler(this.ButtonRequestScreenshot_Click);
            // 
            // FormLiveControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.ButtonRequestScreenshot);
            this.Controls.Add(this.gdiScreen1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Name = "FormLiveControl";
            this.Text = "FormLiveControl";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.LiveControl.GdiScreen gdiScreen1;
        private System.Windows.Forms.Button ButtonRequestScreenshot;

    }
}