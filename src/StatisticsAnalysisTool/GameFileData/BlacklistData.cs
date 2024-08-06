using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Serilog;
using StatisticsAnalysisTool.GameFileData.Models;

namespace StatisticsAnalysisTool.GameFileData;

public static class BlacklistData
{
    private static List<BlacklistJsonObject> _blacklist;
    
    public static bool IsBlacklisted(string name)
    {
        return _blacklist.Exists(x => string.Equals(name, x.Name, StringComparison.CurrentCultureIgnoreCase));
    }
    
    public static async Task<bool> LoadDataAsync()
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "blacklist.json");
        
        await UpdateFromGithubAsync(path);
        
        if (!File.Exists(path))
        {
            _blacklist = [];
            return false;
        }
        
        var json = await File.ReadAllTextAsync(path);
        _blacklist = JsonSerializer.Deserialize<List<BlacklistJsonObject>>(json);
        
        return true;
    }

    private static async Task UpdateFromGithubAsync(string path)
    {
        try
        {
            var url = "https://raw.githubusercontent.com/JuanCalle1606/AlbionOnline-StatisticsAnalysis/blacklist/src/StatisticsAnalysisTool/blacklist.json";
        
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
        
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
        
            var json = await response.Content.ReadAsStringAsync();
            await File.WriteAllTextAsync(path, json);
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to update blacklist data from github.");
        }
    }

    public static BlacklistJsonObject GetBlacklistData(string name)
    {
        return _blacklist.Find(x => string.Equals(name, x.Name, StringComparison.CurrentCultureIgnoreCase));
    }
    
}