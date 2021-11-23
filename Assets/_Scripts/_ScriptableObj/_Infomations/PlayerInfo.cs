using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New PlayerInfo", menuName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject, ICharacterStatable
{
    public MasterData masterData;
    public StringStringStringBoolDict sceneEvents;
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

    public Equipment equipment;

    public string Name { get => playerName; set => playerName = value; }
    public int MaxHP { get => status.maxHP; }
    public int MaxMP { get => status.maxMP; }
    public int Hp { get => status.hp; set => status.hp = value; }
    public int Mp { get => status.mp; set => status.mp = value; }

    [System.Serializable]
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

    [System.Serializable]
    public class Equipment
    {
        public Item[] items = new Item[6];
    }

    public void UnEquipped(int index)
    {
        var item = equipment.items[index];
        if(item != null) item.isEquip = false;
        equipment.items[index] = null;
    }

    public void SetEquipment(Item item)
    {
        var position = (int)item.equipPosition;
         UnEquipped(position);

        item.isEquip = true;
        equipment.items[position] = item;
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
