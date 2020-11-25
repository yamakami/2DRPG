using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBasicCommand : MonoBehaviour
{
    [SerializeField] MagicCommand magicCommand = default;
    [SerializeField] Button attackButton = default;
    [SerializeField] Button healButton = default;

    BattleCanvas battleCanvas;
    BattleManager battleManager;
    PlayerInfo playerInfo;

    public BattleCanvas BattleCanvas { set => battleCanvas = value; }

    void OnEnable()
    {
        magicCommand.BattleCanvas = battleCanvas;
    }

    public void Open()
    {
        battleManager = battleCanvas.BattleManager;
        playerInfo = battleManager.PlayerAction.playerInfo;

        if (playerInfo.magicAttackCommands.Count == 0)
            attackButton.interactable = false;

        if (playerInfo.magicHealCommands.Count == 0)
            healButton.interactable = false;

        gameObject.SetActive(true);
        battleCanvas.BattleBasicCommand.gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        battleCanvas.BattleBasicCommand.gameObject.SetActive(true);
    }

    public void CloseWithOutBasicCommand()
    {
        gameObject.SetActive(false);
    }
}
