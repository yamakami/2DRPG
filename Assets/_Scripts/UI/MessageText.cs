using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageText : MonoBehaviour
{
    [SerializeField] float preWait = 0.2f;
    [SerializeField] float aftWait = 1f;
    [SerializeField] float textSpeed = 0.04f;
    [SerializeField] Text textArea;
    [SerializeField] Button textForwardButton;
    Sequence sequence;

    static float stTextSpeed;
    static Text stTextArea;
    static string stMessage;
    static AudioSource stAudio;

    public Button TextForwardButton { get => textForwardButton; }

    void  Start()
    {
        sequence = DOTween.Sequence();
        stTextSpeed = textSpeed;
        stTextArea  = textArea;
    }

    public Tween TweenText(string message, AudioSource audio = null)
    {
        stMessage = message;
        stTextArea.text = "";
        stAudio = audio;

        sequence.SetDelay(preWait)
                .AppendCallback(() => TweenMessage())
                .AppendInterval(aftWait)
                .SetAutoKill(false)
                .SetLink(gameObject);

        sequence.Rewind();

        return sequence;
    }

    static Tween TweenMessage()
    {
        return stTextArea.DOText(stMessage, stMessage.Length * stTextSpeed)                        
                         .OnStart(() => PlayAudio(true))
                         .OnComplete(() => PlayAudio(false))
                         .Play();
    }

    static void PlayAudio(bool value)
    {
        if(!stAudio) return;

        if(value)
        
            stAudio.Play();
        else
            stAudio.Stop();
    }
}
