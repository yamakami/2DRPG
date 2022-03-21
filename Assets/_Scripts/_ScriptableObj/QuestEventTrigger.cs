using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuestEventTrigger", menuName = "QuestEventTrigger")]
public class QuestEventTrigger : ScriptableObject
{
    List<QuestEventListener> questEventListeners = new List<QuestEventListener>();

    public void AddEvent(QuestEventListener qe)
    {
       if(! questEventListeners.Contains(qe) ) questEventListeners.Add(qe); 

    }

    public void Invoke()
    {
        for(var i = questEventListeners.Count -1; 0 <= i; i-- )
        {
            questEventListeners[i].OnEventRaised();
        }
    }

    public void RemoveEvent(QuestEventListener qe)
    {
       if(questEventListeners.Contains(qe) ) questEventListeners.Remove(qe); 
    }
}
