using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Conversation", menuName = "ConversationData")]
public class ConversationData : ScriptableObject
{
    public ConversationLine[] conversationLines;
    public SubConverSation[] subConverSations;
    public UnityEvent[] conversatinEvents;
}

[System.Serializable]
public struct ConversationLine
{
    [TextArea(2, 5)]
    public string text;
    public bool eventExec;
    public int eventNum;
}

[System.Serializable]
public struct SubConverSation
{
    [TextArea(2, 5)]
    public string text;
    public ConversationData conversationData;
    public bool eventExec;
    public int eventNum;
}