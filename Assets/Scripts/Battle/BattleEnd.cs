using UnityEngine.SceneManagement;

public class BattleEnd : BattleTimeline
{
    public void FadeOut()
    {
        battleCanvas.MessageBox.Close();

        battleManager.PlayableStop();
        battleCanvas.SceneFadeOut();
    }

    public void UnloadScene()
    {
        SceneManager.LoadSceneAsync(battleManager.PlayerAction.playerInfo.currentScene);
    }
}
