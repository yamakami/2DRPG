using UnityEngine;


[CreateAssetMenu(fileName = "_Item", menuName = "Command/Item")]
public class Item : ScriptableObject, ICommand
{
    public CommandType.type commandType;
    public enum Itemtype {
        Healing,
        Attacking,
    }

    [SerializeField] Itemtype itemType;
    [SerializeField] string nameKana;
    [SerializeField] AudioClip sound;
    [SerializeField] int price = 0;
    [SerializeField] int sellPrice = 0;
    [SerializeField] int value = 0;
    [SerializeField] int player_possession_limit = 0;
    [SerializeField] int player_possession_count = 0;

    [TextArea(2, 5)]
    [SerializeField] string description;

    public string CommadName => name;
    public CommandType.type CommandType => commandType;

    public string NameKana { get => nameKana; }
    public AudioClip Sound { get => sound; }
    public int Price { get => price; }
    public int SellPrice { get => sellPrice; }
    public int Value { get => value; }
    public int Player_possession_limit { get => player_possession_limit; set => player_possession_limit = value; }
    public int Player_possession_count { get => player_possession_count; set => player_possession_count = value; }
    public string Description { get => description; }

    public void AddCommand()
    {
        Player_possession_count++;
    }

    public void RemoveCommand()
    {
        if(0 < Player_possession_count) Player_possession_count--;
    }

    // public void Excute(){}
}
