using System.ComponentModel;
using System.Data;
using System.Text;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace BG3SaveBackup.Forms;
public partial class Display : Form {
    private string? currentNodePath;
    private string? currentNodeFolder;
    private TreeNode root = new(Settings.Default.BackupSaveLoc);
    public Display() {
        InitializeComponent();
        if (Program.Watcher is null)
            Program.Watcher = new SaveWatcher();
        if (Program.ValidBackupTarget && Program.Watcher.Enabled)
            Tray.Icon = Resources.DefaultIcon;
        else
            Tray.Icon = Resources.ErrorIcon;
        LarianFolderTextbox.Text = Settings.Default.LarianSaveLoc;
        BackupFolderTextbox.Text = Settings.Default.BackupSaveLoc;
        Program.Watcher.BackupTriggered += WatcherTrigger;
    }
    private void Status_Load(object sender, EventArgs e) {
        RefreshTree();
    }
    private void Status_Shown(object sender, EventArgs e) {
        RefreshTree();
    }
    private void WatcherTrigger(object? sender, EventArgs e) {
        RefreshTree();
    }
    private void RefreshTree() {
        if (SaveTree.InvokeRequired) {
            Action safeRefresh = delegate {
                RefreshTree();
            };
            SaveTree.Invoke(safeRefresh);
        } else {
            SaveTree.BeginUpdate();
            SaveTree.Nodes.Clear();
            BuildTree(new DirectoryInfo(BackupFolderTextbox.Text), SaveTree.Nodes);
            SaveTree.EndUpdate();
        }
    }
    private void BuildTree(DirectoryInfo dir, TreeNodeCollection nodeCol) {
        TreeNode curNode = nodeCol.Add(dir.Name);
        DirectoryInfo[] dirList = dir.GetDirectories();
        foreach (DirectoryInfo subDir in dirList) {
            BuildTree(subDir, curNode.Nodes);
        }
    }
    private void Status_Resize(object sender, EventArgs e) {
        if (WindowState == FormWindowState.Minimized) {
            Hide();
        }
    }
    private void Tray_DoubleClick(object sender, EventArgs e) {
        RefreshTree();
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
            Program.Watcher!.LarianPath = dialog.FileName;
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
            Program.Watcher!.BackupPath = dialog.FileName;
        }
    }
    private void BackupFolderTextbox_DoubleClick(object sender, EventArgs e) {
        BackupFolderBrowse_Click(sender, e);
    }
    private void statusToolStripMenuItem_Click(object sender, EventArgs e) {
        RefreshTree();
        Show();
        WindowState = FormWindowState.Normal;
    }
    private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
        Close();
    }
    private void SaveTree_MouseUp(object sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Right) return;
        var currentMouseOverNode = SaveTree.GetNodeAt(e.X, e.Y);
        if (currentMouseOverNode is null || currentMouseOverNode.Nodes.Count > 0)
            return;
        GetNodeData(currentMouseOverNode);
        BackupAction.Show((Control)sender, new Point(e.X, e.Y));
    }
    private void GetNodeData(TreeNode node) {
        string path;
        TreeNode thisNode = node;
        currentNodeFolder = thisNode.Text;
        List<string> folders = [];
        while (thisNode.Parent is not null) {
            folders.Add(thisNode.Text);
            thisNode = thisNode.Parent;
        }
        folders.Add(BackupFolderTextbox.Text);
        folders.Reverse();
        path = Path.Combine([.. folders]);
        currentNodePath = path;
    }
    private void SaveTree_AfterSelect(object sender, TreeViewEventArgs e) {
        if (e.Node is null) return;
        GetNodeData(e.Node);
        string? imagePath = Directory
            .GetFiles(currentNodePath!)
            .Where(f => f.EndsWith("WebP"))
            .FirstOrDefault();
        if (imagePath is null) {
            byte[] imageData = File.ReadAllBytes(imagePath);
            ScreenshotImage.Image = WebP.DecodeFromBytes(imageData, imageData.Length);
            ScreenshotImage.SizeMode = PictureBoxSizeMode.StretchImage;
        } else {
            ScreenshotImage.Image = null;
        }
        string? savePath = Directory
            .GetFiles(currentNodePath!)
            .Where(f => f.EndsWith("lsv"))
            .FirstOrDefault();
        if (savePath is null) {
            SaveDataTextbox.Text = "";
            return;
        }
        BG3SaveData saveData = new(savePath);
        saveData.ParseSaveData();
        StringBuilder sb = new();
        sb.AppendLine($"Current Zone: {saveData.Level}");
        sb.AppendLine($"Current Subzone: {saveData.SubLevel}");
        if (saveData.SaveTimestamp is not null) {
            DateTime timestamp = (DateTime)saveData.SaveTimestamp;
            sb.AppendLine($"Save Timestamp: {timestamp.ToString("F")}");
        }
        sb.AppendLine($"Current Difficulty: {saveData.Difficulty}");
        sb.AppendLine($"Current Play Time: {ToReadableString(saveData.PlayTime)}");
        SaveDataTextbox.Text = sb.ToString();
    }
    private string ToReadableString(TimeSpan? span) {
        if (span is null) return "";
        TimeSpan goodSpan = (TimeSpan)span;
        string formatted = string.Format("{0}{1}{2}{3}",
            goodSpan.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", goodSpan.Days, goodSpan.Days == 1 ? string.Empty : "s") : string.Empty,
            goodSpan.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", goodSpan.Hours, goodSpan.Hours == 1 ? string.Empty : "s") : string.Empty,
            goodSpan.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", goodSpan.Minutes, goodSpan.Minutes == 1 ? string.Empty : "s") : string.Empty,
            goodSpan.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", goodSpan.Seconds, goodSpan.Seconds == 1 ? string.Empty : "s") : string.Empty);

        if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

        if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

        return formatted;
    }
    private void deleteBackupToolStripMenuItem_Click(object sender, EventArgs e) {
        try {
            Directory.Delete(currentNodePath!, true);
            RefreshTree();
        } catch {

        }
    }
    private void restoreBackupToolStripMenuItem_Click(object sender, EventArgs e) {
        string targetPath = "";
        if (currentNodePath is null || currentNodeFolder is null)
            return;
        if (currentNodePath.Contains("_HonourMode"))
            targetPath = Path.Combine(Settings.Default.LarianSaveLoc, currentNodeFolder.Split('\\')[0]);
        else
            targetPath = Path.Combine(Settings.Default.LarianSaveLoc, currentNodeFolder);
        Program.Watcher!.EnableRaisingEvents = false;
        foreach (var file in new DirectoryInfo(targetPath).GetFiles()) {
            file.Delete();
        }
        foreach (var file in new DirectoryInfo(currentNodePath).GetFiles()) {
            using var backupSave = SafeFileHandle.WaitForFile(file.FullName);
            if (backupSave is null) continue;
            var saveName = file.Name;
            string targetFile = Path.Combine(targetPath, saveName);
            using FileStream larianSave = File.Create(targetFile);
            backupSave.CopyTo(larianSave);
        }
        Program.Watcher.EnableRaisingEvents = true;
    }

    private void RefreshNow_Click(object sender, EventArgs e) {
        RefreshTree();
    }
}