using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactItem : MonoBehaviour
{
    [SerializeField] GameObject parentLocation;
    [SerializeField] Item item;
    [SerializeField] Vector2 faceTo;
    [SerializeField] Player player;
    [SerializeField] ConversationData emptyConversationData;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.CompareTag("Player"))  player.ContactItem = this;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.CompareTag("Player")) player.ContactItem = null;
    }

    public void FoundItem()
    {
        var questUI = player.QuestManager.QuestUI;
        var messageBox = questUI.MessageBox;

        var playerInfo = questUI.QuestManager.PlayerInfo();

        player.StopPlayer();
        player.ResetPosition(faceTo, player.transform.position);

        messageBox.EnableAsActionMessage();

        var currentScene = playerInfo.currentScene;
        var locationName = parentLocation.name;
        var sceneEventsDict = playerInfo.sceneEvents.CreateParentKeys(playerInfo.currentScene, parentLocation.name);

        if(playerInfo.sceneEvents.KeysExist(playerInfo.currentScene, parentLocation.name, this.name))
        {
            ShowMessage(messageBox, "何もなかった、、、");
            return;
        }

        if(item.player_possession_count < item.player_possession_limit)
        {
            sceneEventsDict[currentScene][locationName].Add(this.name, true);
 
            ShowMessage(messageBox, item.ItemFindMessage());
            item.Add(playerInfo);
            item = null;
            return;
        }

        ShowMessage(messageBox, item.PossessionOverMessage());
    }

    void ShowMessage(MessageBox messageBox, string message)
    {
        emptyConversationData.conversationLines[0].text = message;
        messageBox.PrepareConversation(emptyConversationData);
    }
}