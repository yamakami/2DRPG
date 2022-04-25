using UnityEngine;
using UnityEngine.UI;

public class EquipControl : MonoBehaviour
{
    [SerializeField] Text attackNumText;
    [SerializeField] Text attackNumPreview;
    [SerializeField] Text defenseNumText;
    [SerializeField] Text defenceNumPreview;
    [SerializeField] Text[] equipedTexts;

    PlayerInfo playerInfo;

    public PlayerInfo PlayerInfo { set => playerInfo = value; }

    public void ReSetEquipedItemName()
    {
        var equipedItem = playerInfo.equiped;
        for(var i=0; i < equipedTexts.Length; i++ )
        {
            SetEquipText(equipedTexts[i], equipedItem[i]?.GetNameKana());
        }

        attackNumText.text = AttackNum();
        defenseNumText.text = DefenseNum();

        ReSetPreviewAttackNum();
        ReSetPreviewDefenceNum();
    }

    public void EquipItem(EquipItem item)
    {
        var equip_position = (int)item.equip_position;
        playerInfo.equiped[equip_position] = item;
        item.equiped = true;
        SetEquipText(equipedTexts[equip_position], item.GetNameKana());

        attackNumText.text = AttackNum();
        defenseNumText.text = DefenseNum();
    }

    void SetEquipText(Text text, string itemName)
    {
        text.text = itemName;
    }

    public void TakeOffItem(int equipedItemNum)
    {

        var equipedItem =  playerInfo.equiped[equipedItemNum] as EquipItem;
        equipedItem.equiped = false;
        SetEquipText(equipedTexts[equipedItemNum], null);
        playerInfo.equiped[equipedItemNum] = null;

        attackNumText.text = AttackNum();
        defenseNumText.text = DefenseNum();
    }

    public void SetPreviewAttackNum(EquipItem item = null)
    {
        attackNumPreview.text = AttackNum(item);
    }

    public void ReSetPreviewAttackNum()
    {
        attackNumPreview.text = "0";
    }

    public void SetPreviewDefenceNum(EquipItem item = null)
    {
        defenceNumPreview.text = DefenseNum(item);
    }

    public void ReSetPreviewDefenceNum()
    {
        defenceNumPreview.text = "0";
    }

    string AttackNum(EquipItem previewItem = null)
    {
        var arms_value = 0;

        if(previewItem)
        {
            arms_value = previewItem.value;
        }
        else
        {
            var arms = playerInfo.equiped[0];
            if(arms) arms_value = arms.value;
        }

        var base_attack = playerInfo.status.attack;
        return (base_attack + arms_value).ToString();
    }

    string DefenseNum(EquipItem previewItem = null)
    {
        var defense_value = 0;

        var equipItems = playerInfo.equiped;
        for(var i=1; i <= 5; i++)
        {
            if(previewItem && (int)previewItem.equip_position == i)
            {
                defense_value += previewItem.value;
                continue;
            }
            if(equipItems[i]) defense_value += equipItems[i].value;            
        }

        var base_defence = playerInfo.status.defence;
        return (base_defence + defense_value).ToString();
    }
}
