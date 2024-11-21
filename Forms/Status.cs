using BG3_Save_Backup.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
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
            HonorOnly.Checked = Settings.Default.HonorOnly;
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
        private void RefreshDgv(IEnumerable<DirectoryInfo> folders = null) {
            if (folders is null)
                folders = new DirectoryInfo(BackupFolderTextbox.Text)
                    .GetDirectories()
                    .OrderByDescending(f => f.LastWriteTime)
                    .ToList();
            var normalSaves = folders.Where(f => !f.Name.EndsWith("_HonourMode"));
            var honorSaves = folders.Where(f => f.Name.EndsWith("_HonourMode"));
            if (HonorOnly.Checked)
                folders = null;
            else
                folders = normalSaves.ToList();
            foreach (var honor in honorSaves) {
                string save = honor.FullName;
                var honorSnapshots = new DirectoryInfo(save)
                                            .GetDirectories()
                                            .OrderByDescending(f => f.LastWriteTime)
                                            .ToList();
                folders = folders.Concat(honorSnapshots);
            }
            folders.OrderByDescending(f => f.LastWriteTime).ToList().Take(10);
            if (SavesDgv.InvokeRequired) {
                Action safeRefresh = delegate {
                    RefreshDgv(folders);
                };
                SavesDgv.Invoke(safeRefresh);
            } else {
                SavesDgv.Rows.Clear();
                foreach (var folder in folders) {
                    string file = "";
                    if (folder.FullName.Contains("_HonourMode"))
                        file = Path.Combine(folder.Parent.Name, folder.Name);
                    else
                        file = folder.Name;
                    SavesDgv.Rows.Add(file, folder.LastWriteTime.ToString("dd MMM yyyy HH:mm:ss"));
                    SavesDgv.Sort(LastWriteTime, ListSortDirection.Descending);
                }
            }
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
        private void HonorOnly_CheckedChanged(object sender, EventArgs e) {
            Settings.Default.HonorOnly = HonorOnly.Checked;
            Settings.Default.Save();
        }
    }
}
