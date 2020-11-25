using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanel : MonoBehaviour
{
    [SerializeField] Button forwardButton = default;
    [SerializeField] Button selectionButton = default;
    [SerializeField] Image selectBox = default;

    MessageBox messageBox;

    public MessageBox MessageBox { get => messageBox; set => messageBox = value; }

    private void OnEnable()
    {
        forwardButton.interactable = false;
        CreateSelection();
    }

    void CreateSelection()
    {
        foreach (SubConverSation conversation in messageBox.ConversationData.subConverSations)
        {
            Button selectButton = Instantiate(selectionButton);
            selectButton.transform.SetParent(selectBox.transform);
            selectButton.transform.localScale = Vector3.one;
            Text textfield = selectButton.GetComponentInChildren<Text>();
            textfield.text = conversation.text;
            selectButton.onClick.AddListener(() => OnClickSelection(conversation.conversationData));
        }
    }

    void OnClickSelection(ConversationData conversationData)
    {
        messageBox.PrepareConversation(conversationData);
        messageBox.ForwardConversation(messageBox.Conversations.Dequeue());
        gameObject.SetActive(false);

        foreach (Transform child in selectBox.gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        forwardButton.interactable = true;
    }
}
