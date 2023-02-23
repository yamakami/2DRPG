using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CustomEventTrigger", menuName = "CustomEventTrigger")]
public class CustomEventTrigger : ScriptableObject
{
    List<ICustomEventListener> listeners = new List<ICustomEventListener>();

    public void AddEvent(ICustomEventListener listener)
    {
       if(! listeners.Contains(listener) ) listeners.Add(listener);
    }

    public void Invoke()
    {
        for(var i = listeners.Count -1; 0 <= i; i-- )
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RemoveEvent(ICustomEventListener listener)
    {
       if(! listeners.Contains(listener) ) listeners.Remove(listener);
    }
}
