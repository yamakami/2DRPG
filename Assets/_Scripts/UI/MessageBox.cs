using UnityEngine;
using UnityEngine.UIElements;
using System.Threading;
using Cysharp.Threading.Tasks;

public class MessageBox : MonoBehaviour
{
    [SerializeField] MessageSelect messageSelect;
    IMessageBox interfaceParent;
    SoundManager soundManager;
    VisualElement messageBox;
    Label textArea;
    Button conversationNextButton;
    Button messageNextButton;

    public Button ConversationNextButton { get => conversationNextButton; }
    public Button MessageNextButton { get => messageNextButton; }
    public IMessageBox InterfaceParent { get => interfaceParent; set => interfaceParent = value; }

    public void SetUp(VisualElement _rootUI)
    {
        soundManager = SystemManager.SoundManager();

        messageBox = _rootUI.Q<VisualElement>("message-screen");
        textArea   = _rootUI.Q<Label>("message-text");
        conversationNextButton = _rootUI.Q<Button>("conversation-next-button");
        messageNextButton      = _rootUI.Q<Button>("message-next-button");

        messageSelect.SetUp(_rootUI, this);
    }

    public async UniTask DisplayText(CancellationToken token, string message, AudioClip sound = null)
    {
        textArea.text = "";

        await UniTask.Delay(500, cancellationToken: token);

        soundManager.PlaySubAudio(sound);

        for(var i = 0; i <= message.Length; i++)
        {
            textArea.text = message.Substring(0, i);
            await UniTask.Delay(30, cancellationToken: token);
        }
        soundManager.StopSubAudio();

        await UniTask.Delay(500, cancellationToken: token);
    }

    public void Open(bool open)
    {
        messageBox.style.display = (open)? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void NextConversationButton(bool show)
    {
        conversationNextButton.style.display = (show)? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void OpenSelectBox(ConversationData conversationData)
    {
        messageSelect.Open(conversationData);
    }

    public void PlayButtonClick()
    {
        soundManager.PlayButtonClick();
    }

    public void PlayButtonHover()
    {
        soundManager.PlayButtonHover();        
    }
}
