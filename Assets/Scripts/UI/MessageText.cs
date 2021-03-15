using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageText : MonoBehaviour
{
    [SerializeField] float textSpeed  = 0.04f;
    [SerializeField] float delaytime = 0.2f;
    [SerializeField] Text textArea;
    [SerializeField] Button nextButton = null;

    bool available = true;

    public bool Available { get => available; }

    public void DisplayMessage(Conversation conversation)
    {
        var message = conversation.text;
        textArea.text = "";

        available = false;
        if (nextButton != null) nextButton.gameObject.SetActive(false);

        textArea.DOText(message, message.Length * textSpeed)
            .SetDelay(delaytime)
            .OnComplete(() => NextButtonAvailable())
            .Play();
    }

    void NextButtonAvailable()
    {
        available = true;
        if (nextButton != null) nextButton.gameObject.SetActive(true);
    }
}
