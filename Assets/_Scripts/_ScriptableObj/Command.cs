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
        MAGIC_HEAL,
        ITEM,
        ESCAPE,
    }
    public COMMAND_TYPE commandType;
    public string commandName;
    public string nameKana;
    public AudioClip audioClip;

    [TextArea(2, 5)]
    public string description;
    public MagicCommand magicCommand = default;

    public string ActionMessage()
    {
        var message = "";
        switch(commandType)
        {
            case COMMAND_TYPE.FIST_ATTACK:
                message = "{0}は{1}を攻撃した！";
                break;

            case COMMAND_TYPE.ITEM:
                message = "{0}は{1}を使った！";
                break;
            case COMMAND_TYPE.ESCAPE:
                message = "{0}は逃げだした";
                break;
            case COMMAND_TYPE.MAGIC_ATTACK:
            case COMMAND_TYPE.MAGIC_DEFENCE:
            case COMMAND_TYPE.MAGIC_HEAL:
                message = "{0}は{1}を唱えた！";
                break;
        }

        return message;
    }

    public string AffectMessage()
    {
        var message = "";
        switch(commandType)
        {
            case Command.COMMAND_TYPE.FIST_ATTACK:
            case Command.COMMAND_TYPE.MAGIC_ATTACK:
                message = "{0}は{1}HPのダメージを受けた！！！";;
                break;

            case Command.COMMAND_TYPE.MAGIC_HEAL:
                message = "{0}はHPが{1}回復した！";
                break;
        }

        return message;        
    }
 
    public static string NoDamagedMessage() { return "{0}はダメージを受けてない！！！！"; }
    public static string FailedEscapeMessage() { return "{しかし、まわりこまれてしまった！"; }
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

    public int AffectedValue(Command.COMMAND_TYPE commandType, ICharacterStatable target)
    {
        if(commandType == Command.COMMAND_TYPE.MAGIC_ATTACK) return AttackDamageValue();
        return HealValue(target.Hp, target.MaxHP);
    }

    int AttackDamageValue()
    {
        return Random.Range(attackHealPoint[0], attackHealPoint[1] + 1);
    }
 
    int HealValue(int hp, int maxHP)
    {
        var healValue = AttackDamageValue();
        var total = Mathf.Clamp(hp + healValue, 0, maxHP);

        return total - hp;
    }
}
