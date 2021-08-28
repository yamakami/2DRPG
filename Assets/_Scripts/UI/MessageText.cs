using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageText : UIBase
{ 
    [SerializeField] Text textArea;
    [SerializeField] float textSpeed = 0.04f;
    [SerializeField] float delaytime = 0.5f;

    [SerializeField] AudioSource talkSound = default;

    bool available = true;

    public bool Available { get => available; }

    public void TweenText(string message, float delay=0f)
    {
        available = false;
        textArea.text = "";
        var delaytime = (delay == 0f)? this.delaytime : delay;

        textArea.DOText(message, message.Length * textSpeed)
                 .SetDelay(delaytime)
                 .OnStart(() => TalkSoundPlay())
                 .OnComplete(() => TalkSoundStop())
                 .Play();
    }

    void TalkSoundPlay()
    {
        if (talkSound) talkSound.Play();
    }

    void TalkSoundStop()
    {
        available = true;
        if (talkSound) talkSound.Stop();
    }
}

