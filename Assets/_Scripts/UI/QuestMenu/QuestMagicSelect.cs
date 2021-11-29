using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestMagicSelect :ScrollItem
{
    [SerializeField] QuestUI questUI;
    [SerializeField] Text description;
    [SerializeField] ConversationData emptyConversationData;

    void OnEnable()
    {
        var playerInfo = questUI.QuestManager.PlayerInfo();
        var scrollContent = scrollRect.content;
        var stringBuilder = questUI.MessageBox.StringBuilder;

        foreach(var magic in playerInfo.magicCommands)
        {            
            if(!magic.useForQuest) continue;

            var button = CreateButtonUnderPanel(scrollContent.transform, magic.nameKana);
    
            var consumptionMp = magic.magicCommand.consumptionMp;
            if(playerInfo.Mp < consumptionMp)
                 button.interactable = false;
             else
                button.onClick.AddListener(() => ClickButtonAction(questUI, magic));

            var trigger = button.GetComponent<EventTrigger>();

            stringBuilder.Clear();
            stringBuilder.AppendLine(magic.description);
            stringBuilder.AppendFormat("消費MP: {0}",consumptionMp);
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

    void ClickButtonAction(QuestUI questUI, Command magic)
    {
        var messageBox = questUI.MessageBox;
        var questMenu = questUI.QuestMenu;
 
        messageBox.EnableAsActionMessage();

        UseItem(questUI.QuestManager.PlayerInfo(), magic, messageBox);
        questUI.QuestManager.Player.StopPlayer();

        questMenu.ShowMagicsSelect(false);
        questMenu.ActivateCoverImage(false);
    }

    void UseItem(PlayerInfo playerInfo, Command magic, MessageBox messageBox)
    {
        var stringBuilder = messageBox.StringBuilder;
        stringBuilder.Clear();

        switch(magic.commandType)
        {
            case Command.COMMAND_TYPE.MAGIC_HEAL:
                var affectedPoint = magic.magicCommand.HealValue(playerInfo.Hp, playerInfo.MaxHP);
                playerInfo.Mp -= magic.magicCommand.consumptionMp;
                playerInfo.Hp += affectedPoint;

                emptyConversationData.conversationLines[0].text =
                    stringBuilder.AppendFormat(magic.ActionMessage(), playerInfo.playerName, magic.nameKana).ToString();

                stringBuilder.Clear();
                emptyConversationData.conversationLines[1].text =
                    stringBuilder.AppendFormat(magic.AffectMessage(), playerInfo.playerName, affectedPoint).ToString();

                messageBox.PrepareConversation(emptyConversationData);
                break;
            default:
                break;
        }
    }
}
