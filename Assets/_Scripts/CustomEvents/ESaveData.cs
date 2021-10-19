using System;
using System.IO;
using UnityEngine;

public class ESaveData : CustomEventListener
{

    [SerializeField] GameInfo gameInfo;
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] BattleInfo battleInfo;

    public void SaveData()
    {
        // var playData = new PlayData();
        // var playDataJson  = JsonUtility.ToJson(playData);

        // playData.gameInfo = JsonUtility.ToJson(gameInfo);
        // playData.playerInfo = JsonUtility.ToJson(playerInfo);
        // playData.battleInfo = JsonUtility.ToJson(battleInfo);
var a = JsonUtility.ToJson(playerInfo);

        var path = Application.dataPath + "/player_data.json";

        var writer = new StreamWriter (path, false); // 上書き

        try
        {
            writer.Write(a);
            writer.Flush();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if (writer != null) writer.Dispose();
        }

    }

    [SerializeField]
    class PlayData
    {
        public GameInfo gameInfo;
        public PlayerInfo playerInfo;
        public BattleInfo battleInfo;
    }
}
