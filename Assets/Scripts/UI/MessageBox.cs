using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class MessageBox : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove;
    [SerializeField] MessageText messageText = default;
    [SerializeField] Button nextButton = default;

    ConversationData conversationData;
    Queue<Conversation> conversations = new Queue<Conversation>();

    public MessageText MessageText { get => messageText; set => messageText = value; }

    void Update()
    {
        if (!playerMove.playerInfo.startConversation) return;
        if (isActiveAndEnabled) return;

        gameObject.SetActive(true);
        PrepareConversation(playerMove.characterMove.conversationData);
    }


    void PrepareConversation(ConversationData conversationData)
    {
        this.conversationData = conversationData;
        conversations.Clear();

        foreach (Conversation conversation in conversationData.conversationLines)
        {
            this.conversations.Enqueue(conversation);
        }

        //textField.text = null;
    }


    public void ForwardConversation(string line)
    {
        //messageText.DisplayMessage();
    }



    //float letterDisplaySpeed = 0.02f;
    //ConversationData conversationData;
    //Queue<ConversationLine> conversations = new Queue<ConversationLine>();
    //bool processing;

    //public Text TextField { get => textField; set => textField = value; }
    //public Button NextButton { get => nextButton; }
    //public float LetterDisplaySpeed { get => letterDisplaySpeed; set => letterDisplaySpeed = value; }
    //public ConversationData ConversationData { get => conversationData; set => conversationData = value; }
    //public Queue<ConversationLine> Conversations { get => conversations; set => conversations = value; }
    //public bool Processing { get => processing; set => processing = value; }

    //public void PrepareConversation(ConversationData conversationData)
    //{
    //    this.conversationData = conversationData;
    //    conversations.Clear();

    //    foreach (ConversationLine conversationLine in conversationData.conversationLines)
    //    {
    //        this.conversations.Enqueue(conversationLine);
    //    }

    //    textField.text = null;
    //}

    //public void ForwardConversation(string line)
    //{
    //    StartCoroutine(LetterDisplay(line));
    //}

    //IEnumerator LetterDisplay(string line)
    //{
    //    processing = true;
    //    var builder = new StringBuilder();

    //    foreach (char c in line.ToCharArray())
    //    {
    //        yield return new WaitForSeconds(letterDisplaySpeed);

    //        builder.Append(c);
    //        textField.text = builder.ToString();

    //    }

    //    processing = false;
    //}

    //public void Open()
    //{
    //    gameObject.SetActive(true);
    //}

    //public void Close()
    //{
    //    gameObject.SetActive(false);

    //    TextField.text = "";
    //}

    //public void DisplayMessage(StringBuilder str)
    //{
    //    if (!isActiveAndEnabled)
    //        return;

    //    ForwardConversation(str.ToString());
    //    str.Clear();
    //}

    //public bool MessageAcceptable()
    //{
    //    if (Processing)
    //        return false;

    //    return true;
    //}
}
