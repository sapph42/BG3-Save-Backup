using System.Text.RegularExpressions;
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
			} else {
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
		} catch (InvalidDataException exc) {
		} catch (Exception exc) {
		}
		Regex gamePattern = new(@"\s*<attribute id=""GameID"" type=""FixedString"" value=""(?<gameid>[^""]*)""\s*/>");
		Regex namePattern = new(@"\s*<attribute id=""LeaderName"" type=""LSString"" value=""(?<name>[^""]*)""\s*/>");

        foreach (var line in File.ReadLines(metaXml)) {
			var gameMatch = gamePattern.Match(line);
			var nameMatch = namePattern.Match(line);
			if (gameMatch.Captures.Count + nameMatch.Captures.Count == 0) 
				 continue;
			if (gameMatch.Captures.Count > 0)
				_gameId = gameMatch.Groups["gameid"].Value;
			if (nameMatch.Captures.Count > 0)
				_leaderName = nameMatch.Groups["name"].Value;
			Console.WriteLine($"{_gameId} : {_leaderName}");	
        }
	}
}

