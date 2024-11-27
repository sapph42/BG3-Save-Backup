using BG3_Save_Backup.Properties;
using BG3_Save_Backup.Classes;
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
        private string currentCellVal;
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
        private void RefreshDgv(List<DirectoryInfo> folders = null) {
            if (folders is null) {
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
                    if (folders is null)
                        folders = honorSnapshots;
                    else
                        folders.AddRange(honorSnapshots);
                }
            }
     
            folders.OrderByDescending(f => f.LastWriteTime).ToList().Take(10);
            var splitPath = Settings.Default.BackupSaveLoc.Split(Path.DirectorySeparatorChar);
            var parent = splitPath[splitPath.Length - 1];
            folders.Where(f => f.Name != parent).ToList();
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
        private void SavesDgv_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Right) return;
            var currentMouseOverRow = SavesDgv.HitTest(e.X, e.Y);
            int row = currentMouseOverRow.RowIndex;
            int col = currentMouseOverRow.ColumnIndex;
            if (row < 0 || col < 0) return;
            currentCellVal = SavesDgv.Rows[row].Cells[0].Value.ToString();
            BackupAction.Show((Control)sender, new System.Drawing.Point(e.X, e.Y));
        }
        private void SavesDgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (e.Button != MouseButtons.Left)
                return;
            DataGridViewCell cell = SavesDgv.Rows[e.RowIndex].Cells[0];
            string savePath = cell.Value.ToString();
            string fullPath = Path.Combine(Settings.Default.BackupSaveLoc, savePath);
            string imagePath = Directory
                .GetFiles(fullPath)
                .Where(f => f.EndsWith("WebP"))
                .FirstOrDefault();
            if (imagePath is null)
                return;
            byte[] imageData = File.ReadAllBytes(imagePath);
            ScreenshotImage.Image = WebP.DecodeFromBytes(imageData, imageData.Length);
            ScreenshotImage.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void deleteBackupToolStripMenuItem_Click(object sender, EventArgs e) {
            string path = Path.Combine(Settings.Default.BackupSaveLoc, currentCellVal);
            try {
                Directory.Delete(path, true);
                RefreshDgv();
            } catch {

            }
        }

        private void restoreBackupToolStripMenuItem_Click(object sender, EventArgs e) {
            string targetPath = "";
            if (currentCellVal.Contains("_HonourMode"))
                targetPath = Path.Combine(Settings.Default.LarianSaveLoc, currentCellVal.Split('\\')[0]);
            else
                targetPath = Path.Combine(Settings.Default.LarianSaveLoc, currentCellVal);
            string path = Path.Combine(Settings.Default.BackupSaveLoc, currentCellVal);
            Program.Watcher.EnableRaisingEvents = false;
            foreach (var file in new DirectoryInfo(targetPath).GetFiles()) {
                file.Delete();
            }
            foreach (var file in new DirectoryInfo(path).GetFiles()) {
                using (var backupSave = SafeFileHandle.WaitForFile(file.FullName)) {
                    if (backupSave is null) continue;
                    var saveName = file.Name;
                    string targetFile = Path.Combine(targetPath, saveName);
                    using (FileStream larianSave = File.Create(targetFile)) {
                        backupSave.CopyTo(larianSave);
                    }
                }
            }
            Program.Watcher.EnableRaisingEvents = true;
        }
    }
}
