using UnityEngine;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;

public class UIQuest : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioSource subAudioSource;
    [SerializeField] UIDocument quest;
    [SerializeField] MessageBox messageBox;

    Player player;
    VisualElement rootUiElement;
    AudioClip[] buttonSounds;

    public AudioClip[] ButtonSounds { get => buttonSounds; }
    public VisualElement RootUiElement { get => rootUiElement; }

    public void UiInitialize()
    {
        buttonSounds = gameManager.ButtonSounds;
        player = Player.GetPlayer();

        rootUiElement = quest.rootVisualElement;
        messageBox.Init(this);
    }

    public void StartConversation(ConversationData conversationData)
    {
        messageBox.Conversation(conversationData).Forget();
    }
}
