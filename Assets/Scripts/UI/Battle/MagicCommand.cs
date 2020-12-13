using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class MagicCommand : MonoBehaviour
{
    [SerializeField] GridLayoutGroup gridLayoutGroup = default;
    [SerializeField] Button magicButton = default;
    [SerializeField] Button backButton = default;
    [SerializeField] MonsterSelect monsterSelect = default;
    [SerializeField] CanvasGroup canvasGroup = default;
    BattleCanvas battleCanvas;
    BattleManager battleManager;
    PlayerInfo playerInfo;

    string magicType;

    public BattleCanvas BattleCanvas { set => battleCanvas = value; }
    public CanvasGroup CanvasGroup { get => canvasGroup; }

    public void Open(string magicType)
    {
        battleManager = battleCanvas.BattleManager;
        playerInfo = battleManager.PlayerAction.playerInfo;

        MagickCommand(magicType);

        gameObject.SetActive(true);
        battleCanvas.MagicBasicCommand.CanvasGroup.interactable = false;

        StartCoroutine(RefreshLayout());
    }

    IEnumerator RefreshLayout()
    {
        yield return new WaitForFixedUpdate();
        gridLayoutGroup.enabled = false;
        gridLayoutGroup.enabled = true;

    }

    public void Close()
    {
        gameObject.SetActive(false);
        battleCanvas.MagicBasicCommand.CanvasGroup.interactable = true;
    }

    string MagicTargetKana(Command co)
    {
        if (co.magicInfo.magicTarget == MagicInfo.MAGIC_TARGET.ONE)
            return "単体";
        return "全体";
    }

    void MagickCommand(string magicType)
    {
        PlayerAction playerAction = battleManager.PlayerAction;

        Command[] commands = GetCommands(magicType);

        SetButtonRow(commands.Length);

        StringBuilder mp = new StringBuilder();
        foreach (var co in commands)
        {
            mp.AppendFormat("{0}MP {1}", co.magicInfo.consumptionMp, MagicTargetKana(co));

            Button bt = monsterSelect.CreateButton(transform, magicButton, co.nameKana, mp.ToString());
            mp.Clear();

            if (playerAction.mp < co.magicInfo.consumptionMp)
            {
                bt.interactable = false;
                continue;
            }

            if (co.commandType == Command.COMMAND_TYPE.MAGIC_HEAL)
            {
                bt.onClick.AddListener(() => playerAction.MagicHeal(co));
                continue;
            }

            if (co.magicInfo.magicTarget == MagicInfo.MAGIC_TARGET.ONE)
                bt.onClick.AddListener(() => monsterSelect.Open(co));

        }
        mp = null;

        Button backBt = monsterSelect.CreateButton(transform, backButton, "もどる");
        backBt.onClick.AddListener(() => Close());
    }

    Command[] GetCommands(string magicType)
    {
        Command[] commands = null;

        switch (magicType)
        {
            case "magic_attack":
                commands = playerInfo.magicAttackCommands.ToArray();
                break;
            case "magic_heal":
                commands = playerInfo.magicHealCommands.ToArray();
                break;
        }

        return commands;
    }

    void SetButtonRow(int commandLength)
    {
        int maxRows = 5;
        if (commandLength < maxRows)
            gridLayoutGroup.constraintCount = commandLength + 1;
        else
            gridLayoutGroup.constraintCount = maxRows;
    }

    void OnDisable()
    {
        foreach (Transform t in GetComponentInChildren<Transform>())
        {
            Destroy(t.gameObject);
        }
    }
}
