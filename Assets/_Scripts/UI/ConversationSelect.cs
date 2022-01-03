using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class ConversationSelect : MonoBehaviour
{
    [SerializeField] int optionMax;
    [SerializeField] Canvas canvas;
    [SerializeField] Button prefSelectButton;
    QuestManager questManager;
    Conversation conversation;
    List<Button> selectButtons;

    void Start()
    {
        questManager = QuestManager.GetQuestManager();
        conversation = questManager.QuestUI.Conversation;

        selectButtons = new List<Button>(optionMax);

        Button button;
        for(var i = 0; i < optionMax; i++)
        {
            button = Instantiate(prefSelectButton);

            button.transform.SetParent(this.transform);
            button.gameObject.SetActive(false);
            selectButtons.Add(button);
        }
    }

    void ReCreateButton(ConversationData.Conversation[] options)
    {
        Button button;
        for(var i = 0; i < optionMax; i++)
        {
            button = selectButtons[i];
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);

            if(options.Length <= i) continue;

            var option = options[i];
            var textfield = button.GetComponentInChildren<Text>();
            textfield.text = option.text;
            button.gameObject.SetActive(true);
            button.onClick.AddListener(() => ClickAction(option));
        }
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
