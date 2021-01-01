using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationPanel : MonoBehaviour
{
    [SerializeField] CanvasManager canvasManager = default;
    [SerializeField] MessageBox messageBox = default;
    [SerializeField] SelectPanel selectPanel = default;

    public MessageBox MessageBox { get => messageBox; }

    void OnEnable()
    {
        MessageBox.TextField.text = null;
        Invoke("DelayStart", 0.2f);
    }

    void DelayStart()
    {
        MessageBox.PrepareConversation(canvasManager.PlayerMove.characterMove.conversationData);
        MessageBox.ForwardConversation(MessageBox.Conversations.Dequeue());
    }

    public void NextMessage()
    {
        if (MessageBox.Processing)
            return;

        if (MessageBox.Conversations.Count == 0)
        {
            if (0 < MessageBox.ConversationData.subConverSations.Length)
            {
                selectPanel.MessageBox = MessageBox;
                selectPanel.gameObject.SetActive(true);
                return;
            }

            ClosePanel();
            return;
        }

        MessageBox.ForwardConversation(MessageBox.Conversations.Dequeue());
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);

        canvasManager.PlayerMove.playerInfo.startConversation = false;
        canvasManager.PlayerMove.characterMove.freeze = false;
        canvasManager.PlayerMove.playerInfo.freeze = false;
    }
}
