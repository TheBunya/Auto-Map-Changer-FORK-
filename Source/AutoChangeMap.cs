using System.Text.Json;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Timers;
using Timer = CounterStrikeSharp.API.Modules.Timers.Timer;

namespace AutoMapChanger;

public class AutoMapChanger : BasePlugin
{
    public override string ModuleName => "Auto Map Changer";
    public override string ModuleVersion => "1.1.0";
    public override string ModuleAuthor => "extm.exe";

    private Config _config = null!;
    private Timer? _timer;

    public override void Load(bool hotReload)
    {
        Log($"{ModuleName} [{ModuleVersion}] Plugin has been successfully loaded");

        LoadConfig();

        RegisterListener<Listeners.OnMapStart>(mapName => {
            if (_config.Debug)
                Log($"[ {ModuleName} ] Map {mapName} has started!");
            StartTimer();
        });
        RegisterEventHandler<EventGameEnd>((@event, info) =>
        {
            _timer?.Kill();
            _timer = null;
            return HookResult.Continue;
        });
    }

    public void StartTimer()
    {
        _timer?.Kill();

        var delaySeconds = _config.Delay > 0 ? _config.Delay : 180.0f;
        _timer = AddTimer(delaySeconds, MapChange, TimerFlags.REPEAT);

        if (_config.Debug)
            Log($"[ {ModuleName} ] Timer started with delay {delaySeconds} sec");
    }

    private void MapChange()
    {
        var configuredMap = _config.DefaultMap?.Trim() ?? string.Empty;
        var isWorkshopMap = configuredMap.StartsWith("ws:", StringComparison.OrdinalIgnoreCase);
        var targetMapForCommand = isWorkshopMap ? configuredMap[3..].Trim() : configuredMap;

        if (string.IsNullOrWhiteSpace(targetMapForCommand))
            return;

        var currentMap = NativeAPI.GetMapName() ?? string.Empty;
        if (NormalizeMapName(currentMap) == NormalizeMapName(configuredMap))
        {
            if (_config.Debug)
                Log($"[ {ModuleName} ] Skip change: already on target map \"{currentMap}\"");
            return;
        }

        var hasPlayers = Utilities.GetPlayers().Any(controller => controller is { IsValid: true, IsBot: false, IsHLTV: false });
        if (hasPlayers && !_config.ChangeMap)
        {
            if (_config.Debug)
                Log($"[ {ModuleName} ] Skip change: real players are online");
            return;
        }

        if (isWorkshopMap)
            Server.ExecuteCommand($"ds_workshop_changelevel {targetMapForCommand}");
        else
            Server.ExecuteCommand($"map {targetMapForCommand}");

        if (_config.Debug)
            Log($"[ {ModuleName} ] Map change to \"{targetMapForCommand}\"");
    }

    private static string NormalizeMapName(string mapName)
    {
        if (string.IsNullOrWhiteSpace(mapName))
            return string.Empty;

        var normalized = mapName.Trim();

        if (normalized.StartsWith("ws:", StringComparison.OrdinalIgnoreCase))
            normalized = normalized[3..];

        normalized = normalized.Replace('\\', '/');

        var lastSlashIndex = normalized.LastIndexOf('/');
        if (lastSlashIndex >= 0 && lastSlashIndex < normalized.Length - 1)
            normalized = normalized[(lastSlashIndex + 1)..];

        return normalized.Trim().ToLowerInvariant();
    }

    [ConsoleCommand("css_acm_reload", "Reload config AutoChangeMap")]
    public void ReloadACMConfig(CCSPlayerController? controller, CommandInfo command)
    {
        if (controller != null) return;

        LoadConfig();
        _timer?.Kill();
        _timer = null;
        StartTimer();

        Log($"[ {ModuleName} ] Loaded config success (map: {_config.DefaultMap}, delay: {_config.Delay})");
    }

    private void LoadConfig()
    {
        var configPath = Path.Combine(ModuleDirectory, "autochangemap.json");

        if (!File.Exists(configPath))
            CreateConfig(configPath);
        else
            _config = JsonSerializer.Deserialize<Config>(File.ReadAllText(configPath)) ?? new Config();

        if (_config.Delay <= 0)
            _config.Delay = 180.0f;

        if (_config.Debug)
            Log($"[ {ModuleName} ] Config loaded from {configPath} (delay: {_config.Delay}, map: {_config.DefaultMap}, changeMap: {_config.ChangeMap})");
    }

    private void CreateConfig(string configPath)
    {
        _config = new Config
        {
            Delay = 180.0f,
            DefaultMap = "de_dust2",
            ChangeMap = false,
            Debug = false
        };

        File.WriteAllText(configPath,
            JsonSerializer.Serialize(_config, new JsonSerializerOptions { WriteIndented = true }));

        Log($"[ {ModuleName} ] The configuration was successfully saved to a file: " + configPath);
    }

    public void Log(string message)
    {
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}

public class Config
{
    public float Delay { get; set; } = 180.0f;
    public string DefaultMap { get; set; } = "de_dust2";
    public bool ChangeMap { get; set; } = false;
    public bool Debug { get; set; }
}
