using UnityEngine;
using System;

[Serializable]
public class StringBoolDictionary : SerializableDictionary<string, bool> {}

[Serializable]
public class StringStringBoolDictionary : SerializableDictionary<string, StringBoolDictionary> {}

[Serializable]
public class StringStringStringBoolDictionary : SerializableDictionary<string, StringStringBoolDictionary> {}

[CreateAssetMenu(fileName = "new StringStringStringBoolDict", menuName = "StringStringStringBoolDict", order = 0)]

[Serializable]
public class StringStringStringBoolDict: ScriptableObject
{
    [SerializeField] StringStringStringBoolDictionary dictionary;

    public StringStringStringBoolDictionary Dictionary { get => dictionary; set => dictionary = value; }


    public StringStringStringBoolDictionary CreateParentKeys(string key1, string key2)
    {
        if(!dictionary.ContainsKey(key1))
            dictionary.Add(key1, new StringStringBoolDictionary());

        if(!dictionary[key1].ContainsKey(key2))
            dictionary[key1].Add(key2, new StringBoolDictionary());

        return dictionary;
    }

    public bool KeysExist(string key1, string key2, string key3)
    {
        if(dictionary.ContainsKey(key1) && dictionary[key1].ContainsKey(key2) && dictionary[key1][key2].ContainsKey(key3)) return true;
        return false;
    }
}

