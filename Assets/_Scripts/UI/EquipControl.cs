using UnityEngine;
using UnityEngine.UI;

public class EquipControl : MonoBehaviour
{
    [SerializeField] Text attackNumText;
    [SerializeField] Text defenseNumText;
    [SerializeField] Text[] equipedTexts;
    PlayerInfo playerInfo;

    public void ReSetEquipedItemName(PlayerInfo info)
    {
        playerInfo = info;
        var equipedItem = playerInfo.equiped;
        for(var i=0; i < equipedTexts.Length; i++ )
        {
            SetEquipText(equipedTexts[i], equipedItem[i]?.GetNameKana());
        }
    }

    public void EquipItem(EquipItem item)
    {
        var equip_position = (int)item.equip_position;
        playerInfo.equiped[equip_position] = item;
        SetEquipText(equipedTexts[equip_position], item.GetNameKana());
    }

    void SetEquipText(Text text, string itemName)
    {
        text.text = itemName;
    }

    public void TakeOffItem(int equipedItemNum)
    {
        SetEquipText(equipedTexts[equipedItemNum], null);
        playerInfo.equiped[equipedItemNum] = null;
    }
}
