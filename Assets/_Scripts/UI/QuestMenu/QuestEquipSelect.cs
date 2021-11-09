using UnityEngine;
using System.Text;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestEquipSelect :ScrollItem
{
    [SerializeField] QuestUI questUI;
    [SerializeField] Text description;
    [SerializeField] Text attackStatus;

    [SerializeField] Text[] equippedItemNames;
    [SerializeField] Button[] unequipButton;

    StringBuilder stringBuilder;

    int totalAttackValue;
    int totalDefenceValue;

    void Awake()
    {
        stringBuilder = new StringBuilder();
    }

    void OnEnable()
    {
        var playerInfo = questUI.QuestManager.PlayerInfo();
        var scrollContent = scrollRect.content;

        SetEquipedItemsToName(playerInfo);
        CalculateAttackDefenceValue();

        foreach(var item in playerInfo.items)
        {            
            if(item.itemType != Item.ITEM_TYPE.EQUIP_ITEM) continue;

            var button = CreateButtonUnderPanel(scrollContent.transform, item.nameKana);
            button.onClick.AddListener(() => ClickButtonAction(playerInfo, item));

            var trigger = button.GetComponent<EventTrigger>();

            DescriptionMessageAction(trigger, EventTriggerType.PointerEnter, item.description);
            DescriptionMessageAction(trigger, EventTriggerType.PointerExit);
        }
    }

    void CalculateAttackDefenceValue()
    {
        var playerInfo = questUI.QuestManager.PlayerInfo();
        var playerStatus = playerInfo.status;
        var equippedItems = playerInfo.equipment.items;

        var attack = (equippedItems[5])? equippedItems[5].point : 0;
        totalAttackValue = playerStatus.attack + attack;
        
        var defencePoint = 0;
        for(var i=0; i <= 4; i++)
        {
            if(!equippedItems[i])
                continue;

            defencePoint += equippedItems[i].point;
        }

        totalDefenceValue = playerStatus.defence + defencePoint;

        stringBuilder.Clear();
        stringBuilder.AppendFormat("総合攻撃力: {0}\n", totalAttackValue);
        stringBuilder.AppendFormat("総合守備力: {0}\n", totalDefenceValue);
        stringBuilder.AppendFormat("攻撃力: {0}\n", playerStatus.attack);
        stringBuilder.AppendFormat("守備力: {0}", playerStatus.defence);
        attackStatus.text = stringBuilder.ToString();
    }

    void SetEquipedItemsToName(PlayerInfo playerInfo)
    {
        for(var i=0; i < equippedItemNames.Length; i++)
        {
            if(playerInfo.equipment.items[i] == null)
            {
                unequipButton[i].interactable = false;
                equippedItemNames[i].text = null;
                continue;

            }
            unequipButton[i].interactable = true;
            equippedItemNames[i].text = playerInfo.equipment.items[i].nameKana;
        }
    }

    void DescriptionMessageAction(EventTrigger trigger, EventTriggerType triggerType, string str=null)
    {
        description.text = str;

        var entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener((data) => { description.text = str; });
        trigger.triggers.Add(entry);
    }

    void ClickButtonAction(PlayerInfo playerInfo, Item item)
    {
        playerInfo.SetEquipment(item);
        SetEquipedItemsToName(playerInfo);
        CalculateAttackDefenceValue();
    }

    public void Unequip(int equipPosition)
    {
        var playerInfo = questUI.QuestManager.PlayerInfo();
        playerInfo.UnEquipped(equipPosition);

        equippedItemNames[equipPosition].text = null;

        SetEquipedItemsToName(playerInfo);
        CalculateAttackDefenceValue();
    }
}
