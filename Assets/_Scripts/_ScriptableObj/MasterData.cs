using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "new MasterData", menuName = "MasterData")]
class MasterData : ScriptableObject
{
    public LevelUpTable levelUpTable;
    public List<Command> magicCommandMaster;
    public List<Item> itemMaster;

    public Command FindMagicCommandFromMaster(string commandName)
    {
        var command = magicCommandMaster.Find(o => o.commandName == commandName);
        return command;
    }

    public Item FindItemFromMaster(string itemName)
    {
        var item = itemMaster.Find(o => o.itemName == itemName);
        return item;
    }
}