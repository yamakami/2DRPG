public class BattleMagicSelect: ScrollItem
{
    public override void ActivateScrollItem(BattleSelector selector)
    {
        Activate();

        var battleUI = selector.BattleUI;
        var playerAction = battleUI.BattleManager.PlayerAction;
        var playerInfo = playerAction.PlayerInfo;
        var scrollContent = scrollRect.content;

        foreach(var command in playerInfo.magicCommands)
        {
            var button = CreateButtonUnderPanel(scrollContent.transform, command.nameKana);
            if(command.magicCommand.consumptionMp <= playerAction.mp)

                button.onClick.AddListener(() => ClickButtonAction(selector, command));
            else
                button.interactable = false;
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
            flowMain.Defenders.Clear();
            flowMain.Defenders.Add(flowMain.Attacker);

            selector.PlayerSelectDone();
            return;
        }

        if(command.magicCommand.magicTarget == MagicCommand.MAGIC_TARGET.ALL)
        {
            selector.PlayerSelectDone();
            return;
        }

        selector.MonsterSelect.OpenSelectMenu(selector, BattleSelector.SelectBack.MAGIC_SELECT, command);
    }
}
