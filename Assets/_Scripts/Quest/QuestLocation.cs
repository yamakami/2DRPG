using UnityEngine;

public class QuestLocation : MonoBehaviour
{
    [SerializeField] NPC[] actors;

    void OnEnable()
    {
        QuestManager.NpcActors = actors;
    }
}
