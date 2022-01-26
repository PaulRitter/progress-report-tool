using System.Net;

namespace ProgRepTools;

public class Project
{
    public readonly string Name;
    public DateTime From;
    public DateTime To;
    public DateTime LastCheck;
    public List<String> Repos;

    private static string[] targets = { "from", "to", "lastCheck", "repos" };

    private Project(string name, DateTime from, DateTime to, DateTime? lastCheck = null, List<string>? repos = null)
    {
        Name = name;
        From = from;
        To = to;
        LastCheck = lastCheck ?? from;
        Repos = repos ?? new List<string>();
    }

    public void Edit(string? target)
    {
        Console.WriteLine($"Editing Project {Name}");
        target ??= ConsoleUtils.Prompt<string>($"What do you want to edit? ({string.Join("/", targets)})", TryParseTarget);
        switch (target)
        {
            case "from":
                From = ConsoleUtils.Prompt<DateTime>("Enter date:", TryParseFromDate);
                break;
            case "to":
                To = ConsoleUtils.Prompt<DateTime>("Enter date:", TryParseToDate);
                break;
            case "lastCheck":
                LastCheck = ConsoleUtils.Prompt<DateTime>("Enter date:", TryParseCheckDate);
                break;
            case "repos":
                var action = ConsoleUtils.Prompt<string>("Enter action: (remove/add)", TryParseRepoAction);
                if (action == "remove")
                {
                    var removeTarget = ConsoleUtils.Prompt<string>("Enter repo to remove:", TryParseRepoToRemove);
                    Repos.Remove(removeTarget);
                    break;
                }
                
                var repo = ConsoleUtils.Prompt<string>("Enter repo to add:", TryParseRepo);
                Repos.Add(repo);
                break;
        }
    }

    private bool TryParseRepoToRemove(string raw, out string repo)
    {
        repo = raw;
        return Repos.Contains(repo);
    }

    private bool TryParseRepoAction(string raw, out string action)
    {
        action = raw;
        return raw is "remove" or "add";
    }

    private bool TryParseFromDate(string raw, out DateTime value)
    {
        if (!DateTime.TryParse(raw, out value))
            return false;
        return value <= To;
    }
    
    private bool TryParseToDate(string raw, out DateTime value)
    {
        if (!DateTime.TryParse(raw, out value))
            return false;
        return value >= From;
    }
    
    private bool TryParseCheckDate(string raw, out DateTime value)
    {
        if (!DateTime.TryParse(raw, out value))
            return false;
        return value >= From && value <= To;
    }

    private bool TryParseTarget(string val, out string value)
    {
        value = val;
        return targets.Contains(val);
    }

    private bool TryParseRepo(string val, out string repo)
    {
        repo = val;
        var url = $"https://api.github.com/repos/{val}";
        var client = new HttpClient();
        var response = client.Send(new HttpRequestMessage(HttpMethod.Get, url));

        return response.IsSuccessStatusCode;
    }

    public void Save()
    {
        var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.prt/{Name}.yml";
        using (var f = File.OpenWrite(path))
        {
            //todo yaml
        }
    }

    public static Project Load(string name)
    {
        var rootDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.prt";
        Directory.CreateDirectory(rootDir);
        var path = $"{rootDir}/{name}.yml";
        if (File.Exists(path))
        {
            //todo yaml
            return null;
        }

        DateTime from;
        DateTime to;
        do
        {
            from = ConsoleUtils.Prompt<DateTime>("From which timestamp?", DateTime.TryParse);
            to = ConsoleUtils.Prompt<DateTime>("To which timestamp?", DateTime.TryParse);    
        } while (from > to);
        
        return new Project(name, from, to);
    }
}