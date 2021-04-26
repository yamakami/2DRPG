using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

public class MessageSelect : UIBase
{
    [SerializeField] Canvas canvas;
    [SerializeField] Image selectPanel = default;
    [SerializeField] Button selectionButtonPrefab = default;

    MessageBox messageBox;
    public Canvas Canvas { get => canvas; }
    public MessageBox MessageBox { set => messageBox = value; }

    void OnEnable()
    {
        messageBox.NextButton.gameObject.SetActive(false);
        CreateSelection();
    }

    void CreateSelection()
    {
        foreach (var conversation in messageBox.ConversationData.subConverSationLines)
        {
            var selectButton = Instantiate(selectionButtonPrefab);
            selectButton.transform.SetParent(selectPanel.transform);
            selectButton.transform.localScale = Vector3.one;
            var textfield = selectButton.GetComponentInChildren<Text>();
            textfield.text = conversation.text;

            selectButton.onClick.AddListener(() => OnClickSelection(conversation));
        }
    }

    async void OnClickSelection(Conversation conversation)
    {
        if (conversation.conversationData != null)
            messageBox.PrepareConversation(conversation.conversationData);
        else
        {
            if (conversation.eventExec)
                messageBox.DeactivateInTalk();
            else
                messageBox.Deactivate();
        }

        Deactivate();

        if (conversation.eventExec)
        {
            var tokenSource = new CancellationTokenSource();
            await UniTask.Delay(300, cancellationToken: tokenSource.Token);
            messageBox.ConversationData.conversatinEvents[conversation.eventNum].Invoke();
        }
    }

    void OnDisable()
    {
        foreach (Transform child in selectPanel.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
