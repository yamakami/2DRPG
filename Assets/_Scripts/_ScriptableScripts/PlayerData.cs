using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "_PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] string playerName;
    [SerializeField] List<Item> items = new List<Item>(200);

    public List<Item> Items { get => items; set => items = value; }

    public void AddItem(string _name)
    { 
        var item = SystemManager.DataManager().GetItemByName( _name);

        if(!items.Contains(item)) items.Add(item);
        item.AddCommand();
    }

    public void removeItem(string _name)
    { 
       var item = SystemManager.DataManager().GetItemByName( _name);

        item.RemoveCommand();
        if(item.player_possession_count < 1) items.Remove(item);
    }
}
