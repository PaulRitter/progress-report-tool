namespace ProgRepTools.Models;

public class ProgRepProject
{
    public readonly string Name;
    private DateTime _from;
    private DateTime _to;
    private List<Repository> _repositories;

    public DateTime From
    {
        get => _from;
        set => _from = value;
    }

    public DateTime To
    {
        get => _to;
        set => _to = value;
    }

    public IReadOnlyList<Repository> Repositories => _repositories;
}