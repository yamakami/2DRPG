using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;

public class LocationManager : MonoBehaviour
{
    QuestManager questManager;
    Fader fader;
    CancellationTokenSource tokenSource = new CancellationTokenSource();

    public Fader Fader { set => fader = value; }
    public QuestManager QuestManager {set => questManager = value; }

    public async void MoveToBattleScene()
    {
        questManager.Player.StopPlayer();
        questManager.GameInfo().playerFreeze = true; 
        questManager.BattleInfo().isBattle = true;

        var cancelToken =  tokenSource.Token;

        var alphaUpTo = 1;
        fader.Flash(new Color32(255, 255, 255, 255), alphaUpTo);

        await UniTask.WaitUntil(() => fader.Available(), cancellationToken: cancelToken);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Battle");
        await UniTask.WaitUntil(() => asyncLoad.isDone, cancellationToken: cancelToken);
    }

    public async void FadeAndChangeLocation(StartPosition position)
    {
        var cancelToken = tokenSource.Token;
        var player = questManager.Player;
        var alphaUpTo = 1;

        player.StopPlayer();

        fader.SetAudioClip(position.audioClip).Fade(alphaUpTo);

        await UniTask.WaitUntil(() => fader.Available(), cancellationToken: cancelToken);

        var sceneName = position.sceneName;
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            var gameInfo = questManager.GameInfo();
            gameInfo.loadingSceneWithFade = true;
            gameInfo.playerFreeze = true; 
            gameInfo.playerStartPosition = position;

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            await UniTask.WaitUntil(() => asyncLoad.isDone, cancellationToken: cancelToken);
            return;
        }

        FadeIn(position);
    }

    public async void FadeIn(StartPosition position = null)
    {
        var cancelToken =  tokenSource.Token;
        var gameInfo = questManager.GameInfo();
        var playerInfo = questManager.PlayerInfo();
        var battleInfo = questManager.BattleInfo();
        var player = questManager.Player;

        var questSceneName = playerInfo.currentScene;
        var questLocationNum = playerInfo.currentQuestLocationIndex;
        var questLocationAreaNum = playerInfo.currentMonsterAreaIndex;
        var initPosition =playerInfo.playerLastPosition;
        var facingTo = playerInfo.playerLastFacing;

        if (!battleInfo.isBattle)
        {
            position = (position) ? position : gameInfo.playerStartPosition;
            questSceneName = position.sceneName;
            questLocationNum = position.locationIndex;
            questLocationAreaNum = 0;
            initPosition = position.startPosition;
            facingTo = position.facingTo;
        }

        var locationTo = questManager.FindTargetLocation(questLocationNum);
        questManager.SetCurrentQuest(questSceneName, questLocationNum, questLocationAreaNum);

        player.ResetPosition(facingTo, initPosition);

        fader.SetAlpha(1);

        var delay = 0.5f;
        var alphaUpTo = 0;
        fader.Fade(alphaUpTo, delay);

        await UniTask.WaitUntil(() => fader.Available(), cancellationToken: cancelToken);

        gameInfo.loadingSceneWithFade = false;
        gameInfo.playerStartPosition = null;
        battleInfo.isBattle = false;
        player.EnableMove();
    }
}
