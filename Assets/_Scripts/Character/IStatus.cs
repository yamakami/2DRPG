public interface IStatus
{
    public string CharacterName { get; }
    public int HP { get; set; }
    public int MP { get; set; }

    public int MaxHP { get; }
    public int MaxMP { get; }
}
