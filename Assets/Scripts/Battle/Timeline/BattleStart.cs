using UnityEngine;
using System.Collections.Generic;

public class BattleStart : BattleTimeline
{
    protected override void Start()
    {
        base.Start();
        LocateMonsters();
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
            bm.MonsterActions[i].monsterIndex = i;
            bm.MonsterActions[i].BattleManager = bm;
            bm.BattleMessage.AppendFormat(str, monster.monsterName);
        }
    }

    public void DisplayMessageBox()
    {
        PlayableStop();
        var text = battleCanvas.MessageText;
        text.Activate();
        text.DisplayBattleMessage(battleManager.BattleMessage);
    }
}

