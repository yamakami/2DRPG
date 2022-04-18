using UnityEngine;

public class Equipment : CommandSelect
{
    [SerializeField] EquipControl equipControl;

    protected override ICommand[] GetCommandList()
    {
        return playerInfo.items.FindAll( i => i.item_type == CommandItem.ITEM_TYPE.EQUIP_ITEM).ToArray();
    }

    public override void Open()
    {
        base.Open();
        equipControl.ReSetEquipedItemName(playerInfo);
    }

    protected override void ClickAction(ICommand selected)
    {
        equipControl.EquipItem(selected as EquipItem);
    }

    protected override void AddDescriptionEvents(ICommand command, SelectButton button)
    {
        base.AddDescriptionEvents(command, button);
    }
}
