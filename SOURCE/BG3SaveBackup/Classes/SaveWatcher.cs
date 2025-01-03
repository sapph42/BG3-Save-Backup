﻿namespace BG3SaveBackup.Classes;

internal class SaveWatcher {
	private string _savepath;
	private string _backuppath;
	private FileSystemWatcher? _watcher;
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
	public event EventHandler? BackupTriggered;
	public bool EnableRaisingEvents {
		get {
			if (_watcher is null) return false;
			return _watcher.EnableRaisingEvents;
		}
		set {
			if (_watcher is null) throw new NullReferenceException();
			_watcher.EnableRaisingEvents = value;
		}
	}
	public SaveWatcher() : this(Settings.Default.LarianSaveLoc, Settings.Default.BackupSaveLoc) { }
	public SaveWatcher(string LarianPath, string BackupPath) {
		_savepath = LarianPath;
		_backuppath = BackupPath;
		ResetWatcher();
	}
	private void ResetWatcher() {
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
		if (e.Name is null || e.FullPath is null)
			return;
		if (!Directory.Exists(e.FullPath)) return;
		if (Settings.Default.HonorOnly && !e.Name.Contains("_HonourMode")) return;
		FileWriter(e.FullPath, true);
		BackupTriggered?.Invoke(this, EventArgs.Empty);
	}
	private void OnChanged(object sender, FileSystemEventArgs e) {
		if (e.Name is null || e.FullPath is null)
			return;
		if (!Directory.Exists(e.FullPath)) return;
		if (Settings.Default.HonorOnly && !e.Name.Contains("_HonourMode")) return;
		FileWriter(e.FullPath, false);
		BackupTriggered?.Invoke(this, EventArgs.Empty);
	}
	private void FileWriter(string sourcePath, bool createNew) {
		DirectoryInfo sourceDir = new(sourcePath);
		var files = sourceDir.GetFiles();
		var lsvFile = files.Where(f => f.Extension.Equals(".lsv", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
		var webpFile = files.Where(f => f.Extension.Equals(".webp", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
		if (lsvFile is null)
			return;
		string? gameSessionId;
		string? leaderName;
		using (var larianSave = SafeFileHandle.WaitForFile(lsvFile.FullName)) {
			if (larianSave is null) return;
			var tempFolder = Path.GetTempPath();
			var tempFile = Path.Combine(tempFolder, lsvFile.Name);
			using var tempSave = new FileStream(tempFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
			larianSave.CopyTo(tempSave);
			tempSave.Flush();
			tempSave.Close();
            BG3SaveData saveData = new(tempFile);
            saveData.ParseSaveData();
			gameSessionId = saveData.GameId;
			leaderName = saveData.LeaderName;
		}
		string destinationPath = Settings.Default.BackupSaveLoc;
		if (gameSessionId is null) {
			destinationPath = Path.Combine(destinationPath, sourceDir.Name);
			if (sourcePath.EndsWith("_HonourMode"))
				destinationPath = Path.Combine(destinationPath, DateTime.Now.ToString("ddMMMyyyyHHmm"));
		} else {
			destinationPath = Path.Combine(Settings.Default.BackupSaveLoc, $"{leaderName} - {gameSessionId}");
			if (sourcePath.EndsWith("_HonourMode"))
				destinationPath = Path.Combine(destinationPath, sourceDir.Name, DateTime.Now.ToString("ddMMMyyyyHHmm"));
			else
				destinationPath = Path.Combine(destinationPath, sourceDir.Name);
		}
		foreach (var file in files) {
			if (file.Extension == ".old") continue;
			using var larianSave = SafeFileHandle.WaitForFile(file.FullName);
			if (larianSave is null) return;
			var saveName = file.Name;
			if (!Directory.Exists(destinationPath))
				Directory.CreateDirectory(destinationPath);
			string targetFile = Path.Combine(destinationPath, saveName);
			if (createNew) {
				var backupSave = File.Create(targetFile);
				larianSave.CopyTo(backupSave);
			} else {
				using var backupSave = new FileStream(targetFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				larianSave.CopyTo(backupSave);
			}
			
        }
	}
}