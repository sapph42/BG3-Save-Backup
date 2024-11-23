using BG3_Save_Backup.Properties;
using System;
using System.IO;
using System.Threading;

namespace BG3_Save_Backup.Classes {
    internal class SaveWatcher {
        private string _savepath;
        private string _backuppath;
        private FileSystemWatcher _watcher;
        private bool _enabled;
        public string LarianPath {
            get {
                return _savepath;
            }
            set {
                ResetWatcher(value);
                _savepath = value;
            }
        }
        public string BackupPath {
            get => _backuppath;
            set => _backuppath = value;
        }
        public bool Enabled => _enabled;
        public event EventHandler BackupTriggered;
        public bool EnableRaisingEvents {
            get {
                if (_watcher is null) return false;
                return _watcher.EnableRaisingEvents;
            }
            set {
                _watcher.EnableRaisingEvents = value;
            }
        }
        public SaveWatcher() : this(Settings.Default.LarianSaveLoc, Settings.Default.BackupSaveLoc) { }
        public SaveWatcher(string LarianPath, string BackupPath) {
            _savepath = LarianPath;
            _backuppath = BackupPath;
            ResetWatcher(LarianPath);
        }

        private void ResetWatcher(string newPath) {
            if (!Directory.Exists(newPath)) {
                _enabled = false;
                return;
            }
            _watcher = new FileSystemWatcher(newPath) {
                NotifyFilter = NotifyFilters.CreationTime
                                  | NotifyFilters.DirectoryName
                                  | NotifyFilters.FileName
                                  | NotifyFilters.LastAccess
                                  | NotifyFilters.LastWrite,
                EnableRaisingEvents = true
            };
            _watcher.Created += OnCreated;
            _watcher.Changed += OnChanged;
            _enabled = true;
        }
        private void OnCreated(object sender, FileSystemEventArgs e) {
            if (!Directory.Exists(e.FullPath)) return;
            if (Settings.Default.HonorOnly && !e.Name.Contains("_HonourMode")) return;
            string targetPath = Path.Combine(_backuppath, e.Name);
            _ = Directory.CreateDirectory(targetPath);
            if (e.Name.EndsWith("_HonourMode")) {
                targetPath = Path.Combine(targetPath, DateTime.Now.ToString("ddMMMyyyyHHmm"));
                _ = Directory.CreateDirectory(targetPath);
            }
            foreach (var file in new DirectoryInfo(e.FullPath).GetFiles()) {
                using (var larianSave = SafeFileHandle.WaitForFile(file.FullName)) {
                    if (larianSave is null) return;
                    var saveName = file.Name;
                    string targetFile = Path.Combine(targetPath, saveName);
                    using (FileStream backupSave = File.Create(targetFile)) {
                        larianSave.CopyTo(backupSave);
                    }
                }
            }
            BackupTriggered?.Invoke(this, EventArgs.Empty);
        }
        private void OnChanged(object sender, FileSystemEventArgs e) {
            if (!Directory.Exists(e.FullPath)) return;
            if (Settings.Default.HonorOnly && !e.Name.Contains("_HonourMode")) return;
            string targetPath = Path.Combine(_backuppath, e.Name);
            _ = Directory.CreateDirectory(targetPath);
            if (e.Name.EndsWith("_HonourMode")) {
                targetPath = Path.Combine(targetPath, DateTime.Now.ToString("ddMMMyyyyHHmm"));
                _ = Directory.CreateDirectory(targetPath);
            }
            foreach (var file in new DirectoryInfo(e.FullPath).GetFiles()) {
                using (var larianSave = SafeFileHandle.WaitForFile(file.FullName)) {
                    if (larianSave is null) return;
                    var saveName = file.Name;
                    string targetFile = Path.Combine(targetPath, saveName);
                    using (FileStream backupSave = new FileStream(targetFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite)) {
                        larianSave.CopyTo(backupSave);
                    }
                }
            }
            BackupTriggered?.Invoke(this, EventArgs.Empty);
        }
    }
}
