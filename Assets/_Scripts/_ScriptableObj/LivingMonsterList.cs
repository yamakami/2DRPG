using UnityEngine;

[CreateAssetMenu(fileName = "New LivingMonsterList", menuName = "LivingMonsterList")]
public class LivingMonsterList : ScriptableObject
{
    public int monsterDensity = 0;
    public int unitMaxNum = 1;
    public bool duplicate = false;
    public Monster[] livingMonsters;
}
