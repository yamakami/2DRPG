using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New PlayerInfo", menuName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    public string playerName;
    public bool freeze;
    public bool startConversation;
    public bool startBattle;

    public string currentScene;
    public string currentQuest;

    public Vector2 lastPosition;
    public Vector2 lastMove;
     
    public Status status;

    public List<Command> battleCommands      = new List<Command>();
    public List<Command> magicAttackCommands = new List<Command>();
    public List<Command> magicHealCommands   = new List<Command>();
    public List<Item> items = new List<Item>();

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



