using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleEnd : BattleTimeline
{
    public void FadeOut()
    {
        battleCanvas.MessageText.Deactivate();

        PlayableStop();
        battleCanvas.SceneFadeOut();
    }

    public void UnloadScene()
    {
        Debug.Log("-----------------battle end scene unload");

        //PlayerInfo.SavePoint savePoint = battleManager.PlayerAction.playerInfo.savePoint;
        //if (battleManager.PlayerAction.playerInfo.dead)
        //{
        //    battleManager.PlayerAction.playerInfo.currentScene = savePoint.savedScene;
        //    battleManager.PlayerAction.playerInfo.currentQuest = savePoint.savedLocation;
        //}

        //SceneManager.LoadSceneAsync(battleManager.PlayerAction.playerInfo.currentScene);
    }
}
