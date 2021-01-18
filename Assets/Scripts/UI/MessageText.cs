using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageText : MonoBehaviour
{
    [SerializeField] float textSpeed  = 0.04f;
    [SerializeField] float delayStart = 0.5f;
    [SerializeField] Text textArea;

    private void OnEnable()
    {
        
    }


    public void DisplayMessage(string message)
    {
        textArea.text = "";

        textArea.DOText(message, message.Length * textSpeed)
            .SetDelay(delayStart)
            .Play();
    }

 
}
