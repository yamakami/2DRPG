using UnityEngine;

public class QuestLocation : MonoBehaviour
{
    [SerializeField] QuestManager questManager;
    [SerializeField] BaseCharacter[] actorNpcs;

    public BaseCharacter[] ActorNpcs { get => actorNpcs; }

    void OnEnable()
    {
        questManager.CurrentLocation = this;
    }
}
