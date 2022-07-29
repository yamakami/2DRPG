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

    public void PlayConversationSound(bool play = true, AudioClip talkSound = null)
    {
        subAudioSource.loop = play;
        if (play)
        {
            subAudioSource.clip = talkSound;
            subAudioSource.Play();
        }
        else
            subAudioSource.clip = null;
    }

    public void PlayButtonHoverSound()
    {
        subAudioSource.PlayOneShot(buttonSounds[0]);
    }

    public void PlayButtonClickSound()
    {
        subAudioSource.PlayOneShot(buttonSounds[1]);
    }
}
