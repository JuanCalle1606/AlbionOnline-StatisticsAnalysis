using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
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
        if (!File.Exists(path))
        {
            _blacklist = [];
            return false;
        }
        
        var json = await File.ReadAllTextAsync(path);
        _blacklist = JsonSerializer.Deserialize<List<BlacklistJsonObject>>(json);
        
        return true;
    }

    public static BlacklistJsonObject GetBlacklistData(string name)
    {
        return _blacklist.Find(x => string.Equals(name, x.Name, StringComparison.CurrentCultureIgnoreCase));
    }
    
}