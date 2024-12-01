using System;
using System.Text.RegularExpressions;
using LSLib.LS;
using LSLib.LS.Enums;

namespace BG3SaveBackup.Classes;
internal partial class BG3SaveData {
	public string FileName { get; set; }
	public string? GameId { get => _gameId; }
	public string? LeaderName {get => _leaderName; }
	public DateTime? SaveTimestamp { get => _saveTimestamp; }
	public string? Level { get => _level; }
	public string? SubLevel { get => _subLevel; }
	public int? Difficulty { get => _difficulty; }
	public TimeSpan? PlayTime { get => _playTime; }
	private string _targetPath;
	private string? _gameId;
	private string? _leaderName;
	private DateTime? _saveTimestamp;
	private string? _level;
	private string? _subLevel;
	private int? _difficulty;
	private TimeSpan? _playTime;
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
		Regex gamePattern = GameIdRegex();
		Regex namePattern = NameRegex();

        foreach (var line in File.ReadLines(metaXml)) {
			var gameMatch = GameIdRegex().Match(line);
			var nameMatch = NameRegex().Match(line);
			var saveMatch = TimestampRegex().Match(line);
			var levelMatch = LevelRegex().Match(line);
			var subLMatch = SubLRegex().Match(line);
			var diffMatch = DiffRegex().Match(line);
			var playTMatch = PlayTimeRegex().Match(line);
			if (gameMatch.Captures.Any())
				_gameId = gameMatch.Groups["gameid"].Value;
			else if (nameMatch.Captures.Any())
				_leaderName = nameMatch.Groups["name"].Value;
			else if (saveMatch.Captures.Any()) {
				if (!long.TryParse(saveMatch.Groups["savetime"].Value, out long epoch)) {
					DateTimeOffset offset = DateTimeOffset.FromUnixTimeSeconds(epoch);
					_saveTimestamp = offset.DateTime;
				}
			} else if (levelMatch.Captures.Any())
				_level = levelMatch.Groups["level"].Value;
			else if (subLMatch.Captures.Any())
				_subLevel = subLMatch.Groups["subl"].Value;
			else if (diffMatch.Captures.Any())
				try {
					_difficulty = int.Parse(diffMatch.Groups["diff"].Value);
				} catch { }
			else if (playTMatch.Captures.Any()) {
				if (int.TryParse(playTMatch.Groups["ptime"].Value, out int epoch)) {
					_playTime = new TimeSpan(0, 0, epoch);
				}
			}
        }
	}

    [GeneratedRegex(@"\s*<attribute id=""GameSessionID"" type=""FixedString"" value=""(?<gameid>[^""]*)""\s*/>")]
    private static partial Regex GameIdRegex();
    [GeneratedRegex(@"\s*<attribute id=""LeaderName"" type=""LSString"" value=""(?<name>[^""]*)""\s*/>")]
    private static partial Regex NameRegex();
	[GeneratedRegex(@"\s<attribute id=""SaveTime"" type=""uint64"" value=""(?<savetime>\d*)"" />")]
	private static partial Regex TimestampRegex();
	[GeneratedRegex(@"\s<attribute id=""Level"" type=""FixedString"" value=""(?<level>[^""]*)"" />")]
	private static partial Regex LevelRegex();
	[GeneratedRegex(@"\s<attribute id=""CurrentSubRegion"" type=""FixedString"" value=""(?<subl>[^""]*)"" />")]
	private static partial Regex SubLRegex();
	[GeneratedRegex(@"\s<attribute id=""Difficulty"" type=""uint8"" value=""(?<diff>\d)"" />")]
	private static partial Regex DiffRegex();
	[GeneratedRegex(@"\s<attribute id=""TimeStamp"" type=""uint32"" value=""(?<ptime>\d*)"" />")]
	private static partial Regex PlayTimeRegex();


						
					
}

