using UnityEngine;
using UnityEngine.Events;

public abstract class QuestEventListener : MonoBehaviour
{
    [SerializeField] QuestEventTrigger eventTrigger;

    public QuestEventTrigger EventTrigger { get => eventTrigger; }

    void OnEnable()
    {
        eventTrigger.AddEvent(this);
    }
    public abstract void OnEventRaised();

    void OnDisable()
    {
        eventTrigger.RemoveEvent(this);
    }
}
