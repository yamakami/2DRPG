using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameInfo", menuName = "GameInfo")]
public class GameInfo : ScriptableObject
{
    public LevelUpTable levelUpTable;
    public bool loadingSceneWithFade;
    public StartPosition playerStartPosition;
    public bool playerFreeze;

    public PlayerRestoreInfo playerRestoreInfo;

    [System.Serializable]
    public class PlayerRestoreInfo
    {
        public string Scene;
        public int locationIndex;
        public int restoreLocationIndex;
    }
}
