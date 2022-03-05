using UnityEngine;

public class ConversationSelect : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] SelectButton[] optionButtons;
    [SerializeField] int initialBoxHeight = 55;
    QuestManager questManager;
    Conversation conversation;
    int rowHight = 45;
    ConversationData.Conversation[] selectOptions;

    public ConversationData.Conversation[] SelectOptions { set => selectOptions = value; }

    void Start()
    {
        questManager = QuestManager.GetQuestManager();
        conversation = questManager.QuestUI.Conversation;
    }

    void CreateButton()
    {
        var  boxSize = rectTransform.sizeDelta;
        boxSize.y = initialBoxHeight;
        for(var i = 0; i < optionButtons.Length; i++)
        {
            var button = optionButtons[i];
            button.Button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);

            if(selectOptions.Length <= i) continue;

            var selectOption = selectOptions[i];
            button.Text.text = selectOption.text;
            button.gameObject.SetActive(true);
            button.Button.onClick.AddListener(() => ClickAction(selectOption));
            boxSize.y += rowHight;
        }
        rectTransform.sizeDelta = boxSize;
    }

    void ClickAction(ConversationData.Conversation selectOption)
    {
        Visible(false);

        if(selectOption?.nextConversationData)
        {
            conversation.PrepareConversation(selectOption.nextConversationData);
            return;
        }
        conversation.EndConversation();
    }

    public void Open()
    {
        CreateButton();
        Visible(true);
    }

    void Visible(bool value)
    {
        canvas.enabled = value;
    }
}
