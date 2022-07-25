using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "NpcData", order = 1)]
public class NpcData : ScriptableObject
{
    [SerializeField] string npcName; 
    [SerializeField] int yMaxStep = 0;
    [SerializeField] int xMaxStep = 0;
    [SerializeField] float randomInterval = 3f;
    [SerializeField] ConversationData conversationData;

    bool inCamera;
    float currentTime;
    int xCurrentStep;
    int yCurrentStep;
    int moveDistance;
    int pixelUnit = 16;
    int randomRange = 4;

    [HideInInspector]
    public const int MOVE_UP = 1;
    [HideInInspector]
    public const int MOVE_DOWN = 2;
    [HideInInspector]
    public const int MOVE_LEFT = 3;
    [HideInInspector]
    public const int MOVE_RIGHT = 4;

    public string NpcName { get => npcName; }
    public int YMaxStep { get => yMaxStep; }
    public int XMaxStep { get => xMaxStep; }
    public float RandomInterval { get => randomInterval; }
    public ConversationData ConversationData { get => conversationData; }
    public bool InCamera { get => inCamera; set => inCamera = value; }
    public float CurrentTime { get => currentTime; set => currentTime = value; }
    public int XCurrentStep { get => xCurrentStep; set => xCurrentStep = value; }
    public int YCurrentStep { get => yCurrentStep; set => yCurrentStep = value; }
    public int MoveDistance { get => moveDistance; set => moveDistance = value; }
    public int PixelUnit { get => pixelUnit; }
    public int RandomRange { get => randomRange; }
}
