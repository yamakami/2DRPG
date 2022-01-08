using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class ConversationSelect : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] int initialBoxHeight = 55;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] ConversationSelectButton[] optionButtons;
    QuestManager questManager;
    Conversation conversation;
    List<Button> selectButtons;

    void Start()
    {
        questManager = QuestManager.GetQuestManager();
        conversation = questManager.QuestUI.Conversation;
    }

    void ReCreateButton(ConversationData.Conversation[] options)
    {
        var  boxSize = rectTransform.sizeDelta;
        boxSize.y = initialBoxHeight;
        for(var i = 0; i < optionButtons.Length; i++)
        {
            var button = optionButtons[i];
            button.Button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);

            if(options.Length <= i) continue;

            var option = options[i];
            button.Text.text = option.text;
            button.gameObject.SetActive(true);
            button.Button.onClick.AddListener(() => ClickAction(option));
            boxSize.y += 45;
        }
        rectTransform.sizeDelta = boxSize;
    }

    void ClickAction(ConversationData.Conversation option)
    {
        Close();

        if(option?.nextConversationData)
        {
            conversation.PrepareConversation(option.nextConversationData);
            return;
        }
        conversation.EndConversation();
    }

    public void Open(ConversationData.Conversation[] options)
    {
        Visible(true);
        ReCreateButton(options);
    }

    public void Close()
    {
        Visible(false);
    }

    void Visible(bool value)
    {
        canvas.enabled = value;
        gameObject.SetActive(value);
    }
}
