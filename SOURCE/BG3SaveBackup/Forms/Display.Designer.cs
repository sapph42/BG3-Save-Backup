namespace BG3SaveBackup.Forms {
    partial class Display {
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
            components = new System.ComponentModel.Container();
            Tray = new NotifyIcon(components);
            TrayMenu = new ContextMenuStrip(components);
            statusToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            LarianFolderTextbox = new TextBox();
            LarianFolderBrowse = new Button();
            BackupFolderTextbox = new TextBox();
            BackupFolderBrowse = new Button();
            label2 = new Label();
            label3 = new Label();
            BackupAction = new ContextMenuStrip(components);
            deleteBackupToolStripMenuItem = new ToolStripMenuItem();
            restoreBackupToolStripMenuItem = new ToolStripMenuItem();
            ScreenshotImage = new PictureBox();
            SaveTree = new TreeView();
            button1 = new Button();
            SaveDataTextbox = new TextBox();
            processTimer = new System.Windows.Forms.Timer(components);
            Kill9Bg3 = new Button();
            TrayMenu.SuspendLayout();
            BackupAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ScreenshotImage).BeginInit();
            SuspendLayout();
            // 
            // Tray
            // 
            Tray.BalloonTipTitle = "BG3 Save Backup";
            Tray.ContextMenuStrip = TrayMenu;
            Tray.Text = "BG3 Save Backup";
            Tray.Visible = true;
            Tray.DoubleClick += Tray_DoubleClick;
            // 
            // TrayMenu
            // 
            TrayMenu.Items.AddRange(new ToolStripItem[] { statusToolStripMenuItem, exitToolStripMenuItem });
            TrayMenu.Name = "TrayMenu";
            TrayMenu.Size = new Size(107, 48);
            // 
            // statusToolStripMenuItem
            // 
            statusToolStripMenuItem.Name = "statusToolStripMenuItem";
            statusToolStripMenuItem.Size = new Size(106, 22);
            statusToolStripMenuItem.Text = "Status";
            statusToolStripMenuItem.Click += statusToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(106, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(131, 15);
            label1.TabIndex = 1;
            label1.Text = "BG3 Save Data Location";
            // 
            // LarianFolderTextbox
            // 
            LarianFolderTextbox.Location = new Point(15, 25);
            LarianFolderTextbox.Name = "LarianFolderTextbox";
            LarianFolderTextbox.ReadOnly = true;
            LarianFolderTextbox.Size = new Size(561, 23);
            LarianFolderTextbox.TabIndex = 2;
            LarianFolderTextbox.DoubleClick += LarianFolderTextbox_DoubleClick;
            // 
            // LarianFolderBrowse
            // 
            LarianFolderBrowse.Location = new Point(583, 21);
            LarianFolderBrowse.Name = "LarianFolderBrowse";
            LarianFolderBrowse.Size = new Size(75, 23);
            LarianFolderBrowse.TabIndex = 3;
            LarianFolderBrowse.Text = "Browse";
            LarianFolderBrowse.UseVisualStyleBackColor = true;
            LarianFolderBrowse.Click += LarianFolderBrowse_Click;
            // 
            // BackupFolderTextbox
            // 
            BackupFolderTextbox.Location = new Point(15, 101);
            BackupFolderTextbox.Name = "BackupFolderTextbox";
            BackupFolderTextbox.ReadOnly = true;
            BackupFolderTextbox.Size = new Size(561, 23);
            BackupFolderTextbox.TabIndex = 5;
            BackupFolderTextbox.DoubleClick += BackupFolderTextbox_DoubleClick;
            // 
            // BackupFolderBrowse
            // 
            BackupFolderBrowse.Location = new Point(583, 101);
            BackupFolderBrowse.Name = "BackupFolderBrowse";
            BackupFolderBrowse.Size = new Size(75, 23);
            BackupFolderBrowse.TabIndex = 6;
            BackupFolderBrowse.Text = "Browse";
            BackupFolderBrowse.UseVisualStyleBackColor = true;
            BackupFolderBrowse.Click += BackupFolderBrowse_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 85);
            label2.Name = "label2";
            label2.Size = new Size(95, 15);
            label2.TabIndex = 4;
            label2.Text = "Backup Location";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 161);
            label3.Name = "label3";
            label3.Size = new Size(147, 15);
            label3.TabIndex = 7;
            label3.Text = "Most Recent Backup Saves";
            // 
            // BackupAction
            // 
            BackupAction.Items.AddRange(new ToolStripItem[] { deleteBackupToolStripMenuItem, restoreBackupToolStripMenuItem });
            BackupAction.Name = "BackupAction";
            BackupAction.Size = new Size(156, 48);
            // 
            // deleteBackupToolStripMenuItem
            // 
            deleteBackupToolStripMenuItem.Name = "deleteBackupToolStripMenuItem";
            deleteBackupToolStripMenuItem.Size = new Size(155, 22);
            deleteBackupToolStripMenuItem.Text = "Delete Backup";
            deleteBackupToolStripMenuItem.Click += deleteBackupToolStripMenuItem_Click;
            // 
            // restoreBackupToolStripMenuItem
            // 
            restoreBackupToolStripMenuItem.Name = "restoreBackupToolStripMenuItem";
            restoreBackupToolStripMenuItem.Size = new Size(155, 22);
            restoreBackupToolStripMenuItem.Text = "Restore Backup";
            restoreBackupToolStripMenuItem.Click += restoreBackupToolStripMenuItem_Click;
            // 
            // ScreenshotImage
            // 
            ScreenshotImage.Location = new Point(666, 262);
            ScreenshotImage.Name = "ScreenshotImage";
            ScreenshotImage.Size = new Size(400, 225);
            ScreenshotImage.TabIndex = 10;
            ScreenshotImage.TabStop = false;
            // 
            // SaveTree
            // 
            SaveTree.Location = new Point(15, 179);
            SaveTree.Name = "SaveTree";
            SaveTree.Size = new Size(643, 308);
            SaveTree.TabIndex = 11;
            SaveTree.AfterSelect += SaveTree_AfterSelect;
            SaveTree.MouseUp += SaveTree_MouseUp;
            // 
            // button1
            // 
            button1.Location = new Point(568, 150);
            button1.Name = "button1";
            button1.Size = new Size(90, 23);
            button1.TabIndex = 12;
            button1.Text = "RefreshNow";
            button1.UseVisualStyleBackColor = true;
            button1.Click += RefreshNow_Click;
            // 
            // SaveDataTextbox
            // 
            SaveDataTextbox.Location = new Point(666, 153);
            SaveDataTextbox.Multiline = true;
            SaveDataTextbox.Name = "SaveDataTextbox";
            SaveDataTextbox.ReadOnly = true;
            SaveDataTextbox.Size = new Size(400, 103);
            SaveDataTextbox.TabIndex = 13;
            // 
            // processTimer
            // 
            processTimer.Interval = 30000;
            processTimer.Tick += processTimer_Tick;
            // 
            // Kill9Bg3
            // 
            Kill9Bg3.Location = new Point(917, 101);
            Kill9Bg3.Name = "Kill9Bg3";
            Kill9Bg3.Size = new Size(149, 47);
            Kill9Bg3.TabIndex = 14;
            Kill9Bg3.Text = "kill -9 bg3";
            Kill9Bg3.UseVisualStyleBackColor = true;
            Kill9Bg3.Click += Kill9Bg3_Click;
            // 
            // Display
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1078, 499);
            Controls.Add(Kill9Bg3);
            Controls.Add(SaveDataTextbox);
            Controls.Add(button1);
            Controls.Add(SaveTree);
            Controls.Add(ScreenshotImage);
            Controls.Add(label3);
            Controls.Add(BackupFolderBrowse);
            Controls.Add(BackupFolderTextbox);
            Controls.Add(label2);
            Controls.Add(LarianFolderBrowse);
            Controls.Add(LarianFolderTextbox);
            Controls.Add(label1);
            Name = "Display";
            Text = "Status";
            Load += Status_Load;
            Shown += Status_Shown;
            TrayMenu.ResumeLayout(false);
            BackupAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ScreenshotImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
		private System.Windows.Forms.ContextMenuStrip TrayMenu;
		private System.Windows.Forms.ToolStripMenuItem statusToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip BackupAction;
		private System.Windows.Forms.ToolStripMenuItem deleteBackupToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem restoreBackupToolStripMenuItem;
		private System.Windows.Forms.PictureBox ScreenshotImage;
        private TreeView SaveTree;
        private Button button1;
        private TextBox SaveDataTextbox;
        private System.Windows.Forms.Timer processTimer;
        private Button Kill9Bg3;
    }
}