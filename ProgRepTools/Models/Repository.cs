namespace ProgRepTools.Models;

public class Repository
{
    //todo sync to file in setter
    //todo bulk edit methods to avoid multiple syncs per write
    
    private string _repositoryUrl;
    private List<ushort> _definites;
    private List<ushort> _maybies;
    private List<ushort> _excluded;

    public string RepositoryUrl
    {
        get => _repositoryUrl;
        set => _repositoryUrl = value;
    }

    public List<ushort> Definites
    {
        get => _definites;
        set => _definites = value;
    }

    public List<ushort> Maybies
    {
        get => _maybies;
        set => _maybies = value;
    }

    public List<ushort> Excluded
    {
        get => _excluded;
        set => _excluded = value;
    }WW
}