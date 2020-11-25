using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove = default;
    [SerializeField] ConversationPanel conversationPanel = default;
    [SerializeField] BattleFlash battleFlash = default;

    public PlayerMove PlayerMove { get => playerMove; set => playerMove = value; }

    void Update()
    {
        ActivateBattleFlashWhite();

        ActivateConversationPanel();
    }

    void ActivateConversationPanel()
    {
        if (!playerMove.playerInfo.startConversation)
            return;

        conversationPanel.gameObject.SetActive(true);
    }

    void ActivateBattleFlashWhite()
    {
        if (!playerMove.playerInfo.startBattle)
            return;

        battleFlash.gameObject.SetActive(true);
    }
}
