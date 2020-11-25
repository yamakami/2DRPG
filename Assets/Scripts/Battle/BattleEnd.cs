using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleEnd : MonoBehaviour
{
    [SerializeField] BattleManager battleManager = default;
    [SerializeField] LevelUpManager levelUpManager = default;

    BattleCanvas battleCanvas;
    MessageBox messageBox;
    string pauseName;

    STATE state;
    STATE nextState;
    public enum STATE
    {
        PLAYER_WIN,
        PLAYER_DEAD,
        REWARD,
        FADE_OUT,
        UNLOAD_SCENE,
        PLAY_PAUSE,
        DONE,
    }
    public STATE State { set => state = value; }

    void Start()
    {
        battleCanvas = battleManager.BattleCanvas;
        messageBox   = battleManager.BattleCanvas.MessageBox;
    }

    void Update()
    {
        if (!messageBox.MessageAcceptable() || !battleManager.AnimationNotPlaying(battleManager.Animator))
            return;

        switch (state)
        {
            case STATE.PLAYER_WIN:
                PlayerWin();
                break;
            case STATE.PLAYER_DEAD:
                Debug.Log("player is dead");
                break;
            case STATE.REWARD:
                Reward();
                break;
            case STATE.FADE_OUT:
                FadeOut();
                break;
            case STATE.UNLOAD_SCENE:
                UnloadScene();
                break;
            case STATE.PLAY_PAUSE:
                battleManager.PlayPause(pauseName);
                state = nextState;
                break;
            case STATE.DONE:
                Destroy(this);
                break;
        }
    }

    void UnloadScene()
    {
        if (battleCanvas.SceneFadeOutFinished())
        {
            var bm = battleManager;
            SceneManager.LoadSceneAsync(bm.PlayerAction.playerInfo.currentScene);
            state = STATE.DONE;
        }
    }

    void FadeOut()
    {
        battleCanvas.SceneFadeOut();
        state = STATE.UNLOAD_SCENE;
    }

    void Reward()
    {
        var bm = battleManager;
            bm.ReflectRwardToPlayer();
            messageBox.DisplayMessage(bm.BattleMessage);
            PlayPause(STATE.FADE_OUT, "pause_long");
    }

    void PlayerWin()
    {
        var bm = battleManager;
        var playerName = bm.PlayerAction.characterName;
        var str = "{0}はモンスター討伐に成功した。";

        messageBox.DisplayMessage(bm.BattleMessage.AppendFormat(str, playerName));
        PlayPause(STATE.REWARD);
    }

    void PlayPause(STATE nextState, string pauseName = "pause_short")
    {
        state = STATE.PLAY_PAUSE;
        this.pauseName = pauseName;
        this.nextState = nextState;
    }
}
