using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class MessageBox : MonoBehaviour
{
    [SerializeField] Text textField = default;
    [SerializeField] Button nextButton = default;

    float letterDisplaySpeed = 0.02f;
    ConversationData conversationData;
    Queue<string> conversations = new Queue<string>();
    bool processing;

    public Text TextField { get => textField; set => textField = value; }
    public Button NextButton { get => nextButton; }
    public float LetterDisplaySpeed { get => letterDisplaySpeed; set => letterDisplaySpeed = value; }
    public ConversationData ConversationData { get => conversationData; set => conversationData = value; }
    public Queue<string> Conversations { get => conversations; set => conversations = value; }
    public bool Processing { get => processing; set => processing = value; }

    public void PrepareConversation(ConversationData conversationData)
    {
        this.conversationData = conversationData;
        conversations.Clear();

        foreach (ConversationLine conversation in conversationData.conversationLines)
        {
            this.conversations.Enqueue(conversation.text);
        }

        textField.text = null;
    }
    
    public void ForwardConversation(string line)
    {
        StartCoroutine(LetterDisplay(line));
    }

    IEnumerator LetterDisplay(string line)
    {
        processing = true;
        var builder = new StringBuilder();

        foreach (char c in line.ToCharArray())
        {
            yield return new WaitForSeconds(letterDisplaySpeed);

            builder.Append(c);
            textField.text = builder.ToString();

        }

        processing = false;
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        //NextButton.gameObject.SetActive(false);
        gameObject.SetActive(false);

        TextField.text = "";
    }

    public void DisplayMessage(StringBuilder str)
    {
        if (!isActiveAndEnabled)
            return;

        ForwardConversation(str.ToString());
        str.Clear();
    }

    public bool MessageAcceptable()
    {
        if (Processing)
            return false;

        return true;
    }
}
