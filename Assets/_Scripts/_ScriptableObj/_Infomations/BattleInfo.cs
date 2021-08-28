using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New BattleInfo", menuName = "BattleInfo")]
public class BattleInfo : ScriptableObject
{
    public bool isBattle;
    public LivingMonsterList livingMonsterList;
}
