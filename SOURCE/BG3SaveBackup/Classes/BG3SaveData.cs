using LSLib.LS;
using LSLib.LS.Enums;

namespace BG3SaveBackup.Classes;
internal class BG3SaveData {
	public string FileName { get; set; }
	public string? GameId { get => _gameId; }
	public string? LeaderName {get => _leaderName; }
	private string _targetPath;
	private string? _gameId;
	private string? _leaderName;
	public BG3SaveData(string fileName) {
		FileName = fileName;
		_targetPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(fileName));
	}
	public void ParseSaveData() {
		var packager = new Packager();
		try {
			packager.UncompressPackage(FileName, _targetPath);
		} catch (NotAPackageException) {
			if (ModPathVisitor.archivePartRe.IsMatch(Path.GetFileName(FileName))) {
				MessageBox.Show($"The specified file is part of a multi-part package; only the first part needs to be extracted.", "Extraction Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			} else {
				MessageBox.Show($"The specified file ({FileName}) is not an PAK package or savegame archive.", "Extraction Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		string metaFile = Path.Combine(_targetPath, "meta.lsf");
		string metaXml = Path.Combine(_targetPath, "meta.xml");
        try {
			var loadParams = ResourceLoadParameters.FromGameVersion(Game.BaldursGate3);
			loadParams.ByteSwapGuids = true;
			var resource = ResourceUtils.LoadResource(metaFile, loadParams);
			var conversionParams = ResourceConversionParameters.FromGameVersion(Game.BaldursGate3);
			ResourceUtils.SaveResource(resource, metaXml, ResourceFormat.LSX, conversionParams);
			MessageBox.Show("Resource saved successfully.");
		} catch (InvalidDataException exc) {
			MessageBox.Show($"Unable to convert resource.{Environment.NewLine}{Environment.NewLine}{exc.Message}", "Conversion Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		} catch (Exception exc) {
			MessageBox.Show($"Internal error!{Environment.NewLine}{Environment.NewLine}{exc}", "Conversion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		System.Xml.XmlDocument metaData = new();
		metaData.LoadXml(metaFile);
		_gameId = metaData.GetElementById("GameID")?.GetAttribute("value").ToString();
		_leaderName = metaData.GetElementById("LeaderName")?.GetAttribute("value").ToString();
	}
}

