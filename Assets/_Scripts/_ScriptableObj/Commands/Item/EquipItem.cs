using UnityEngine;

[CreateAssetMenu(fileName = "_EquipItem", menuName = "Command/EquipItem")]

public class EquipItem : CommandItem
{    
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
 
    public EQUIP_POSITION equip_position;

    // public override string ActionMessage()
    // {
    //     return $"{0}は#{this.GetNameKana()}を使った";
    // }

    // public override string AffectMessage(bool result = true)
    // {
    //     // var prefix = (healingType == HEALING_TYPE.HP)?  HEALING_TYPE.HP : HEALING_TYPE.MP;

    //     // var message = messageNothingHappen;
    //     // if(result) message = $"{0}は#{prefix}が{1}回復した";

    //     return "";
    // }
}
