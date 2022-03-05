using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageText : MonoBehaviour
{
    [SerializeField] float textSpeed = 0.04f;
    [SerializeField] Text textArea;

    static AudioSource stAudio;

    public void ClearText()
    {
        textArea.text = "";
    }

    public Tween TweenText(string message, AudioSource audio = null)
    {
        stAudio = audio;

        ClearText();
        var tween = textArea.DOText(message, message.Length * textSpeed);

        if(stAudio != null)
            tween.OnStart(() => PlayAudio(true)).OnComplete(() => PlayAudio(false));

        return tween;
    }

    static void PlayAudio(bool value)
    {
        if(stAudio == null) return;

        if(value)
            stAudio.Play();
        else
            stAudio.Stop();
    }
}
