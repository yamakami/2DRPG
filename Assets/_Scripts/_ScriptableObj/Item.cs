using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public bool useForQuest;
    public bool useForBattle;

    public enum ITEM_TYPE
    {
        KEY,
        HEALING_ITEM,
        ATTACK_ITEM,
        DEFENCE_ITEM,
        MISC
    }

    public ITEM_TYPE itemType;
    public string itemName;
    public string nameKana;
    public int point = 0;
    public int price = 0;
    public int sellPrice = 0;
    public int player_possession_count = 0;
    public int player_possession_limit = 0;
    public AudioClip audioClip;
    [TextArea(2, 5)]
    public string description;
    [TextArea(2, 5)]
    public string resultMessage;
}
