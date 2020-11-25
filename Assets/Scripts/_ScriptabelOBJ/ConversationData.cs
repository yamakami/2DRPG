using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "ConversationData")]
public class ConversationData : ScriptableObject
{
    public ConversationLine[] conversationLines;
    public SubConverSation[] subConverSations;
}

[System.Serializable]
public struct ConversationLine
{
    [TextArea(2, 5)]
    public string text;
}

[System.Serializable]
public struct SubConverSation
{
    [TextArea(2, 5)]
    public string text;
    public ConversationData conversationData;
}