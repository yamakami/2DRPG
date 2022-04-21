using UnityEngine;
using UnityEngine.EventSystems;

public class Equipment : CommandSelect
{
    [SerializeField] EquipControl equipControl;

    protected override void Start()
    {
        base.Start();
        equipControl.PlayerInfo = playerInfo;
    }

    protected override ICommand[] GetCommandList()
    {
        return playerInfo.items.FindAll( i => i.item_type == CommandItem.ITEM_TYPE.EQUIP_ITEM).ToArray();
    }

    public override void Open()
    {
        base.Open();
        equipControl.ReSetEquipedItemName();
    }

    protected override void ClickAction(ICommand selected)
    {
        equipControl.EquipItem(selected as EquipItem);
    }

    protected override void DescriptionMessageAction(EventTrigger trigger, EventTriggerType triggerType, ICommand command=null)
    {
        var entry = new EventTrigger.Entry();
        entry.eventID = triggerType;

        entry.callback.AddListener((data) => {
            descriptionText.text = command?.GetDescription();

            var equipItem = command as EquipItem;

            if(triggerType == EventTriggerType.PointerEnter)
            {
                if(equipItem.equip_position == EquipItem.EQUIP_POSITION.ARMS)
                    equipControl.SetPreviewAttackNum(equipItem);
                else
                    equipControl.SetPreviewDefenceNum(equipItem);
            }
            else
            {
                equipControl.ReSetPreviewAttackNum();
                equipControl.ReSetPreviewDefenceNum();
            }
        });

        trigger.triggers.Add(entry);
    }
}
