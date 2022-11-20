using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "_PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] string playerName;
    [SerializeField] int gold;
    [SerializeField] List<Item> items = new List<Item>(200);

    public string PlayerName { get => playerName; }
    public int Gold { get => gold; set => gold = value; }
    public List<Item> Items { get => items; set => items = value; }

    public void AddItem(string _name, int amount)
    { 
        var item = SystemManager.DataManager().GetItemByName( _name);

        for(var i = 0; i < amount; i++)
        {
            if(!items.Contains(item)) items.Add(item);
            item.AddCommand();
        }

    }

    public void removeItem(string _name, int amount)
    { 
       var item = SystemManager.DataManager().GetItemByName( _name);

        for(var i = 0; i < amount; i++)
        {
            item.RemoveCommand();
            if(item.Player_possession_count < 1) items.Remove(item);
        }
    }
}
