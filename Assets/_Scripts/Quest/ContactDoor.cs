using UnityEngine;

public class ContactDoor : MonoBehaviour, IMessageCallbackable
{
    [SerializeField] GameObject parentLocation;
    [SerializeField] Item item;
    [SerializeField] Vector2 faceTo;
    [SerializeField] QuestManager questManager;

    void OnEnable()
    {
        var playerInfo =  questManager.PlayerInfo();
        if(playerInfo.sceneEvents.KeysExist(playerInfo.currentScene, parentLocation.name, this.name)) gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.CompareTag("Player"))  questManager.Player.ContactDoor = this;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.CompareTag("Player")) questManager.Player.ContactDoor = null;
    }

    public string DoorOpen(Item selectItem, MessageBox messageBox)
    {
        var player  = questManager.Player;

        player.ResetPosition(faceTo, player.transform.position);

        var nothingHappen = NothingHappenMessage();
        if(item.itemName != selectItem.itemName) return nothingHappen;

        var playerInfo =  questManager.PlayerInfo();
        var currentScene = playerInfo.currentScene;
        var locationName = parentLocation.name;

        if(playerInfo.sceneEvents.KeysExist(currentScene, locationName, this.name)) return nothingHappen;

        var sceneEventsDict = playerInfo.sceneEvents.CreateParentKeys(currentScene, locationName);
        sceneEventsDict[currentScene][locationName].Add(this.name, true);

        messageBox.Callbackable = this;

        return selectItem.resultMessage;
    }

    public void MessageCallbackAction()
    {
        var messageBox = questManager.QuestUI.MessageBox;
        gameObject.SetActive(false);
        questManager.QuestAudioSource.PlayOneShot(item.audioClip);
        messageBox.Callbackable = null;
    }

    public static string NothingHappenMessage() { return "何もおこらない、、、"; }
}