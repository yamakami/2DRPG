using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public List<QuestLocation> questLocations = new List<QuestLocation>();

    [System.Serializable]
    public struct QuestLocation
    {
        public string questName;
        public Area[] areas;
    }

    [System.Serializable]
    public struct Area
    {
        public string areaName;
        public int monsterDensity;
        public int maxUnit;
        public Monster[] monsters;
    }
}



