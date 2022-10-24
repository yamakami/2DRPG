using UnityEngine;


[CreateAssetMenu(fileName = "_Item", menuName = "Command/Item")]
public class Item : ScriptableObject, ICommand
{
    public CommandType.type commandType;
    public enum Itemtype {
        Healing,
        Attacking,
    }

    public Itemtype itemType;
    public string nameKana;
    public AudioClip sound;
    public int price = 0;
    public int sellPrice = 0;
    public int value = 0;
    public int player_possession_limit = 0;
    public int player_possession_count = 0;
    [TextArea(2, 5)]
    public string description;

    public string CommadName => name;
    public CommandType.type CommandType => commandType;

    public void AddCommand()
    {
        player_possession_count++;
    }

    public void RemoveCommand()
    {
        if(0 < player_possession_count) player_possession_count--;
    }

    // public void Excute(){}
}
