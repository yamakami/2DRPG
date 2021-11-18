using UnityEngine;
using System;

[Serializable]
public class StringBoolDictionary : SerializableDictionary<string, bool> {}

[Serializable]
public class StringStringBoolDictionary : SerializableDictionary<string, StringBoolDictionary> {}

[CreateAssetMenu(fileName = "StringStringBoolDictionary", menuName = "string-string-bool-dictionary", order = 0)]

[Serializable]
public class StringStringBoolDict: ScriptableObject
{
    [SerializeField] string sceneName;
    [SerializeField] StringStringBoolDictionary dictionary;

    public StringStringBoolDictionary Dictionary { get => dictionary; set => dictionary = value; }
}

