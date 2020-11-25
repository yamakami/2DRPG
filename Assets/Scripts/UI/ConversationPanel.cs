using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationPanel : MonoBehaviour
{
    [SerializeField] CanvasManager canvasManager = default;
    [SerializeField] MessageBox messageBox = default;
    [SerializeField] SelectPanel selectPanel = default;

    void OnEnable()
    {
        messageBox.PrepareConversation(canvasManager.PlayerMove.characterMove.conversationData);
        messageBox.ForwardConversation(messageBox.Conversations.Dequeue());
    }

    public void NextMessage()
    {
        if (messageBox.Processing)
            return;

        if (messageBox.Conversations.Count == 0)
        {
            if (0 < messageBox.ConversationData.subConverSations.Length)
            {
                selectPanel.MessageBox = messageBox;
                selectPanel.gameObject.SetActive(true);
                return;
            }

            ClosePanel();
            return;
        }

        messageBox.ForwardConversation(messageBox.Conversations.Dequeue());
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);

        canvasManager.PlayerMove.playerInfo.startConversation = false;
        canvasManager.PlayerMove.characterMove.freeze = false;
        canvasManager.PlayerMove.playerInfo.freeze = false;
    }
}
