using UnityEngine;
public class BattleItemSelect: ScrollItem
{
    [SerializeField] Command itemCommand;
    public void ActivateScrollItem(BattleSelector selector)
    {
        Activate();

        var battleUI = selector.BattleUI;
        var playerAction = battleUI.BattleManager.PlayerAction;
        var playerInfo = playerAction.PlayerInfo;
        var scrollContent = scrollRect.content;

        foreach(var item in playerInfo.items)
        {            
            if(!item.useForBattle) continue;

            var button = CreateButtonUnderPanel(scrollContent.transform, item.nameKana);
            button.onClick.AddListener(() => ClickButtonAction(selector, item));
        }
    }

    void ClickButtonAction(BattleSelector selector, Item item)
    {
        var battleManager = selector.BattleUI.BattleManager;
        var flowMain = selector.FlowMain;

        Deactivate();
        battleManager.PlayerAction.SelectedCommand = itemCommand;
        battleManager.PlayerAction.SelectedItem = item;

        if(item.itemType == Item.ITEM_TYPE.HEALING_ITEM)
        {
            flowMain.Defenders.Clear();
            flowMain.Defenders.Add(flowMain.Attacker);

            selector.PlayerSelectDone();
            return;
        }

        selector.MonsterSelect.OpenSelectMenu(selector, BattleSelector.SelectBack.ITEM_SELECT, itemCommand);
    }
}
