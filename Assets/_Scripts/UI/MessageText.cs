using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageText : MonoBehaviour
{
    [SerializeField] float textSpeed = 0.04f;
    [SerializeField] Text textArea;
    [SerializeField] Button textForwardButton;
    [SerializeField] AudioSource audioSource;

    static float _textSpeed;
    static Text _textArea;
    static string _message;
    static AudioSource _audioSource;

    public Button TextForwardButton { get => textForwardButton; }

    void  Start()
    {
        _textSpeed = textSpeed;
        _textArea  = textArea;
        _audioSource = audioSource;
    }

    public Tween TweenText(string message)
    {
        _message = message;
        _textArea.text = "";

        var sequence = DOTween.Sequence();
        sequence.SetDelay(0.2f)
                .AppendCallback(() => TweenMessage())
                .AppendInterval(1f);

        return sequence;
    }

    static Tween TweenMessage()
    {
        return _textArea.DOText(_message, _message.Length * _textSpeed)                        
                        .OnStart(() => PlayTalkSound(true))
                        .OnComplete(() => PlayTalkSound(false))
                        .Play();
    }

    static void PlayTalkSound(bool value)
    {
        if(!_audioSource) return;

        if(value)
        
            _audioSource.Play();
        else
            _audioSource.Stop();
    }
}
