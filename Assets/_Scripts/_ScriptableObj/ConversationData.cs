using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "ConversationData")]
public class ConversationData : ScriptableObject
{
    [TextArea(2, 5)]
    public string dynamicText;
    public Conversation[] conversationLines;
    public Conversation[] subConverSationLines;

    [System.Serializable]
    public class Conversation
    {
        [TextArea(2, 5)]
        public string text;
        public ConversationData conversationData;

        public CustomEventTrigger eventTrigger = default;
    }
}
