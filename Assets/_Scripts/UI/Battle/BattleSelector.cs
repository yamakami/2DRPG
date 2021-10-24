using UnityEngine;
using UnityEngine.UI;

public class BattleSelector : UIBase
{
    [SerializeField] GameObject basicCommands;

    [SerializeField] Button magicSelectButton;
    [SerializeField] Button itemSelectButton;
 
    [SerializeField] GameObject rayBlockObj;

    [SerializeField] BattleMonsterSelect monsterSelect;
    [SerializeField] BattleMagicSelect magicSelect;
    [SerializeField] BattleItemSelect itemSelect;

    BattleUI battleUI;
    FlowMain flowMain;

    public BattleUI BattleUI { get => battleUI; set => battleUI = value; }
    public FlowMain FlowMain { get => flowMain; set => flowMain = value; }
    public BattleMonsterSelect MonsterSelect { get => monsterSelect; set => monsterSelect = value; }

    public enum SelectBack
    {
        BASE_COMMAND,
        MAGIC_SELECT,
        ITEM_SELECT,
    }

    public void PlayerSelectDone()
    {
        flowMain.PlayerInput = true;
        ActivateRayBlock(false);
        ActivateBasicCommands(false);
    }

    public void ActivateBasicCommands(bool activation)
    {
        if(activation)
        {
            var playerInfo = battleUI.BattleManager.PlayerAction.PlayerInfo;
            if(playerInfo.magicCommands.Count < 1)
                magicSelectButton.gameObject.SetActive(false);

            if(!playerInfo.items.Find(i => i.useForBattle))
                itemSelectButton.gameObject.SetActive(false);
        }

        basicCommands.gameObject.SetActive(activation);
    }

    public void ActivateRayBlock(bool activate)
    {
        rayBlockObj.gameObject.SetActive(activate);
    }

    public void ClickBasicCommandFight()
    {　
        monsterSelect.OpenSelectMenu(this, SelectBack.BASE_COMMAND);
    }

    public void ClickBasicCommandMagicSpell()
    {　
        ActivateRayBlock(true);
        magicSelect.ActivateScrollItem(this);
    }

    public void ClickMagicSelectBack()
    {　
        ActivateRayBlock(false);
        magicSelect.Deactivate();
    }

    public void ClickBasicCommandItems()
    {　
        ActivateRayBlock(true);
        itemSelect.ActivateScrollItem(this);
    }

    public void ClickItemSelectBack()
    {　
        ActivateRayBlock(false);
        itemSelect.Deactivate();
    }


    public void ClickEscape()
    {
        flowMain.Attacker.SelectedCommand =  battleUI.BattleManager.PlayerAction.PlayerInfo.battleCommands[1];

        var defender = flowMain.Defenders;
        flowMain.Defenders.RemoveAll(d => d.characterName != defender[0].characterName);

        PlayerSelectDone();
    }

    public void ClickGoBack(SelectBack backindex)
    {
        switch(backindex)
        {
            case SelectBack.MAGIC_SELECT:
                ClickBasicCommandMagicSpell();
                break;
            case SelectBack.ITEM_SELECT:
                ClickBasicCommandItems();
                break;
            default:
                ActivateBasicCommands(true);
                break;
        }
    }
}
