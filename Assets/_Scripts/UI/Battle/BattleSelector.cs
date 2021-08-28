using UnityEngine;

public class BattleSelector : UIBase
{
    [SerializeField] GameObject basicCommands;
    [SerializeField] GameObject rayBlockObj;
    [SerializeField] BattleMonsterSelect monsterSelect;
    [SerializeField] BattleMagicSelect magicSelect;

    BattleUI battleUI;
    FlowMain flowMain;

    public BattleUI BattleUI { get => battleUI; set => battleUI = value; }
    public FlowMain FlowMain { get => flowMain; set => flowMain = value; }
    public BattleMonsterSelect MonsterSelect { get => monsterSelect; set => monsterSelect = value; }

    public void ActivateBasicCommands(bool activation)
    {
        basicCommands.gameObject.SetActive(activation);
    }

    public void ActivateRayBlock(bool activate)
    {
        rayBlockObj.gameObject.SetActive(activate);
    }

    public void ClickBasicCommandFight()
    {　
        monsterSelect.OpenSelectMenu(this);
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
}
