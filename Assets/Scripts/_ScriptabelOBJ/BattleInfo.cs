using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New BattleInfo", menuName = "BattleInfo")]
public class BattleInfo : ScriptableObject
{
    public string questName;
    public int areaIndex;
    public Quest.Area[] areas;
    public List<Monster> monsters = new List<Monster>();
}



