using UnityEngine;

[CreateAssetMenu(fileName = "_EquipItem", menuName = "Command/EquipItem")]

public class EquipItem : CommandItem
{    
     public enum EQUIP_POSITION
    {
        NONE = -1,
        ARMS = 0,
        SHIELD = 1, 
        HEAD = 2,
        BODY = 3,
        HAND = 4,
        LEG = 5,
    }
 
    public EQUIP_POSITION equip_position;
    public bool equiped;

    public override string ActionMessage()
    {
        return $"{{0}}は#{this.GetNameKana()}を使った";
    }

    public override string AffectMessage(bool result = true)
    {
        return "何も起こらなかった";
    }

    public override void Consume(IStatus userStatus, IStatus targetStatus = null)　{}
}
