using System.Text.Json.Serialization;

namespace StatisticsAnalysisTool.GameFileData.Models;

public class BlacklistJsonObject
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("reason")]
    public string Reason { get; set; }
    [JsonPropertyName("reporter")]
    public string Reporter { get; set; }
    [JsonPropertyName("bad")]
    public bool IsBadPlayer { get; set; }
}