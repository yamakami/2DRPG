using UnityEngine;
using System.Text;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestItemSelect :ScrollItem
{
    [SerializeField] QuestUI questUI;
    [SerializeField] Text description;

    StringBuilder stringBuilder;

    void Awake()
    {
        stringBuilder = new StringBuilder();
    }

    void OnEnable()
    {
        var playerInfo = questUI.QuestManager.PlayerInfo();
        var scrollContent = scrollRect.content;

        foreach(var item in playerInfo.items)
        {            
            if(!item.useForQuest) continue;

            var button = CreateButtonUnderPanel(scrollContent.transform, item.nameKana);
            button.onClick.AddListener(() => ClickButtonAction(playerInfo, item));

            var trigger = button.GetComponent<EventTrigger>();

            DescriptionMessageAction(trigger, EventTriggerType.PointerEnter, item.description);
            DescriptionMessageAction(trigger, EventTriggerType.PointerExit);
        }
    }



    void DescriptionMessageAction(EventTrigger trigger, EventTriggerType triggerType, string str=null)
    {
        description.text = str;

        var entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener((data) => { description.text = str; });
        trigger.triggers.Add(entry);
    }

    void ClickButtonAction(PlayerInfo playerInfo, Item item)
    {
        // not yet implement
    }
}
