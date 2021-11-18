using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New LevelUpTable", menuName = "LevelUpTable")]
public class LevelUpTable : ScriptableObject
{
    public bool reCalculate;
    public PlayerInfo playerInfo;
    public MagicLevel[] magiclevels;
    public int firstLevelUpAmount = 18;
    public Level[] levels;

    [System.Serializable]
    public class Level
    {
        public int levelIndex;
        public int minimumExp;
        public int nextLevelUpAmount;
        public int hp;
        public int mp;
        public int attack;
        public int defence;
        public string[] magicCommands;
    }

    [System.Serializable]
    public class MagicLevel
    {
        public Command magicCommand;
        public int levelRangeBegin;
        public int levelRangeEnd;
    }

    public void Calculate()
    {
        if (!reCalculate)
            return;

        if(levels.Length < magiclevels.Last().levelRangeEnd)
            throw new System.Exception("levels length is not enough. It should be more than MagicLevel.levelRangeEnd");

        for (var i = 0; i < levels.Length; i++)
        {
            levels[i].levelIndex = i + 1;
            levels[i].hp      = Random.Range(0, 13);
            levels[i].mp      = Random.Range(0, 13);
            levels[i].attack  = Random.Range(0, 13);
            levels[i].defence = Random.Range(0, 13);

            System.Array.Resize(ref levels[i].magicCommands, 0);

            if (i == 0)
            {
                levels[0].nextLevelUpAmount = firstLevelUpAmount;
                continue;
            }


            var ex = levels[i - 1].nextLevelUpAmount;
            var cur = levels[i - 1].minimumExp;
            var seed = Random.Range(1.3f, 1.8f);

            var nextEx = (int) System.Math.Round(ex * seed, System.MidpointRounding.AwayFromZero);

            levels[i].nextLevelUpAmount = nextEx;
            levels[i].minimumExp = ex + cur;
        }

        foreach(var ml in magiclevels)
        {
            var index = Random.Range(ml.levelRangeBegin, ml.levelRangeEnd + 1) -1;
            var magicCommands = levels[index].magicCommands;

            System.Array.Resize(ref levels[index].magicCommands, levels[index].magicCommands.Length + 1);
            levels[index].magicCommands[ levels[index].magicCommands.Length -1 ] = ml.magicCommand.commandName;
        }

        playerInfo.InitializePlayerInfo();
        reCalculate = false;
    }
}
