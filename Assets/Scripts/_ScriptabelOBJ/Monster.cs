using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Monster")]
public class Monster : ScriptableObject
{
    public string monsterName;
    public Sprite monsterSprite;
    public Command[] commands;
    public Command[] healCommands;
    public int[] itemWeights;
    public Item[] items;

    public Status status;

    [System.Serializable]
    public struct Status
    {
        public int[] hp;
        public int[] attack;
        public int[] defence;
        public int[] mp;
        public int[] exp;
        public int[] gold;
    }

    public Item DropItem()
    {

        if (items.Length == 0)
            return null;

        int drop = Random.Range(0, 101);
        int itemIndex = 0;
        foreach (var weight in itemWeights)
        {
            if(drop <= weight)
                break;

            itemIndex++;
        }
        return items[itemIndex];
    }
}
