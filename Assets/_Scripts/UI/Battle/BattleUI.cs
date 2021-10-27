using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;

public class BattleUI : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;
    [SerializeField] Fader fader;
    [SerializeField] BattleMessageBox battleMessageBox;
    [SerializeField] BattleSelector battleSelector;
    [SerializeField] FlowPrepare prepare;

    public Fader Fader { get => fader; }
    public BattleManager BattleManager { get => battleManager; }
    public BattleMessageBox BattleMessageBox { get => battleMessageBox; }
    public BattleSelector BattleSelector { get => battleSelector; }

    void Awake()       
    {
        prepare.BattleUI = this;
        battleSelector.BattleUI = this;
    }

    public async void BackToQuestScene()
    {
        var tokenSource = new CancellationTokenSource();
        battleManager.GameInfo.loadingSceneWithFade = true;

        var alphaUpTo = 1;
        fader.Fade(alphaUpTo);
        await UniTask.WaitUntil(() => fader.Available(), cancellationToken: tokenSource.Token);

        var playerInfo = battleManager.PlayerAction.PlayerInfo;

        if(battleManager.BattleInfo.isQuestFail)
        {
            playerInfo.currentScene = playerInfo.savedLocationScene;
        }
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(playerInfo.currentScene);
        await UniTask.WaitUntil(() => asyncLoad.isDone, cancellationToken: tokenSource.Token);
    }
}
