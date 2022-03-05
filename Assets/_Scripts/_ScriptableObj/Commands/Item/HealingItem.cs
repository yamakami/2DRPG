using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

[CreateAssetMenu(fileName = "_HealingItem", menuName = "Command/HealingItem")]

public class HealingItem : CommandItem
{    
    public enum HEALING_TYPE
    {
        HP,
        MP
    }
 
    public HEALING_TYPE healingType;

    public override string AffectMessage(bool result = true)
    {
        var prefix = (healingType == HEALING_TYPE.HP)?  HEALING_TYPE.HP : HEALING_TYPE.MP;

        var message = messageNothingHappen;
        if(result) message = $"{{0}}は{prefix.ToString()}が{affectValue.ToString()}回復した";

        return message;
    }

    void HealHp(IStatus status)
    {
        var targetHP = status.HP;
        affectValue = Mathf.Clamp(targetHP + value, 0, status.MaxHP) - targetHP;
        status.HP += affectValue;
    }

    void HealMp(IStatus status)
    {
        var targetMP = status.MP;
        affectValue = Mathf.Clamp(targetMP + value, 0, status.MaxMP) - targetMP;
        status.MP += affectValue;
    }

    public override void Consume(IStatus userStatus, IStatus targetStatus)
    {
        base.Consume(userStatus);

        if (healingType == HEALING_TYPE.HP)
            HealHp(targetStatus);
        else
            HealMp(targetStatus);
    }
}
