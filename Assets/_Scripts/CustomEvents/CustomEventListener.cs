using UnityEngine;
using UnityEngine.Events;


public abstract class CustomEventListener : MonoBehaviour
{
    [SerializeField] UnityEvent inspectorObject;
    [SerializeField] CustomEventTrigger eventTrigger;

    void OnEnable()
    {
        eventTrigger.AddEvent(this);
    }
    public void OnEventRaised()
    {
        inspectorObject.Invoke();
    }

    void OnDisable()
    {
        eventTrigger.RemoveEvent(this);
    }
}
