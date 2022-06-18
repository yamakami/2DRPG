using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] Player player;
    QuestLocation currentLocation;

    public QuestLocation CurrentLocation { set => currentLocation = value; }

    void Update()
    {
        // player.SubstituteUpdate();

        for (var i = 0; i < currentLocation.ActorNpcs.Length; i++)
        {
            var npc = currentLocation.ActorNpcs[i];
            npc.SubstituteUpdate();
        }
    }

    void FixedUpdate()
    {
        // player.SubstituteFixedUpdate();

        for (var i = 0; i < currentLocation.ActorNpcs.Length; i++)
        {
            var npc = currentLocation.ActorNpcs[i];
            npc.SubstituteFixedUpdate();
        }
    }
}
