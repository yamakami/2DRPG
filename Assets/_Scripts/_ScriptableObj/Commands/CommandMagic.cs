using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;

[CreateAssetMenu(fileName = "_Magic", menuName = "Command/Magic")]
public class CommandMagic : ScriptableObject, ICommand
{
    public bool useForQuest;
    public bool useForBattle;

    public enum MAGIC_TYPE
    {
        HEAL,
        ATTACK,
        DEFENCE,
        MISC
    }
    public MAGIC_TYPE magic_type;

    public enum TARGET
    {
        ONE,
        ALL,
    }

    public TARGET target;

    public string magicName;
    public string nameKana;
    public AudioClip sound = null;

    [TextArea(2, 5)]
    public string description;

    public int consumptionMp;

    [SerializeField] int[] affectValueRange;
    protected int affectValue;

    protected int GetAffectValue()
    {
        if(affectValueRange.Length < 2) return affectValueRange[0];
        return Random.Range(affectValueRange[0], affectValueRange[1] + 1);
    }

    // interface
    public string GetName()
    {
        return magicName;
    }

    public string GetNameKana()
    {
        return nameKana;
    }

    public string GetDescription()
    {
        return description;
    }

    public AudioClip GetAudioClip()
    {
        return sound;
    }

    public int AvailableAmount()
    {
        return consumptionMp;
    }

    public string ActionMessage()
    {
        return $"{{0}}は{GetNameKana().ToString()}を唱えた";
    }

    public virtual string AffectMessage(bool result = true)
    {
         return $"{{0}}は{AffectValue.ToString()}HPのダメージを受けた";;
    }

    public virtual void Consume(IStatus userStatus, IStatus targetStatus = null)
    {
        userStatus.MP -= consumptionMp;
        affectValue = GetAffectValue();
        targetStatus.HP -= AffectValue;
    }
    public int AffectValue { get => affectValue; }
}
