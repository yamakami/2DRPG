using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMagicSelect: ScrollItem
{
    public override void ActivateScrollItem(BattleSelector selector)
    {
        Activate();

        var battleUI = selector.BattleUI;
        var playerInfo = battleUI.BattleManager.PlayerInfo;
        var scrollContent = scrollRect.content;

        foreach(var command in playerInfo.magicCommands)
        {
            var button = CreateButtonUnderPanel(scrollContent.transform, command.nameKana);
            button.onClick.AddListener(() => ClickButtonAction(selector, command));
        }
    }

    void ClickButtonAction(BattleSelector selector, Command command)
    {
        var battleManager = selector.BattleUI.BattleManager;
        var flowMain = selector.FlowMain;

        Deactivate();
        battleManager.PlayerAction.SelectedCommand = command;

        if(command.commandType == Command.COMMAND_TYPE.MAGIC_HEAL)
        {
            selector.FlowMain.PlayerInput = true;
            flowMain.Defenders.Clear();
            flowMain.Defenders.Add(flowMain.Attacker);

            selector.ActivateRayBlock(false);
            selector.ActivateBasicCommands(false);
            return;
        }

        if(command.magicCommand.magicTarget == MagicCommand.MAGIC_TARGET.ALL)
        {
            selector.FlowMain.PlayerInput = true;

            selector.ActivateRayBlock(false);
            selector.ActivateBasicCommands(false);
            return;
        }

        selector.MonsterSelect.OpenSelectMenu(selector, command);
    }
}
