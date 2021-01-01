using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove = default;
    [SerializeField] ConversationPanel conversationPanel = default;
    [SerializeField] BattleFlash battleFlash = default;
    [SerializeField] FaderBlack faderBlack = default;
    [SerializeField] GameObject pristFlash = default;

    public PlayerMove PlayerMove { get => playerMove; set => playerMove = value; }
    public ConversationPanel ConversationPanel { get => conversationPanel; }
    public GameObject PristFlash { get => pristFlash; }
    public FaderBlack FaderBlack { get => faderBlack; }

    void Update()
    {
        ActivateBattleFlashWhite();

        ActivateConversationPanel();
    }

    public void ActivateConversationPanel()
    {
        if (!playerMove.playerInfo.startConversation)
            return;

        ConversationPanel.gameObject.SetActive(true);
    }

    void ActivateBattleFlashWhite()
    {
        if (!playerMove.playerInfo.startBattle)
            return;

        battleFlash.gameObject.SetActive(true);
    }
}
