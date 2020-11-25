using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePreparation : MonoBehaviour
{
    [SerializeField] BattleManager battleManager = default;
    [SerializeField] BattleMain battleMain = default;

    MessageBox messageBox;

    STATE state;
    enum STATE
    {
        LOCATE_MONSTER,
        MESSAGE_BOX_OPEN,
        PLAY_PAUSE,
        DONE
    }

    void Start()
    {
        messageBox = battleManager.BattleCanvas.MessageBox;
    }

    void Update()
    {
        if (!messageBox.MessageAcceptable() || !battleManager.AnimationNotPlaying(battleManager.Animator))
            return;

        switch (state)
        {
            case STATE.LOCATE_MONSTER:
                LocateMonsters();
                break;
            case STATE.MESSAGE_BOX_OPEN:
                DisplayMessage();
                break;
            case STATE.PLAY_PAUSE:
                battleManager.PlayPause();
                state = STATE.DONE;
                break;
            case STATE.DONE:
                battleMain.enabled = true;
                Destroy(this);
                break;
        }
    }

    void DisplayMessage()
    {
        if (messageBox.Open())
        {
            messageBox.DisplayMessage(battleManager.BattleMessage);
            state = STATE.PLAY_PAUSE;
        }
    }

    void LocateMonsters()
    {
        var bm = battleManager;
        int unitNum = bm.BattleInfo.monsters.Count - 1;

        bm.MonsterActions = new List<MonsterAction>(
                                        bm.MonsterUnit[unitNum].GetComponentsInChildren<MonsterAction>());

        bm.MonsterUnit[unitNum].SetActive(true);

        string str = "{0}があらわれた！\n";
        for (var i = 0; i <= unitNum; i++)
        {
            var monster = bm.BattleInfo.monsters[i];
            bm.MonsterActions[i].monster = monster;
            bm.MonsterActions[i].index = i;
            bm.MonsterActions[i].BattleManager = battleManager;
            battleManager.BattleMessage.AppendFormat(str, monster.monsterName);
        }
        state = STATE.MESSAGE_BOX_OPEN;

    }
}
