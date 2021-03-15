using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Conversation", menuName = "ConversationData")]
public class ConversationData : ScriptableObject
{
    [TextArea(2, 5)]
    public string dynamicText;
    public Conversation[] conversationLines;
    public Conversation[] subConverSationLines;
    public UnityEvent[] conversatinEvents;
}

[System.Serializable]
public struct Conversation
{
    [TextArea(2, 5)]
    public string text;
    public ConversationData conversationData;
    public bool eventExec;
    public int eventNum;
}
