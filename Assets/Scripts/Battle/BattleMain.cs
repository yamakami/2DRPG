using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMain : MonoBehaviour
{
    [SerializeField] BattleManager battleManager = default;
    [SerializeField] BattleEnd battleEnd = default;

    BattleCanvas battleCanvas;
    MessageBox messageBox;
    string pauseName;

    STATE state;
    STATE nextState;
    public enum STATE
    {
        DECIDE_TURN,
        ATTACK_SELECT,
        WAIT_FOR_PLAYER_INPUT,
        ATTACK_MESSAGE,
        CLOSE_MESSAGE_BOX,
        ATTACK_EFFECT,
        WAIT_FOR_ATTACK_EFFECT_DONE,
        ATTACK_RESULT_MESSAGE,
        TURNN_RESULT,
        BUTTLE_RESULT,
        PLAY_PAUSE,
        DONE
    }
    public STATE State { set => state = value; }

    public TURN turn;
    public enum TURN { PLAYER, MONSTER }

    void Start()
    {
        battleCanvas = battleManager.BattleCanvas;
        messageBox = battleCanvas.MessageBox;
    }

    void Update()
    {
        if (!messageBox.MessageAcceptable() || !battleManager.AnimationNotPlaying(battleManager.Animator))
            return;

        switch (state)
        {
            case STATE.DECIDE_TURN:
                DetermineTurn();
                break;
            case STATE.ATTACK_SELECT:
                AttackSelect(battleManager);
                break;
            case STATE.WAIT_FOR_PLAYER_INPUT:
                break;
            case STATE.ATTACK_MESSAGE:
                AttackMessage();
                break;
            case STATE.CLOSE_MESSAGE_BOX:
                CloseMessageBox();
                break;
            case STATE.ATTACK_EFFECT:
                AttackEffect();
                break;
            case STATE.WAIT_FOR_ATTACK_EFFECT_DONE:
                WaitForAttacEffectDone();
                break;
            case STATE.ATTACK_RESULT_MESSAGE:
                AttackResultMessage();
                break;
            case STATE.TURNN_RESULT:
                TurnResult(battleManager);
                break;
            case STATE.BUTTLE_RESULT:
                BattleResult(battleManager);
                break;
            case STATE.PLAY_PAUSE:
                battleManager.PlayPause(pauseName);
                state = nextState;
                break;
            case STATE.DONE:
                battleEnd.enabled = true;
                Destroy(this);
                break;
        }
    }

    void BattleResult(BattleManager bm)
    {
        if (bm.MonsterActions.Count <= 0)
        {
            battleEnd.State = BattleEnd.STATE.PLAYER_WIN;
            PlayPause(STATE.DONE);

            return;
        }

        PlayPause(STATE.DECIDE_TURN);
    }

    void TurnResult(BattleManager bm)
    {
        bm.Defender.hp -= bm.Damage;
        bm.Defender.StatusBar.SubtructValue(bm.Defender.hp);

        if (turn == TURN.MONSTER)
        {
            if (bm.Defender.hp <= 0)
            {
                battleEnd.State = BattleEnd.STATE.PLAYER_DEAD;
                state = STATE.DONE;
                return;
            }
        }
        else
        {
            if (bm.Defender.hp <= 0)
            {
                MonsterAction monster = (MonsterAction) bm.Defender;

                string str = "{0}を退治した！";
                bm.BattleMessage.AppendFormat(str, monster.characterName);

                bm.KeepReward(monster);

                RemoveFromAttackOrder(monster);

                bm.MonsterActions.RemoveAt(monster.index);

                Destroy(bm.Defender.gameObject);

                messageBox.DisplayMessage(bm.BattleMessage);
                PlayPause(STATE.BUTTLE_RESULT);

                return;
            }
        }

        PlayPause(STATE.DECIDE_TURN);
    }

    void AttackResultMessage()
    {
        messageBox.Open();
        messageBox.DisplayMessage(battleManager.ResultMessage);
        PlayPause(STATE.TURNN_RESULT);
    }

    void WaitForAttacEffectDone()
    {
        if (!battleManager.AnimationNotPlaying(battleManager.Defender.Animator))
            return;

        state = STATE.ATTACK_RESULT_MESSAGE;
    }

    void AttackEffect()
    {
        var bm = battleManager;

        Animator animator = bm.Defender.Animator;
        AudioSource audio = bm.Audio;
        Command command = bm.Command;

        animator.Play(command.commandName, 0, 0);
        audio.PlayOneShot(command.audioClip);

        state = STATE.WAIT_FOR_ATTACK_EFFECT_DONE;
    }

    void CloseMessageBox()
    {
        messageBox.Close();
        PlayPause(STATE.ATTACK_EFFECT);
    }

    void AttackMessage()
    {
        messageBox.DisplayMessage(battleManager.BattleMessage);
        PlayPause(STATE.CLOSE_MESSAGE_BOX, "pause_mid");
    }

    void AttackSelect(BattleManager bm)
    {
        if (turn == TURN.PLAYER)
        {
            bm.BattleCanvas.BattleBasicCommand.Open();
            State = STATE.WAIT_FOR_PLAYER_INPUT;
        }
        else
        {
            MonsterAction monster = (MonsterAction) bm.Attacker;
            monster.Attack((PlayerAction) bm.Defender);
            State = STATE.ATTACK_MESSAGE;
        }
    }

    void DetermineTurn()
    {
        var bm = battleManager;
        AttackOrder(bm);

        bm.Attacker = bm.AttackOrder[0];
        bm.AttackOrder.RemoveAt(0);

        if (bm.Attacker is PlayerAction)
        {
            turn = TURN.PLAYER;
        }
        else
        {
            turn = TURN.MONSTER;
            bm.Defender = bm.PlayerAction;
        }

        State = STATE.ATTACK_SELECT;
    }

    void RemoveFromAttackOrder(MonsterAction monster)
    {
        var bm = battleManager;
        for (var i = 0; i < bm.AttackOrder.Count; i++)
        {
            if (bm.AttackOrder[i].characterName == monster.characterName)
            {
                bm.AttackOrder.RemoveAt(i);
                break;
            }
        }
    }

    void AttackOrder(BattleManager bm)
    {
        if (0 < battleManager.AttackOrder.Count)
            return;

        bm.AttackOrder.Add(bm.PlayerAction);

        foreach (MonsterAction monsterAction in bm.MonsterActions)
        {
            bm.AttackOrder.Add(monsterAction);
        }

        bm.AttackOrder.Sort((v1, v2) => v2.speed.CompareTo(v1.speed));
    }

    void PlayPause(STATE nextState, string pauseName = "pause_short")
    {
        state = STATE.PLAY_PAUSE;
        this.pauseName = pauseName;
        this.nextState = nextState;
    }
}
