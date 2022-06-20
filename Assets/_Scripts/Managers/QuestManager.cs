using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] Player player;
    QuestLocation currentLocation;

    public QuestLocation CurrentLocation { set => currentLocation = value; }

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
