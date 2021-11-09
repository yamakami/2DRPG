using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
[System.Serializable]
public class Item : ScriptableObject
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

    public enum HEALING_TYPE
    {
        NONE,
        HP,
        MP
    }

    public enum EQUIP_POSITION
    {
        NONE = -1,
        HEAD = 0,
        BODY = 1,
        HAND = 2,
        LEG = 3,
        SHIELD = 4,
        ARM = 5,
    }

    public bool isEquip;
    public ITEM_TYPE itemType;
    public HEALING_TYPE healingType;
    public EQUIP_POSITION equipPosition;
    public string itemName;
    public string nameKana;
    public int point = 0;
    public int price = 0;
    public int sellPrice = 0;
    public int player_possession_count = 0;
    public int player_possession_limit = 0;
    public bool hasAnimationClip;
    public AudioClip audioClip;

    [TextArea(2, 5)]
    public string description;
    [TextArea(2, 5)]
    public string resultMessage;

    public int AffectValue()
    {
        return point;
    }

    public int HealAffectValue(int hp, int maxHP, int mp, int maxMP)
    {
        var val = hp;
        var maxVal = maxHP;

        if(healingType == Item.HEALING_TYPE.MP)
        {
            val = mp;
            maxVal = maxMP;
        }

        var total = Mathf.Clamp(val + point, 0, maxVal);

        return total - val;
    }

    public void Consume(PlayerInfo playerInfo)
    {
        player_possession_count--;

        if(player_possession_count < 1)
            playerInfo.items.Remove(this);
    }
}
