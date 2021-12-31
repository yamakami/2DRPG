using UnityEngine;

[CreateAssetMenu(fileName = "_New NpcData", menuName = "NpcData")]
public class NpcData : ScriptableObject
{
    public string npcName;
    public int yMaxStep = 0;
    public int xMaxStep = 0;
    public float randomInterval = 3f;
    public ConversationData conversationData;
}
