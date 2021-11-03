using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
public class SaveManager : CustomEventListener
{
    [SerializeField] Player player;
    [SerializeField] int saveLocationIndex;

    string saveDataFile = "/playdata.json";

    public bool load;

    private void Start() {
        if(load)
        {
            LoadPlayData();
            load =false;
        }      
    }

    public void LoadPlayData()
    {
        var playerInfo = player.QuestManager.PlayerInfo();
        var json = System.IO.File.ReadAllText(Application.dataPath + saveDataFile);
        var playDataFormat = JsonUtility.FromJson<PlayDataFormat>(json);
        var masterData = playerInfo.masterData;

        playerInfo.playerName = playDataFormat.playerName;
        masterData.levelUpTable.reCalculate = playDataFormat.levelUpRecalculate;

        playerInfo.currentScene = playDataFormat.savedLocationScene;

        playerInfo.currentQuestLocationIndex = playDataFormat.currentQuestLocationIndex;
        playerInfo.currentMonsterAreaIndex = playDataFormat.currentMonsterAreaIndex;

        playerInfo.playerLastPosition = playDataFormat.playerLastPosition;
        playerInfo.playerLastFacing = playDataFormat.playerLastFacing;

        playerInfo.savedLocationScene = playDataFormat.savedLocationScene;
        playerInfo.savedLocationIndex = playDataFormat.savedLocationIndex;
        playerInfo.status = playDataFormat.status;

        playerInfo.magicCommands.Clear();
        playerInfo.items.Clear();

        RestoreFromItemMaster(playDataFormat, playerInfo, masterData);
        RestoreFromCommandMaster(playDataFormat.magicCommands, playerInfo, masterData);
    }

    void RestoreFromItemMaster(PlayDataFormat playDataFormat, PlayerInfo playerInfo, MasterData masterData)
    {
        var equipmentFormat = playDataFormat.equipments;
        var itemMaster = masterData.itemMaster;

        foreach(var item in playDataFormat.items)
        {
            var asset = masterData.FindItemFromMaster(item.itemName);
            asset.player_possession_count = item.itemPossessionCount;
            asset.isEquip = item.isEquip;
            playerInfo.items.Add(asset);

            if(asset.isEquip) playerInfo.SetEquipment(asset);
        }
    }

    void RestoreFromCommandMaster(string[] commands, PlayerInfo playerInfo, MasterData masterData)
    {
        var magicMaster = masterData.magicCommandMaster;
        foreach(var command in commands)
        {
            var asset = masterData.FindMagicCommandFromMaster(command);
            playerInfo.magicCommands.Add(asset);
        }
    }

    public void SavePlayData()
    {
        var playDataFormat = new PlayDataFormat();
        var playerInfo = player.QuestManager.PlayerInfo();


        var levelUpTable = playerInfo.masterData.levelUpTable;

        playDataFormat.playerName = playerInfo.playerName;
        playDataFormat.levelUpRecalculate = levelUpTable.reCalculate;

        playDataFormat.savedLocationScene = playerInfo.currentScene;
        playDataFormat.savedLocationIndex = playerInfo.savedLocationIndex = saveLocationIndex;

        playDataFormat.currentQuestLocationIndex = playerInfo.currentQuestLocationIndex;
        playDataFormat.currentMonsterAreaIndex = playerInfo.currentMonsterAreaIndex;

        playDataFormat.playerLastPosition = playerInfo.playerLastPosition;
        playDataFormat.playerLastFacing = playerInfo.playerLastFacing;

        playDataFormat.status    = playerInfo.status;
        playDataFormat.equipments = SetEquipment(playerInfo);

        playDataFormat.magicCommands = SetCommandData(playDataFormat, playerInfo.magicCommands);
        playDataFormat.items = SetItemData(playerInfo.items);
        playDataFormat.levels = levelUpTable.levels;

        var file = Application.dataPath + saveDataFile;
        var streamWriter = new StreamWriter (file, false); // 上書き

        var json = JsonUtility.ToJson(playDataFormat, true);
        try
        {
            streamWriter.Write(json);
            streamWriter.Flush();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if (streamWriter != null) streamWriter.Dispose();
        }
    }

    string[] SetEquipment(PlayerInfo playerInfo)
    {
        var equippedItems = playerInfo.equipment.items;
        var equipPositionCount = equippedItems.Length;
        var equipments = new string[equipPositionCount];

        for(var i=0; i < equipPositionCount; i++)
        {
            equipments[i] = equippedItems[i]?.itemName;
        }
        return equipments;
    }

    string[] SetCommandData(PlayDataFormat dataFormat, List<Command> commands)
    {
        var length = commands.Count;
        var commandArray = new String[length];
        for(var i=0; i < length; i++ )
        {
            commandArray[i] = commands[i].commandName;
        }
        return commandArray;
    }

    ItemFormat[] SetItemData(List<Item> items)
    {
        var itemLength = items.Count;
        var itemArray = new ItemFormat[itemLength];

        for(var i=0; i < itemLength; i++ )
        {
            var itemFormat = new ItemFormat();

            itemFormat.itemName = items[i].itemName;
            itemFormat.itemPossessionCount = items[i].player_possession_count;
            itemFormat.isEquip = items[i].isEquip;
            itemArray[i] = itemFormat;
        }
        return itemArray;
    }

    class PlayDataFormat
    {
        public string playerName;
        public bool levelUpRecalculate;
        public int currentQuestLocationIndex;
        public int currentMonsterAreaIndex;
        public string savedLocationScene;
        public int savedLocationIndex;
        public Vector2 playerLastPosition;
        public Vector2 playerLastFacing;
        public string[] magicCommands;
        public ItemFormat[] items;
        public PlayerInfo.Status status;
        public string[] equipments;
        public LevelUpTable.Level[] levels;
    }

    [System.Serializable]
    class ItemFormat
    {
        public string itemName;
        public int itemPossessionCount;
        public bool isEquip;
    }
}
