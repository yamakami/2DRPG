using UnityEngine;
using UnityEngine.UI;

public class BattleMonsterSelect : UIBase
{
    [SerializeField] Image uiFrame;
    [SerializeField] Button prefTextButton;

    public void OpenSelectMenu(BattleSelector selector, Command command = null)
    {
        Activate();
        selector.ActivateRayBlock(true);

        var battleUI = selector.BattleUI;
        var battleManager = battleUI.BattleManager;
        var monsterActions  = battleManager.MonsterActions;

        foreach (var monster in monsterActions)
        {
            var button = CreateButtonUnderPanel();
            var textfield = button.GetComponentInChildren<Text>();
            textfield.text = monster.characterName;
            button.gameObject.SetActive(true);
            button.onClick.AddListener(() => ClickButtonAction(selector, selector.FlowMain, monster, command));
        }
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

    Button CreateButtonUnderPanel()
    {
        var button = Instantiate(prefTextButton);
        button.transform.SetParent(uiFrame.transform);
        button.transform.localScale = Vector3.one;

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
