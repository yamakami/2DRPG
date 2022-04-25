using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;

[Serializable]
public class CommandItem : ScriptableObject, ICommand
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
    public ITEM_TYPE item_type;
    public string itemName;
    public string nameKana;
    public AudioClip sound;
    public int price = 0;
    public int sellPrice = 0;
    public int value = 0;
    public int player_possession_count = 0;
    public int player_possession_limit = 0;

    [TextArea(2, 5)]
    public string description;
    protected string messageNothingHappen = "何も起こらなかった";

    protected int affectValue;

    // interface
    public string GetName()
    {
        return itemName;
    }

    public string GetNameKana()
    {
        return nameKana;
    }

    public string GetDescription()
    {
        var sb = CommandUtils.GetStringBuilder();
        sb.Append($"{description}\n\n");
        sb.AppendFormat("所有数: {0}\n", player_possession_count).ToString();
        return sb.AppendFormat("所有限度: {0}", player_possession_limit).ToString();
    }

    public int AvailableAmount()
    {
        return player_possession_count;
    }

    public AudioClip GetAudioClip()
    {
        return sound;
    }

    public virtual void Consume(IStatus userStatus, IStatus targetStatus = null)
    {
        var playerInfo = userStatus as PlayerInfo;

        player_possession_count--;

        if(player_possession_count < 1)
            playerInfo.items.Remove(this);
    }

    public virtual string ActionMessage()
    {
        return $"{{0}}は{GetNameKana().ToString()}を使った";
    }

    public virtual string AffectMessage(bool result = true)
    {
        throw new System.NotImplementedException();
    }
    public int AffectValue { get => affectValue; }
}
