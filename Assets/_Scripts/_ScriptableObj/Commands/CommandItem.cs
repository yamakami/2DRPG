using UnityEngine;


[System.Serializable]
public class CommandItem : ScriptableObject
{
    public bool useForQuest;
    public bool useForBattle;

    public enum ITEM_TYPE
    {
        KEY,
        HEALING_ITEM,
        ATTACK_ITEM,
        EQUIP_ITEM,
        MISC
    }
    public string itemName;
    public string nameKana;

    public int price = 0;
    public int sellPrice = 0;
    public int player_possession_count = 0;
    public int player_possession_limit = 0;

    [TextArea(2, 5)]
    public string description;

    public string getName()
    {
        return itemName;
    }

    public string ActionMessage()
    {
        throw new System.NotImplementedException();
    }

    public string AffectMessage()
    {
        throw new System.NotImplementedException();
    }

    public void Consume()
    {
        throw new System.NotImplementedException();
    }
}
