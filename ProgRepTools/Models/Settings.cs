namespace ProgRepTools.Models;

public class Settings
{
    //todo sync in setter
    
    private Dictionary<string, string> _usernameMap;

    public Dictionary<string, string> UsernameMap
    {
        get => _usernameMap;
        set => _usernameMap = value;
    }
}