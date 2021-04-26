using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : UIBase
{
    [SerializeField] PlayerMove playerMove;
    [SerializeField] Canvas canvas;
    [SerializeField] MessageText messageText = default;
    [SerializeField] Button nextButton = default;
    [SerializeField] MessageSelect messageSelect = default;

    ConversationData conversationData;
    Queue<Conversation> conversations = new Queue<Conversation>();

    public ConversationData ConversationData { get => conversationData; set => conversationData = value; }
    public Queue<Conversation> Conversations { get => conversations; }
    public Button NextButton { get => nextButton; }
    public MessageSelect MessageSelect { get => messageSelect; }
    public Canvas Canvas { get => canvas; }
    public MessageText MessageText { get => messageText; }

    void Start()
    {
        if (MessageSelect)
            MessageSelect.MessageBox = this;
    }

    public void PrepareConversation(ConversationData conversationData)
    {
        Activate();
        this.conversationData = conversationData;
        
        conversations.Clear();

        foreach (var conversation in conversationData.conversationLines)
        {
            conversations.Enqueue(conversation);
        }

        ForwardConversation(conversations.Dequeue());
    }

    public void NextMessage()
    {
        if (!MessageText.Available)
            return;

        if (conversations.Count == 0)
        {
            if (0 < conversationData.subConverSationLines.Length)
            {
                MessageSelect.Activate();
                return;
            }

            var lastIndex = conversationData.conversationLines.Length - 1;
            if (conversationData.conversationLines[lastIndex].eventExec)
            {
                var eventNum = conversationData.conversationLines[lastIndex].eventNum;
                conversationData.conversatinEvents[eventNum].Invoke();
                base.Deactivate();
                return;
            } 

            Deactivate();
            return;
        }

        ForwardConversation(conversations.Dequeue());
    }

    public void ForwardConversation(Conversation conversation)
    {
        MessageText.DisplayMessage(conversation);
    }

    public void SortOrderFront()
    {
        canvas.sortingOrder = 3;
        messageSelect.Canvas.sortingOrder = 3;
    }

    public void SortOrderBack()
    {
        canvas.sortingOrder = 0;
        messageSelect.Canvas.sortingOrder = 0;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        playerMove.StopConversation();
    }

    public void DeactivateInTalk()
    {
        base.Deactivate();
        playerMove.QuestUIManager.InTalk = false;
    }

    void OnDisable()
    {
        SortOrderBack();
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
