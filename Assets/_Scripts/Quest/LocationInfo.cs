using UnityEngine;

public class LocationInfo : MonoBehaviour
{
    [SerializeField] NPC[] actors;

    void OnEnable()
    {
        QuestManager.NpcActors = actors;
    }
}
