namespace Introducer
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.LabelNumMachinesActive = new System.Windows.Forms.Label();
            this.LabelNumMachinesIntroduced = new System.Windows.Forms.Label();
            this.LabelNumMachinesRegistered = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.RichTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ListViewMachines = new System.Windows.Forms.ListView();
            this.ColumnNovaId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnServerIp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnClientIp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnAttempts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Toolbar = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLog = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLog_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuIntroducer = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuIntroducer_Start = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuIntroducer_Stop = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuIntroducer_Status = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.LabelNumMachinesBanned = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.Toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Controls.Add(this.tabPage3);
            this.TabControl.Location = new System.Drawing.Point(0, 27);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(368, 159);
            this.TabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.LabelNumMachinesBanned);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.LabelNumMachinesActive);
            this.tabPage1.Controls.Add(this.LabelNumMachinesIntroduced);
            this.tabPage1.Controls.Add(this.LabelNumMachinesRegistered);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(360, 133);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Statistics";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // LabelNumMachinesActive
            // 
            this.LabelNumMachinesActive.AutoSize = true;
            this.LabelNumMachinesActive.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNumMachinesActive.Location = new System.Drawing.Point(131, 49);
            this.LabelNumMachinesActive.Name = "LabelNumMachinesActive";
            this.LabelNumMachinesActive.Size = new System.Drawing.Size(13, 13);
            this.LabelNumMachinesActive.TabIndex = 5;
            this.LabelNumMachinesActive.Text = "0";
            // 
            // LabelNumMachinesIntroduced
            // 
            this.LabelNumMachinesIntroduced.AutoSize = true;
            this.LabelNumMachinesIntroduced.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNumMachinesIntroduced.Location = new System.Drawing.Point(131, 31);
            this.LabelNumMachinesIntroduced.Name = "LabelNumMachinesIntroduced";
            this.LabelNumMachinesIntroduced.Size = new System.Drawing.Size(13, 13);
            this.LabelNumMachinesIntroduced.TabIndex = 4;
            this.LabelNumMachinesIntroduced.Text = "0";
            // 
            // LabelNumMachinesRegistered
            // 
            this.LabelNumMachinesRegistered.AutoSize = true;
            this.LabelNumMachinesRegistered.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNumMachinesRegistered.Location = new System.Drawing.Point(131, 13);
            this.LabelNumMachinesRegistered.Name = "LabelNumMachinesRegistered";
            this.LabelNumMachinesRegistered.Size = new System.Drawing.Size(13, 13);
            this.LabelNumMachinesRegistered.TabIndex = 3;
            this.LabelNumMachinesRegistered.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Machines Active:";
            this.ToolTip.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Machines Introduced:";
            this.ToolTip.SetToolTip(this.label2, "The number of machines introduced, successful or not.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Machines Registered:";
            this.ToolTip.SetToolTip(this.label1, "The total number of machines ever registered since the Introducer began.");
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.RichTextBoxLog);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(360, 133);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // RichTextBoxLog
            // 
            this.RichTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RichTextBoxLog.Location = new System.Drawing.Point(3, 3);
            this.RichTextBoxLog.Name = "RichTextBoxLog";
            this.RichTextBoxLog.ReadOnly = true;
            this.RichTextBoxLog.Size = new System.Drawing.Size(354, 127);
            this.RichTextBoxLog.TabIndex = 0;
            this.RichTextBoxLog.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ListViewMachines);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(360, 133);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Table";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ListViewMachines
            // 
            this.ListViewMachines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnNovaId,
            this.ColumnServerIp,
            this.ColumnClientIp,
            this.ColumnAttempts});
            this.ListViewMachines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewMachines.FullRowSelect = true;
            this.ListViewMachines.GridLines = true;
            this.ListViewMachines.Location = new System.Drawing.Point(3, 3);
            this.ListViewMachines.Name = "ListViewMachines";
            this.ListViewMachines.ShowGroups = false;
            this.ListViewMachines.ShowItemToolTips = true;
            this.ListViewMachines.Size = new System.Drawing.Size(354, 127);
            this.ListViewMachines.TabIndex = 0;
            this.ListViewMachines.UseCompatibleStateImageBehavior = false;
            this.ListViewMachines.View = System.Windows.Forms.View.Details;
            // 
            // ColumnNovaId
            // 
            this.ColumnNovaId.Text = "Nova ID";
            this.ColumnNovaId.Width = 110;
            // 
            // ColumnServerIp
            // 
            this.ColumnServerIp.Text = "Server IP";
            this.ColumnServerIp.Width = 109;
            // 
            // ColumnClientIp
            // 
            this.ColumnClientIp.Text = "Client IP";
            this.ColumnClientIp.Width = 120;
            // 
            // ColumnAttempts
            // 
            this.ColumnAttempts.Text = "Authentication Attempts";
            this.ColumnAttempts.Width = 140;
            // 
            // Toolbar
            // 
            this.Toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuLog,
            this.MenuIntroducer});
            this.Toolbar.Location = new System.Drawing.Point(0, 0);
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.Size = new System.Drawing.Size(368, 24);
            this.Toolbar.TabIndex = 2;
            this.Toolbar.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile_Exit});
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(37, 20);
            this.MenuFile.Text = "File";
            // 
            // MenuFile_Exit
            // 
            this.MenuFile_Exit.Name = "MenuFile_Exit";
            this.MenuFile_Exit.Size = new System.Drawing.Size(92, 22);
            this.MenuFile_Exit.Text = "Exit";
            this.MenuFile_Exit.Click += new System.EventHandler(this.MenuFile_Exit_Click);
            // 
            // MenuLog
            // 
            this.MenuLog.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuLog_SaveAs});
            this.MenuLog.Name = "MenuLog";
            this.MenuLog.Size = new System.Drawing.Size(39, 20);
            this.MenuLog.Text = "Log";
            // 
            // MenuLog_SaveAs
            // 
            this.MenuLog_SaveAs.Name = "MenuLog_SaveAs";
            this.MenuLog_SaveAs.Size = new System.Drawing.Size(123, 22);
            this.MenuLog_SaveAs.Text = "Save As...";
            // 
            // MenuIntroducer
            // 
            this.MenuIntroducer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuIntroducer_Start,
            this.MenuIntroducer_Stop,
            this.MenuIntroducer_Status});
            this.MenuIntroducer.Name = "MenuIntroducer";
            this.MenuIntroducer.Size = new System.Drawing.Size(74, 20);
            this.MenuIntroducer.Text = "Introducer";
            // 
            // MenuIntroducer_Start
            // 
            this.MenuIntroducer_Start.Name = "MenuIntroducer_Start";
            this.MenuIntroducer_Start.Size = new System.Drawing.Size(106, 22);
            this.MenuIntroducer_Start.Text = "Start";
            this.MenuIntroducer_Start.Click += new System.EventHandler(this.MenuIntroducer_Start_Click);
            // 
            // MenuIntroducer_Stop
            // 
            this.MenuIntroducer_Stop.Name = "MenuIntroducer_Stop";
            this.MenuIntroducer_Stop.Size = new System.Drawing.Size(106, 22);
            this.MenuIntroducer_Stop.Text = "Stop";
            this.MenuIntroducer_Stop.Click += new System.EventHandler(this.MenuIntroducer_Stop_Click);
            // 
            // MenuIntroducer_Status
            // 
            this.MenuIntroducer_Status.Name = "MenuIntroducer_Status";
            this.MenuIntroducer_Status.Size = new System.Drawing.Size(106, 22);
            this.MenuIntroducer_Status.Text = "Status";
            this.MenuIntroducer_Status.Click += new System.EventHandler(this.MenuIntroducer_Status_Click);
            // 
            // ToolTip
            // 
            this.ToolTip.AutomaticDelay = 100;
            this.ToolTip.AutoPopDelay = 20000;
            this.ToolTip.InitialDelay = 100;
            this.ToolTip.ReshowDelay = 20;
            // 
            // LabelNumMachinesBanned
            // 
            this.LabelNumMachinesBanned.AutoSize = true;
            this.LabelNumMachinesBanned.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNumMachinesBanned.Location = new System.Drawing.Point(131, 67);
            this.LabelNumMachinesBanned.Name = "LabelNumMachinesBanned";
            this.LabelNumMachinesBanned.Size = new System.Drawing.Size(13, 13);
            this.LabelNumMachinesBanned.TabIndex = 7;
            this.LabelNumMachinesBanned.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Machines Banned:";
            this.ToolTip.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 186);
            this.Controls.Add(this.Toolbar);
            this.Controls.Add(this.TabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.Toolbar;
            this.Name = "FormMain";
            this.Text = "Introducer - Nova Remote Assistance Tool";
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label LabelNumMachinesActive;
        private System.Windows.Forms.Label LabelNumMachinesIntroduced;
        private System.Windows.Forms.Label LabelNumMachinesRegistered;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox RichTextBoxLog;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView ListViewMachines;
        private System.Windows.Forms.ColumnHeader ColumnNovaId;
        private System.Windows.Forms.ColumnHeader ColumnServerIp;
        private System.Windows.Forms.ColumnHeader ColumnClientIp;
        private System.Windows.Forms.ColumnHeader ColumnAttempts;
        private System.Windows.Forms.MenuStrip Toolbar;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStripMenuItem MenuFile_Exit;
        private System.Windows.Forms.ToolStripMenuItem MenuLog;
        private System.Windows.Forms.ToolStripMenuItem MenuLog_SaveAs;
        private System.Windows.Forms.ToolStripMenuItem MenuIntroducer;
        private System.Windows.Forms.ToolStripMenuItem MenuIntroducer_Start;
        private System.Windows.Forms.ToolStripMenuItem MenuIntroducer_Stop;
        private System.Windows.Forms.ToolStripMenuItem MenuIntroducer_Status;
        private System.Windows.Forms.Label LabelNumMachinesBanned;
        private System.Windows.Forms.Label label5;
    }
}