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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLiveControl));
            this.ButtonRequestScreenshot = new System.Windows.Forms.Button();
            this.gdiScreen1 = new Controls.LiveControl.GdiScreen();
            this.SuspendLayout();
            // 
            // ButtonRequestScreenshot
            // 
            this.ButtonRequestScreenshot.BackColor = System.Drawing.Color.Red;
            this.ButtonRequestScreenshot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonRequestScreenshot.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonRequestScreenshot.Location = new System.Drawing.Point(570, 340);
            this.ButtonRequestScreenshot.Name = "ButtonRequestScreenshot";
            this.ButtonRequestScreenshot.Size = new System.Drawing.Size(212, 40);
            this.ButtonRequestScreenshot.TabIndex = 3;
            this.ButtonRequestScreenshot.Text = "Click to Begin";
            this.ButtonRequestScreenshot.UseVisualStyleBackColor = false;
            this.ButtonRequestScreenshot.Click += new System.EventHandler(this.ButtonRequestScreenshot_Click);
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
            // FormLiveControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.ButtonRequestScreenshot);
            this.Controls.Add(this.gdiScreen1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormLiveControl";
            this.Text = "Remote Desktop Client";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.LiveControl.GdiScreen gdiScreen1;
        private System.Windows.Forms.Button ButtonRequestScreenshot;

    }
}