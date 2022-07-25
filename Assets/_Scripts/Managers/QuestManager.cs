using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] UIQuest uIQuest;
    QuestLocation currentLocation;

    public QuestLocation CurrentLocation { set => currentLocation = value; }
    public UIQuest UIQuest { get => uIQuest; }

    void Awake()
    {
        uIQuest.UiInitialize();
    }

    void Update()
    {
        player.SubstituteUpdate();
        MoveCharacter("update");
    }

    void FixedUpdate()
    {
        player.SubstituteFixedUpdate();
        MoveCharacter("fixed");
    }

    void MoveCharacter(string type)
    {
        for (var i = 0; i < currentLocation.ActorNpcs.Length; i++)
        {
            var npc = currentLocation.ActorNpcs[i];

            if(type == "update") npc.SubstituteUpdate();
            if(type == "fixed") npc.SubstituteFixedUpdate();
        }
    }
}
