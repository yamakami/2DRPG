using UnityEngine;
using UnityEngine.UIElements;

public class UIQuest : MonoBehaviour
{
    [SerializeField] UIDocument quest;
    [SerializeField] MessageBox messageBox;

    Player player;

    public void UiInitialize()
    {
        player = Player.GetPlayer();
        messageBox.Init(quest.rootVisualElement);
    }

    public void StartConversation()
    {
        messageBox.BoxOpen(true);
    }
}
