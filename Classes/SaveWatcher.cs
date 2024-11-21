using BG3_Save_Backup.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BG3_Save_Backup.Classes {
    internal class SaveWatcher {
        private string _savepath;
        private string _backuppath;
        private FileSystemWatcher _watcher;
        private bool _enabled;
        public string LarianPath {
            get {
                return _savepath;
            } set {
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
            _watcher = new FileSystemWatcher(newPath);
            _watcher.NotifyFilter = NotifyFilters.CreationTime
                                  | NotifyFilters.DirectoryName
                                  | NotifyFilters.FileName
                                  | NotifyFilters.LastAccess
                                  | NotifyFilters.LastWrite;
            _watcher.Created += OnCreated;
            _watcher.Changed += OnChanged;
            _watcher.EnableRaisingEvents = true;
            //_watcher.IncludeSubdirectories = true;
            _enabled = true;
        }
        private void OnCreated(object sender, FileSystemEventArgs e) {
            if (!Directory.Exists(e.FullPath)) return;
            string targetPath = Path.Combine(_backuppath, e.Name);
            _ = Directory.CreateDirectory(targetPath);
            if (e.Name.EndsWith("_HonourMode")) {
                targetPath = Path.Combine(targetPath, DateTime.Now.ToString("ddMMMyyyyHHmm"));
                _ = Directory.CreateDirectory(targetPath);
            }
            foreach (var file in new DirectoryInfo(e.FullPath).GetFiles()) {
                using (var larianSave = WaitForFile(file.FullName)) {
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
            string targetPath = Path.Combine(_backuppath, e.Name);
            _ = Directory.CreateDirectory(targetPath);
            if (e.Name.EndsWith("_HonourMode")) {
                targetPath = Path.Combine(targetPath, DateTime.Now.ToString("ddMMMyyyyHHmm"));
                _ = Directory.CreateDirectory(targetPath);
            }
            foreach (var file in new DirectoryInfo(e.FullPath).GetFiles()) {
                using (var larianSave = WaitForFile(file.FullName)) {
                    var saveName = file.Name;
                    string targetFile = Path.Combine(targetPath, saveName);
                    using (FileStream backupSave = new FileStream(targetFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite)) {
                        larianSave.CopyTo(backupSave);
                    }
                }
            }
            BackupTriggered?.Invoke(this, EventArgs.Empty);
        }
        private FileStream WaitForFile(string fullpath) {
            for (int numTries = 0; numTries < 100; numTries++) {
                FileStream fs = null;
                try {
                    fs = new FileStream(fullpath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    return fs;
                } catch (IOException) {
                    if (fs != null)
                        fs.Dispose();
                    Thread.Sleep(250);
                }
            }
            return null;
        }
    }
}
