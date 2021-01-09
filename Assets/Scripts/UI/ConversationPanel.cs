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

        ConversationLine line = MessageBox.Conversations.Dequeue();
        MessageBox.ForwardConversation(line.text);
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

        ConversationLine line = MessageBox.Conversations.Dequeue();


        //Debug.Log("-------hello: " + MessageBox.ConversationData.conversatinEvents.Length);

        //if (MessageBox.ConversationData.conversatinEvents[line.eventNum] != null)
        //{
        //    Debug.Log("-------main: " + line.eventNum);
        //    MessageBox.ConversationData.conversatinEvents[line.eventNum].Invoke();
        //    return;
        //}

        MessageBox.ForwardConversation(line.text);
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);

        canvasManager.PlayerMove.playerInfo.startConversation = false;
        canvasManager.PlayerMove.characterMove.freeze = false;
        canvasManager.PlayerMove.playerInfo.freeze = false;
    }
}
