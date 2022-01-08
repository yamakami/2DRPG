using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButton : ConversationSelectButton
{
    [SerializeField] EventTrigger eventTrigger;

    public EventTrigger EventTrigger { get => eventTrigger; }
}
