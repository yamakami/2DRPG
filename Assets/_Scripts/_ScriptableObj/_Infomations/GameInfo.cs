using UnityEngine;

[CreateAssetMenu(fileName = "New GameInfo", menuName = "GameInfo")]
public class GameInfo : ScriptableObject
{
    public LevelUpTable levelUpTable;
    public bool loadingSceneWithFade;
    public StartPosition playerStartPosition;
    public bool playerFreeze;
}
