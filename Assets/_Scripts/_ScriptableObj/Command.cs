using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Command", menuName = "Command")]
public class Command : ScriptableObject
{
    public bool useForQuest;
    public bool useForBattle;
    public enum COMMAND_TYPE
    {
        FIST_ATTACK,
        MAGIC_ATTACK,
        MAGIC_DEFENCE,
        MAGIC_HEAL
    }
    public COMMAND_TYPE commandType;
    public string commandName;
    public string nameKana;
    public AudioClip audioClip;

    [TextArea(2, 5)]
    public string description;
    public MagicCommand magicCommand = default;
}

[System.Serializable]
public class MagicCommand
{
    public enum MAGIC_TARGET
    {
        ONE,
        ALL,
    }

    public MAGIC_TARGET magicTarget;
    public int consumptionMp;

    [SerializeField] int[] attackHealPoint;
    public int MagicAttack()
    {
        return Random.Range(attackHealPoint[0], attackHealPoint[1] + 1);
    }
 
    public int Heal(int hp, int maxHP)
    {
        var healValue = Random.Range(attackHealPoint[0], attackHealPoint[1] + 1);

        var total = hp + healValue;

        if (maxHP <= total)
            return healValue - (total - maxHP);

        return healValue;
    }
}
