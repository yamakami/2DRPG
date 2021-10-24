using UnityEngine;

[CreateAssetMenu(fileName = "New BattleInfo", menuName = "BattleInfo")]
public class BattleInfo : ScriptableObject
{
    public bool isBattle;
    public bool isQuestFail;
    public LivingMonsterList livingMonsterList;
}
