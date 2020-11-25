using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Command", menuName = "Command")]
public class Command : ScriptableObject
{
    public enum COMMAND_TYPE
    {
        MAGIC_ATTACK,
        MAGIC_HEAL,
        ATTACK_PLAYER,
        ATTACK_MONSTER,
        THRASH,
    }

    public COMMAND_TYPE commandType;
    public string commandName;
    public string nameKana;
    public AudioClip audioClip;
    [TextArea(2, 5)]
    public string description;
    public MagicInfo magicInfo = default;
}

[System.Serializable]
public class MagicInfo
{
    public enum MAGIC_TARGET
    {
        ONE,
        ALL,
    }

    public MAGIC_TARGET magicTarget;
    public int requirementEx;
    public int consumptionMp;
    [SerializeField] int[] attackPoint = default;
    [TextArea(2, 5)]
    public string resultMessage;

    public int MagicAttack()
    {
        return Random.Range(attackPoint[0], attackPoint[1] + 1);
    }

    public int Heal(int hp, int maxHP)
    {
        int healValue = Random.Range(attackPoint[0], attackPoint[1] + 1);

        int total = hp + healValue;
        if (maxHP < total)
        {            
            return healValue - (total - maxHP);
        }
        return healValue;
    }
}
