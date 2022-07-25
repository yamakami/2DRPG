using UnityEngine;

[CreateAssetMenu(fileName = "ConversationData", menuName = "Conversation", order = 0)]
public class ConversationData : ScriptableObject
{
    public Conversation[] conversations;
    public Conversation[] options;

    [System.Serializable]
    public class Conversation
    {
        [TextArea(2, 5)]
        public string text;
        public ConversationData nextConversationData;
        // public QuestEventTrigger questEventTrigger;
    }    
}