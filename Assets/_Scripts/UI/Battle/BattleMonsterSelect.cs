using UnityEngine;
using UnityEngine.UI;

public class BattleMonsterSelect : UIBase
{
    [SerializeField] Image uiFrame;
    [SerializeField] Button prefTextButton;

    public void OpenSelectMenu(BattleSelector selector, BattleSelector.SelectBack selectBack,  Command command = null)
    {
        Activate();
        selector.ActivateRayBlock(true);

        var battleUI = selector.BattleUI;
        var battleManager = battleUI.BattleManager;
        var monsterActions  = battleManager.MonsterActions;

        foreach (var monster in monsterActions)
        {
            var button = CreateButtonUnderPanel(monster.characterName);
            button.onClick.AddListener(() => ClickButtonAction(selector, selector.FlowMain, monster, command));
        }

        var backButton = CreateButtonUnderPanel("もどる");
        backButton.onClick.AddListener(() => ClickBackBottonAction(selector, selectBack));
    }

    void ClickButtonAction(BattleSelector selector, FlowMain flowMain, MonsterAction monsterAction, Command command)
    {
        flowMain.Defenders.RemoveAll(m => m.characterName !=  monsterAction.characterName);
        flowMain.PlayerInput = true;

        var battleManager = selector.BattleUI.BattleManager;

        if(!command) command = battleManager.PlayerInfo.battleCommands[0];

        battleManager.PlayerAction.SelectedCommand = command;

        Deactivate();
        selector.ActivateRayBlock(false);
        selector.ActivateBasicCommands(false);
    }

    void ClickBackBottonAction(BattleSelector selector, BattleSelector.SelectBack backIndex)
    {
        Deactivate();
        selector.ActivateRayBlock(false);
        selector.ClickGoBack(backIndex);
    }

    Button CreateButtonUnderPanel(string text)
    {
        var button = Instantiate(prefTextButton);
        button.transform.SetParent(uiFrame.transform);
        button.transform.localScale = Vector3.one;

        var textfield = button.GetComponentInChildren<Text>();
        textfield.text = text;
        button.gameObject.SetActive(true);

        return button;
    }

    void OnDisable()
    {
        foreach (Transform child in uiFrame.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
