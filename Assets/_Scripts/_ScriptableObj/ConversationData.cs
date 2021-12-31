using UnityEngine;

[CreateAssetMenu(fileName = "_New Conversation", menuName = "Conversation")]
public class ConversationData : ScriptableObject
{
    public Conversation[] conversationLines;
    public Conversation[] subConverSationLines;

    [System.Serializable]
    public class Conversation
    {
        [TextArea(2, 5)]
        public string text;
        public ConversationData nextConversationData;
    }
}
