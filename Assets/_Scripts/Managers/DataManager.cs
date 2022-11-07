using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] Item[] itemMaster;

    Dictionary<string, Item> itemDictionary = new Dictionary<string, Item>();

    public PlayerData PlayerData { get => playerData; }
    public Item[] ItemMaster { get => itemMaster; }
    public Dictionary<string, Item> ItemDictionary { get => itemDictionary; }

    void Awake()
    {
        SetItemDictionary();
    }

    void SetItemDictionary()
    {
        for(var i=0; i < itemMaster.Length; i++)
        {
            var item = itemMaster[i];
            ItemDictionary.Add(item.name, item);
        }
    }

    public Item GetItemByName(string itemName)
    {
        if(itemDictionary.ContainsKey(itemName))
            return itemDictionary[itemName];
        
        return null;
    }
}
