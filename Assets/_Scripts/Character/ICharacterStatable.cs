public interface ICharacterStatable
{
    public string Name { get; }
    public int MaxHP { get; }
    public int MaxMP { get; }
    public int Hp { get; set; }
    public int Mp { get; set; }
}