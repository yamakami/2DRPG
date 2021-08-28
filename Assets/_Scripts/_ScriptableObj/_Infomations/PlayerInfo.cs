using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerInfo", menuName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    public string playerName;
    public string currentScene;
    public string currentQuestLocation;
    public int currentMonsterAreaIndex;
    public Vector2 playerLastPosition;
    public Vector2 playerLastFacing;

    public List<Command> battleCommands = new List<Command>();
    public List<Command> magicCommands = new List<Command>();
    public List<Item> items = new List<Item>();

    public Status status;

    [System.Serializable]
    public struct Status
    {
        [SerializeField] public int lv;
        [SerializeField] public int maxHP;
        [SerializeField] public int maxMP;
        [SerializeField] public int hp;
        [SerializeField] public int mp;
        [SerializeField] public int attack;
        [SerializeField] public int defence;
        [SerializeField] public int exp;
        [SerializeField] public int gold;
    }
}
