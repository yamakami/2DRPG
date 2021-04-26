using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Text;

public class MessageText : UIBase
{
    [SerializeField] float textSpeed  = 0.04f;
    [SerializeField] float delaytime = 0.2f;
    [SerializeField] Text textArea;
    [SerializeField] Button nextButton = null;
    [SerializeField] AudioSource talkSound = null;

    bool available = true;

    public bool Available { get => available; }

    public void DisplayMessage(Conversation conversation)
    {
        var message = conversation.text;

        if (nextButton != null) nextButton.gameObject.SetActive(false);

        TweenText(message);
    }

    public void DisplayBattleMessage(StringBuilder builder)
    {
        var str = builder.ToString();
        TweenText(str);

        builder.Clear();
    }

    void TweenText(string message)
    {
        textArea.text = "";

        available = false;

        textArea.DOText(message, message.Length * textSpeed)
            .SetDelay(delaytime)
            .OnStart(() => TalkSoundPlay())
            .OnComplete(() => NextButtonAvailable())
            .Play();
    }

    void NextButtonAvailable()
    {
        available = true;
        if (nextButton != null) nextButton.gameObject.SetActive(true);
        TalkSoundStop();
    }

    void TalkSoundPlay()
    {
        if (talkSound) talkSound.Play();
    }

    void TalkSoundStop()
    {
        if (talkSound) talkSound.Stop();
    }
}
