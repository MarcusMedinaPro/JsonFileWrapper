using System.Text;
using MarcusMedinaPro.JsonFileWrapper;

Console.OutputEncoding = Encoding.UTF8;

var storeFolder = Path.Combine(Path.GetTempPath(), "json-wrapper-demo");
Directory.CreateDirectory(storeFolder);
var filePath = Path.Combine(storeFolder, "workspace-settings");

Console.WriteLine($"JsonFile wrapper demo (file: {filePath}.json)\n");

using (var workspaceFile = new JsonFile<WorkspaceSettings>(filePath))
{
    workspaceFile.Data ??= WorkspaceSettings.Empty();

    if (workspaceFile.Data.Environments.Count == 0)
    {
        workspaceFile.Data.Environments.Add(new EnvironmentConfig
        {
            Name = "dev",
            ApiUrl = "https://api.dev.local",
            FeatureFlags = new() { "beta-search" }
        });
    }

    workspaceFile.Data.LastOpened = DateTimeOffset.UtcNow;
    workspaceFile.Data.Tabs.Add($"notes-{workspaceFile.Data.Tabs.Count + 1}");

    Console.WriteLine("Current workspace snapshot:");
    Console.WriteLine($"- Last opened: {workspaceFile.Data.LastOpened:O}");
    Console.WriteLine($"- Tabs       : {string.Join(", ", workspaceFile.Data.Tabs)}");
    Console.WriteLine("- Environments:");
    foreach (var environment in workspaceFile.Data.Environments)
    {
        Console.WriteLine($"  • {environment.Name} → {environment.ApiUrl}");
    }

    workspaceFile.Save();
}

Console.WriteLine();
Console.WriteLine("Reloading file using implicit conversion...");
var reloaded = new JsonFile<WorkspaceSettings>(filePath);
WorkspaceSettings snapshot = reloaded;
Console.WriteLine($"Tabs restored: {string.Join(", ", snapshot.Tabs)}");

Console.WriteLine();
Console.WriteLine("Demo finished. Inspect the JSON file to see the structure.");

public sealed class WorkspaceSettings
{
    public DateTimeOffset LastOpened { get; set; }
    public List<string> Tabs { get; set; } = new();
    public List<EnvironmentConfig> Environments { get; set; } = new();

    public static WorkspaceSettings Empty() => new()
    {
        Tabs = new List<string> { "welcome" }
    };
}

public sealed class EnvironmentConfig
{
    public string Name { get; set; } = "";
    public string ApiUrl { get; set; } = "";
    public List<string> FeatureFlags { get; set; } = new();
}
