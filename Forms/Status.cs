using BG3_Save_Backup.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

namespace BG3_Save_Backup.Forms {
    public partial class Status : Form {
        public Status() {
            InitializeComponent();
            if (Program.ValidBackupTarget && Program.Watcher.Enabled)
                Tray.Icon = Resources.DefaultIcon;
            else
                Tray.Icon = Resources.ErrorIcon;
            Program.Watcher.BackupTriggered += WatcherTrigger;
        }

        private void Status_Load(object sender, EventArgs e) {
            Hide();
            WindowState = FormWindowState.Minimized;
            LarianFolderTextbox.Text = Settings.Default.LarianSaveLoc;
            BackupFolderTextbox.Text = Settings.Default.BackupSaveLoc;
            RefreshDgv();
        }

        private void Status_Shown(object sender, EventArgs e) {
            RefreshDgv();
        }

        private void WatcherTrigger(object sender, EventArgs e) {
            RefreshDgv();
        }

        private void RefreshDgv(IEnumerable<string> files = null) {
            if (files is null)
                files = new DirectoryInfo(BackupFolderTextbox.Text)
                    .GetDirectories()
                    .OrderByDescending(f => f.LastWriteTime)
                    .Select(f => f.Name)
                    .ToList()
                    .Take(10);
            if (SavesDgv.InvokeRequired) {
                Action safeRefresh = delegate {
                    RefreshDgv(files);
                };
                SavesDgv.Invoke(safeRefresh);
            } else {
                SavesDgv.Rows.Clear();
                foreach (string file in files) {
                    SavesDgv.Rows.Add(file);
                }
            }
        }

        private void RefreshDgvByInvoke(IEnumerable<string> files) {

        }

        private void Status_Resize(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Minimized) {
                Hide();
            }
        }

        private void Tray_DoubleClick(object sender, EventArgs e) {
            RefreshDgv();
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void LarianFolderBrowse_Click(object sender, EventArgs e) {
            var dialog = new CommonOpenFileDialog {
                IsFolderPicker = true,
                Multiselect = false,
                DefaultDirectory = LarianFolderTextbox.Text,
                InitialDirectory = LarianFolderTextbox.Text
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                LarianFolderTextbox.Text = dialog.FileName;
                Settings.Default.LarianSaveLoc = dialog.FileName;
                Settings.Default.Save();
                Program.Watcher.LarianPath = dialog.FileName;
            }
        }

        private void LarianFolderTextbox_DoubleClick(object sender, EventArgs e) {
            LarianFolderBrowse_Click(sender, e);
        }

        private void BackupFolderBrowse_Click(object sender, EventArgs e) {
            var dialog = new CommonOpenFileDialog {
                IsFolderPicker = true,
                Multiselect = false,
                DefaultDirectory = BackupFolderTextbox.Text,
                InitialDirectory = BackupFolderTextbox.Text
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                BackupFolderTextbox.Text = dialog.FileName;
                Settings.Default.BackupSaveLoc = dialog.FileName;
                Settings.Default.Save();
                Program.ValidBackupTarget = true;
                Tray.Icon = Resources.DefaultIcon;
                Program.Watcher.BackupPath = dialog.FileName;
            }
        }

        private void BackupFolderTextbox_DoubleClick(object sender, EventArgs e) {
            BackupFolderBrowse_Click(sender, e);
        }

        private void statusToolStripMenuItem_Click(object sender, EventArgs e) {
            RefreshDgv();
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
