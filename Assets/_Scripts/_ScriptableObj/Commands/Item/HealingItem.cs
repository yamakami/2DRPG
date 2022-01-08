using UnityEngine;
using System;

[CreateAssetMenu(fileName = "_Item", menuName = "Command/Item")]
[Serializable]
public class HealingItem : CommandItem
{    
    public enum HEALING_TYPE
    {
        HP,
        MP
    }
    public string hello;
}
