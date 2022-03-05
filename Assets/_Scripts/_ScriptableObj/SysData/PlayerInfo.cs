using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


[CreateAssetMenu(fileName = "PlayerInfo", menuName = "SysDatas/PlayerInfo")]

[Serializable]
public class PlayerInfo : ScriptableObject, IStatus
{
    public string playerName;
    public Status status;

    public CommandItem[] equip = new CommandItem[6];
    public List<CommandItem> items = new List<CommandItem>(40);
    public List<CommandMagic> magics = new List<CommandMagic>(40);

    [Serializable]
    public class Status
    {
        public int lv;
        public int maxHP;
        public int maxMP;
        public int hp;
        public int mp;
        public int attack;
        public int defence;
        public int exp;
        public int gold;
    }

    public string CharacterName { get => playerName; }
    public int HP { get => status.hp; set => status.hp = value; }
    public int MP { get => status.mp; set => status.mp = value; }
    public int MaxHP { get => status.maxHP; }
    public int MaxMP { get => status.maxMP; }

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
    }
}
