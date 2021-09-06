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

        var currentScene = battleManager.PlayerAction.PlayerInfo.currentScene;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentScene);
        await UniTask.WaitUntil(() => asyncLoad.isDone, cancellationToken: tokenSource.Token);
    }
}
