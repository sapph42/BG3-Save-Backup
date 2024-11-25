namespace BG3_Save_Backup.Forms {
    partial class Status {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Status));
            this.Tray = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.LarianFolderTextbox = new System.Windows.Forms.TextBox();
            this.LarianFolderBrowse = new System.Windows.Forms.Button();
            this.BackupFolderBrowse = new System.Windows.Forms.Button();
            this.BackupFolderTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SavesDgv = new System.Windows.Forms.DataGridView();
            this.SavePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastWriteTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HonorOnly = new System.Windows.Forms.CheckBox();
            this.BackupAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScreenshotImage = new System.Windows.Forms.PictureBox();
            this.TrayMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SavesDgv)).BeginInit();
            this.BackupAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tray
            // 
            this.Tray.BalloonTipTitle = "BG3 Save Backup";
            this.Tray.ContextMenuStrip = this.TrayMenu;
            this.Tray.Text = "BG3 Save Backup";
            this.Tray.Visible = true;
            this.Tray.DoubleClick += new System.EventHandler(this.Tray_DoubleClick);
            // 
            // TrayMenu
            // 
            this.TrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.TrayMenu.Name = "TrayMenu";
            this.TrayMenu.Size = new System.Drawing.Size(107, 48);
            // 
            // statusToolStripMenuItem
            // 
            this.statusToolStripMenuItem.Name = "statusToolStripMenuItem";
            this.statusToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.statusToolStripMenuItem.Text = "Status";
            this.statusToolStripMenuItem.Click += new System.EventHandler(this.statusToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "BG3 Save Data Location";
            // 
            // LarianFolderTextbox
            // 
            this.LarianFolderTextbox.Location = new System.Drawing.Point(15, 25);
            this.LarianFolderTextbox.Name = "LarianFolderTextbox";
            this.LarianFolderTextbox.ReadOnly = true;
            this.LarianFolderTextbox.Size = new System.Drawing.Size(561, 20);
            this.LarianFolderTextbox.TabIndex = 2;
            this.LarianFolderTextbox.DoubleClick += new System.EventHandler(this.LarianFolderTextbox_DoubleClick);
            // 
            // LarianFolderBrowse
            // 
            this.LarianFolderBrowse.Location = new System.Drawing.Point(583, 21);
            this.LarianFolderBrowse.Name = "LarianFolderBrowse";
            this.LarianFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.LarianFolderBrowse.TabIndex = 3;
            this.LarianFolderBrowse.Text = "Browse";
            this.LarianFolderBrowse.UseVisualStyleBackColor = true;
            this.LarianFolderBrowse.Click += new System.EventHandler(this.LarianFolderBrowse_Click);
            // 
            // BackupFolderBrowse
            // 
            this.BackupFolderBrowse.Location = new System.Drawing.Point(583, 101);
            this.BackupFolderBrowse.Name = "BackupFolderBrowse";
            this.BackupFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.BackupFolderBrowse.TabIndex = 6;
            this.BackupFolderBrowse.Text = "Browse";
            this.BackupFolderBrowse.UseVisualStyleBackColor = true;
            this.BackupFolderBrowse.Click += new System.EventHandler(this.BackupFolderBrowse_Click);
            // 
            // BackupFolderTextbox
            // 
            this.BackupFolderTextbox.Location = new System.Drawing.Point(15, 101);
            this.BackupFolderTextbox.Name = "BackupFolderTextbox";
            this.BackupFolderTextbox.ReadOnly = true;
            this.BackupFolderTextbox.Size = new System.Drawing.Size(561, 20);
            this.BackupFolderTextbox.TabIndex = 5;
            this.BackupFolderTextbox.DoubleClick += new System.EventHandler(this.BackupFolderTextbox_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Backup Location";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Most Recent Backup Saves";
            // 
            // SavesDgv
            // 
            this.SavesDgv.AllowUserToAddRows = false;
            this.SavesDgv.AllowUserToDeleteRows = false;
            this.SavesDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SavesDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SavePath,
            this.LastWriteTime});
            this.SavesDgv.Location = new System.Drawing.Point(15, 177);
            this.SavesDgv.Name = "SavesDgv";
            this.SavesDgv.ReadOnly = true;
            this.SavesDgv.Size = new System.Drawing.Size(643, 207);
            this.SavesDgv.TabIndex = 8;
            this.SavesDgv.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SavesDgv_MouseUp);
            // 
            // SavePath
            // 
            this.SavePath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SavePath.HeaderText = "Save";
            this.SavePath.MinimumWidth = 450;
            this.SavePath.Name = "SavePath";
            this.SavePath.ReadOnly = true;
            this.SavePath.Width = 450;
            // 
            // LastWriteTime
            // 
            this.LastWriteTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.LastWriteTime.HeaderText = "LastWriteTime";
            this.LastWriteTime.Name = "LastWriteTime";
            this.LastWriteTime.ReadOnly = true;
            // 
            // HonorOnly
            // 
            this.HonorOnly.AutoSize = true;
            this.HonorOnly.Location = new System.Drawing.Point(543, 157);
            this.HonorOnly.Name = "HonorOnly";
            this.HonorOnly.Size = new System.Drawing.Size(115, 17);
            this.HonorOnly.TabIndex = 9;
            this.HonorOnly.Text = "Honour Mode Only";
            this.HonorOnly.UseVisualStyleBackColor = true;
            this.HonorOnly.CheckedChanged += new System.EventHandler(this.HonorOnly_CheckedChanged);
            //
            // ScreenshotImage
            //
            this.ScreenshotImage.Location = new System.Drawing.Point(664, 159);
            this.ScreenshotImage.Name = "ScreenshotImage";
            this.ScreenshotImage.Size = new System.Drawing.Size(400, 225);
            this.ScreenshotImage.TabIndex = 10;
            this.ScreenshotImage.TabStop = false;
            // 
            // BackupAction
            // 
            this.BackupAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteBackupToolStripMenuItem,
            this.restoreBackupToolStripMenuItem});
            this.BackupAction.Name = "BackupAction";
            this.BackupAction.Size = new System.Drawing.Size(181, 70);
            // 
            // deleteBackupToolStripMenuItem
            // 
            this.deleteBackupToolStripMenuItem.Name = "deleteBackupToolStripMenuItem";
            this.deleteBackupToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteBackupToolStripMenuItem.Text = "Delete Backup";
            this.deleteBackupToolStripMenuItem.Click += new System.EventHandler(this.deleteBackupToolStripMenuItem_Click);
            // 
            // restoreBackupToolStripMenuItem
            // 
            this.restoreBackupToolStripMenuItem.Name = "restoreBackupToolStripMenuItem";
            this.restoreBackupToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.restoreBackupToolStripMenuItem.Text = "Restore Backup";
            this.restoreBackupToolStripMenuItem.Click += new System.EventHandler(this.restoreBackupToolStripMenuItem_Click);
            // 
            // Status
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 397);
            this.Controls.Add(this.ScreenshotImage);
            this.Controls.Add(this.HonorOnly);
            this.Controls.Add(this.SavesDgv);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BackupFolderBrowse);
            this.Controls.Add(this.BackupFolderTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LarianFolderBrowse);
            this.Controls.Add(this.LarianFolderTextbox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Status";
            this.Text = "Status";
            this.Load += new System.EventHandler(this.Status_Load);
            this.Shown += new System.EventHandler(this.Status_Shown);
            this.Resize += new System.EventHandler(this.Status_Resize);
            this.TrayMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SavesDgv)).EndInit();
            this.BackupAction.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon Tray;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LarianFolderTextbox;
        private System.Windows.Forms.Button LarianFolderBrowse;
        private System.Windows.Forms.Button BackupFolderBrowse;
        private System.Windows.Forms.TextBox BackupFolderTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView SavesDgv;
        private System.Windows.Forms.ContextMenuStrip TrayMenu;
        private System.Windows.Forms.ToolStripMenuItem statusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn SavePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastWriteTime;
        private System.Windows.Forms.CheckBox HonorOnly;
        private System.Windows.Forms.ContextMenuStrip BackupAction;
        private System.Windows.Forms.ToolStripMenuItem deleteBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreBackupToolStripMenuItem;
        private System.Windows.Forms.PictureBox ScreenshotImage;
    }
}
