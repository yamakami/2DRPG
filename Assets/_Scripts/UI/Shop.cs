using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, ICustomEventListener
{
    [SerializeField] CustomEventTrigger shopTrigger;
    NpcData npcData;

    public NpcData NpcData { get => npcData; set => npcData = value; }

    void OnEnable()
    {
        shopTrigger.AddEvent(this);
    }

    void OnDisable()
    {
        shopTrigger.RemoveEvent(this);
    }

    public void OnEventRaised()
    {
        ShopStart();
    }

    void ShopStart()
    {
        Debug.Log($"----------------hello #{npcData.NpcName}");
    }

}
