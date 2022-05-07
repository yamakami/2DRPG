using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] SelectButton magicSelect;
    [SerializeField] SelectButton itemSelect;
    [SerializeField] SelectButton equipSelect;


    public void Activate()
    {
        var  playerInfo = GameManager.GetPlayerInfo();
        canvas.enabled = true;

        ActivateMagicSelect(playerInfo); 
        ActivateItemSelect(playerInfo);
        ActivatEquipSelect(playerInfo);
    }

    void ActivateMagicSelect(PlayerInfo playerInfo)
    {
        if(playerInfo.magics.FindAll(i => i.useForQuest).Count < 1)
            magicSelect.DisableButton();
        else
            magicSelect.EnableButton();
    }

    void ActivateItemSelect(PlayerInfo playerInfo)
    {
        if(playerInfo.items.FindAll(i => i.useForQuest).Count < 1)
            itemSelect.DisableButton();
        else
            itemSelect.EnableButton();
    }

    void ActivatEquipSelect(PlayerInfo playerInfo)
    {
        if(playerInfo.items.FindAll(i => i.item_type == CommandItem.ITEM_TYPE.EQUIP_ITEM).Count < 1)
            equipSelect.DisableButton();
        else
            equipSelect.EnableButton();
    }
}
