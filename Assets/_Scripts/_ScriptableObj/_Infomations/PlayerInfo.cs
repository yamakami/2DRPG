using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New PlayerInfo", menuName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    public MasterData masterData;
    public string playerName;
    public string currentScene;
    public int currentQuestLocationIndex;
    public int currentMonsterAreaIndex;
    public Vector2 playerLastPosition;
    public Vector2 playerLastFacing;

    public List<Command> battleCommands = new List<Command>();
    public List<Command> magicCommands = new List<Command>();
    public List<Item> items = new List<Item>();

    public string savedLocationScene;
    public int savedLocationIndex;

    public Status status;

    [System.Serializable]
    public class Status
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

    public void InitializePlayerInfo()
    {
        status.lv = 1;
        status.maxHP = 8;
        status.maxMP = 8;
        status.hp = 8;
        status.mp = 8;
        status.attack = 10;
        status.defence = 8;
        status.exp = 0;
        status.gold = 0;

        masterData.RestItemPlayerPossession();
        magicCommands.Clear();
        items.Clear();
    }
}
