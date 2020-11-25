using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelUpTable", menuName = "LevelUpTable")]
public class LevelUpTable : ScriptableObject
{
    public bool reCalculate;
    public Level[] levels;

    [System.Serializable]
    public struct Level
    {
        public int nextLevelUpAmount;
        public int goalExp;
        public int hp;
        public int attack;
        public int defence;
        public string magicName;
    }

    public void Calculate()
    {
        if (!reCalculate)
            return;
        
        for (var i = 0; i < levels.Length; i++)
        {
            levels[i].hp      = Random.Range(3, 14);
            levels[i].attack  = Random.Range(3, 14);
            levels[i].defence = Random.Range(3, 14);

            if (i == 0)
                continue;

            var ex = levels[i - 1].nextLevelUpAmount;
            var cur = levels[i - 1].goalExp;

            var nextEx = (int) System.Math.Round(ex * 1.1f, System.MidpointRounding.AwayFromZero);

            levels[i].nextLevelUpAmount = nextEx;
            levels[i].goalExp = ex + cur;

        }
    }
}