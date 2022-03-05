using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;

[CreateAssetMenu(fileName = "_MagicHeal", menuName = "Command/MagicHeal")]
public class CommandMagicHeal : CommandMagic
{
    public override string AffectMessage(bool result = true)
    {
         return $"{{0}}はHPが{affectValue.ToString()}回復した";
    }

    public override void Consume(IStatus userStatus, IStatus targetStatus = null)
    {
        userStatus.MP -= consumptionMp;

        var targetHP = targetStatus.HP;
        affectValue= Mathf.Clamp(targetHP + GetAffectValue(), 0, targetStatus.MaxHP) - targetHP;
        targetStatus.HP += affectValue;
    }
}
