using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestItemSelect :ScrollItem
{
    [SerializeField] QuestUI questUI;
    [SerializeField] Text description;
    [SerializeField] ConversationData emptyConversationData;

    void OnEnable()
    {
        var playerInfo = questUI.QuestManager.PlayerInfo();
        var scrollContent = scrollRect.content;
        var stringBuilder = questUI.MessageBox.StringBuilder;

        foreach(var item in playerInfo.items)
        {            
            if(!item.useForQuest) continue;

            var button = CreateButtonUnderPanel(scrollContent.transform, item.nameKana);
            button.onClick.AddListener(() => ClickButtonAction(questUI, item));

            var trigger = button.GetComponent<EventTrigger>();

            stringBuilder.Clear();
            stringBuilder.AppendLine(item.description);
            stringBuilder.AppendFormat("残り: {0}", item.player_possession_count);
            DescriptionMessageAction(trigger, EventTriggerType.PointerEnter, stringBuilder.ToString());
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

    void ClickButtonAction(QuestUI questUI, Item item)
    {
        var messageBox = questUI.MessageBox;
        var questMenu = questUI.QuestMenu;
 
        messageBox.EnableAsActionMessage();

        UseItem(questUI, item, messageBox);
        questUI.QuestManager.Player.StopPlayer();

        questMenu.ShowItemsSelect(false);
        questMenu.ActivateCoverImage(false);
    }

    void UseItem(QuestUI questUI, Item item, MessageBox messageBox)
    {
        var questManager = questUI.QuestManager;
        var playerInfo = questUI.QuestManager.PlayerInfo();
        var player = questUI.QuestManager.Player;
        var stringBuilder = messageBox.StringBuilder;
        stringBuilder.Clear();

        switch(item.itemType)
        {
            case Item.ITEM_TYPE.KEY:
                emptyConversationData.conversationLines[0].text =
                    stringBuilder.AppendFormat(item.ActionMessage(), playerInfo.playerName, item.nameKana).ToString();

                stringBuilder.Clear();
                var openMessage = (player.ContactDoor)? player.ContactDoor.DoorOpen(item): ContactDoor.NothingHappenMessage();
                emptyConversationData.conversationLines[1].text = stringBuilder.Append(openMessage).ToString();
                break;
            default:
                item.Consume(playerInfo);
                var affectPoint = item.AffectValue(playerInfo);

                if(item.healingType == Item.HEALING_TYPE.HP) playerInfo.Hp += affectPoint; 
                if(item.healingType == Item.HEALING_TYPE.MP) playerInfo.Mp += affectPoint; 

                questManager.QuestAudioSource.PlayOneShot(item.audioClip);

                emptyConversationData.conversationLines[0].text =
                    stringBuilder.AppendFormat(item.ActionMessage(), playerInfo.playerName, item.nameKana).ToString();

                stringBuilder.Clear();
                emptyConversationData.conversationLines[1].text =
                    stringBuilder.AppendFormat(item.AffectMessage(), playerInfo.playerName, item.healingType, affectPoint).ToString();

                break;
        }
        messageBox.PrepareConversation(emptyConversationData);
    }
}
