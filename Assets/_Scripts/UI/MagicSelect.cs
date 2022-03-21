
using System.Collections.Generic;
using UnityEngine;

public class MagicSelect : CommandSelect
{
    protected override  ICommand[] GetCommandList()
    {
        return playerInfo.magics.ToArray();
    }

    bool IsAvailable(ICommand command)
    {
        var amount = command.AvailableAmount();
        if(amount <= playerInfo.status.mp) return true;

        return false;
    }

    protected override void CreateButton()
    {
        var startIndex = PageSetting();
        PageNumDisplay();

        for(var i=0; i < pageButtonNum; i++)
        {
            var button = InitializeButton(i);

            if(commandList.Length <= startIndex) continue;

            var command = ActivateButton(startIndex, button);

            startIndex++;

            if(!IsAvailable(command)){
                button.Button.interactable = false;
                button.EventTrigger.enabled = false;
                button.Text.color = new Color32(135, 135, 135, 255);
                continue;
            }

            button.Button.onClick.AddListener(() => ClickAction(command));
            AddDescriptionEvents(command, button);
        }
    }
}
