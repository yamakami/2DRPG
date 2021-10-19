using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CustomEventTrigger", menuName = "CustomEvent")]
public class CustomEventTrigger : ScriptableObject
{
    List<CustomEventListener> eventListeners = new List<CustomEventListener>();

    public void AddEvent(CustomEventListener ce)
    {
       if(! eventListeners.Contains(ce) ) eventListeners.Add(ce); 

    }

    public void Invoke()
    {
        for(var i = eventListeners.Count -1; 0 <= i; i-- )
        {
            eventListeners[i].OnEventRaised();
        }
    }

    public void RemoveEvent(CustomEventListener ce)
    {
       if(eventListeners.Contains(ce) ) eventListeners.Remove(ce); 
    }
}
