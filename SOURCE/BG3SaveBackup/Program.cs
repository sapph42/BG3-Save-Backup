using System.Reflection;

namespace BG3SaveBackup;
internal static class Program {
	private static readonly object _lock = new();
	private static Settings _default = Settings.Default;
	public static SaveWatcher? Watcher;
	public static bool ValidBackupTarget;
	static bool FirstTimeRun() {
		string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		string MyDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		_default.LarianSaveLoc = _default.LarianSaveLoc.NullIfWhiteSpace() ?? $"{LocalAppData}{Resources.DefaultSavePath}";
		_default.BackupSaveLoc = _default.BackupSaveLoc.NullIfWhiteSpace() ?? $"{MyDocs}\\{Resources.DefaultBackupFolder}";
		_default.Save();
		return CreateSaveFolder(_default.BackupSaveLoc);
	}
	static bool CreateSaveFolder(string path) {
		if (Directory.Exists(path)) return true;
		try {
			Directory.CreateDirectory(path);
			return true;
		} catch {
			return false;
		}
	}
	static bool ValidateSettings() {
		if (_default.UpgradeRequired) {
			_default.Upgrade();
			_default.UpgradeRequired = false;
			_default.Save();
		}
		if (string.IsNullOrWhiteSpace(_default.LarianSaveLoc) || string.IsNullOrWhiteSpace(_default.BackupSaveLoc))
			FirstTimeRun();
		if (!Directory.Exists(_default.BackupSaveLoc))
			return CreateSaveFolder(_default.BackupSaveLoc);
		return true;
	}
	static void UnhandledException(object sender, UnhandledExceptionEventArgs args) {
		Exception e = (Exception)args.ExceptionObject;
		MessageBox.Show($"Unhandled exception caught:\r\n{e.Message}");
	}
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
    static void Main() {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
		//AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
		ValidBackupTarget = ValidateSettings();
		Watcher = new SaveWatcher();
		Application.Run(new Display());
    }
}
