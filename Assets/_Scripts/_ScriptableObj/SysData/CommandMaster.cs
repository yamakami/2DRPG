using UnityEngine;
using System;

[CreateAssetMenu(fileName = "new CommandMaster", menuName = "CommandMaster")]
[Serializable]
public class CommandMaster : ScriptableObject
{
    public CommandItem[] items;
    public CommandMagic[] magics;
}

