using UnityEngine;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;

public class UIQuest : MonoBehaviour
{
    [SerializeField] UIDocument quest;
    [SerializeField] MessageBox messageBox;

    Player player;

    public void UiInitialize()
    {
        player = Player.GetPlayer();
        var rootElement = quest.rootVisualElement;
        messageBox.Init(rootElement);
    }

    public void StartConversation(ConversationData conversationData)
    {
        messageBox.Conversation(conversationData).Forget();
    }
}
