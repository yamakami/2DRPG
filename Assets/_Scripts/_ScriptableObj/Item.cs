using System.Collections;
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

    public enum EQUIP_TYPE
    {
        NONE,
        SWORD,
        SHIELD,
        ARMOR,
        HELMET,
        GLOVE,
        BOOTS
    }

    public ITEM_TYPE itemType;
    public HEALING_TYPE healingType;
    public EQUIP_TYPE equipType;
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

    public int HealAffectValue(PlayerAction playerAction)
    {
        var hp = playerAction.hp;
        var maxHP = playerAction.maxHP;
        var total = Mathf.Clamp(hp + point, 0, maxHP);

        return total - hp;
    }

    public void Consume(PlayerInfo playerInfo)
    {
        player_possession_count--;

        if(player_possession_count < 1)
            playerInfo.items.Remove(this);
    }
}
