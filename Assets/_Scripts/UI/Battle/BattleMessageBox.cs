using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class BattleMessageBox : UIBase
{
    [SerializeField] MessageText messageText;
    StringBuilder stringBuilder = new StringBuilder();

    public StringBuilder StringBuilder { get => stringBuilder; }

    public void DisplayMessage(float delay = 0f)
    {
        messageText.TweenText(stringBuilder.ToString(), delay);
    }

    public bool Available()
    {
        return messageText.Available;
    }
}
