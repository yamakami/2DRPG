using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;

public class FlowPrepare : FlowBase
{
    [SerializeField] GameObject[] monsterUnits;
    [SerializeField] FlowMain flowMain;

    async UniTaskVoid Start()
    {
        var cancelToken = cancellationTokenSource.Token;
        var battleManager = battleUI.BattleManager;
        var messageBox = battleUI.BattleMessageBox;

        PickUpMonster(battleManager);

        var fader = battleUI.Fader;
        fader.CutOff();
        await UniTask.WaitUntil(() => fader.Available(), cancellationToken: cancelToken);
        await UniTask.Delay(delaytime, cancellationToken: cancelToken);

        StartMessage(battleManager, messageBox);
        await UniTask.WaitUntil(() => messageBox.Available(), cancellationToken: cancelToken);
        await UniTask.Delay(delaytime, cancellationToken: cancelToken);

        enabled = false;
    }

    override protected void OnDisable()
    {
        flowMain.BattleUI = battleUI;
        flowMain.enabled = true;

        base.OnDisable();
    }

    void PickUpMonster(BattleManager battleManager)
    {
        var livingMonsterList = battleManager.BattleInfo.livingMonsterList;
        var monsterList = new List<Monster>(livingMonsterList.livingMonsters);
        var unitMaxNum = livingMonsterList.unitMaxNum;
        var unitNum = Random.Range(1, unitMaxNum + 1);
        var unitObj = monsterUnits[ unitNum - 1 ];
        var monsterActions = unitObj.GetComponentsInChildren<MonsterAction>();

        var isDuplicate = livingMonsterList.duplicate;
        for (var i = 0; i < unitNum; i++)
        {
            var index = Random.Range(0, monsterList.Count);
            monsterActions[i].Monster =  monsterList[index];
            monsterActions[i].characterName = monsterList[index].monsterName;

            if(!isDuplicate) monsterList.RemoveAt(index);
        }

        if(isDuplicate) RenameMonster(monsterActions);

        battleManager.MonsterActions = monsterActions.ToList();

        unitObj.SetActive(true);
    }

    void RenameMonster(MonsterAction[] monsterActions)
    {
        var uniqueMonsters = monsterActions.Distinct().ToArray();
        var monsterActionList = monsterActions.ToList();
        var postFix = new[]{"A", "B", "C"};

        foreach(var uni in uniqueMonsters)
        {
            var duplicateActions = monsterActionList.FindAll( m => m.Monster.monsterName == uni.Monster.monsterName);
            var actionCt = duplicateActions.Count;

            if(1 < actionCt)
            {
                for(var i = 0; i < actionCt; i++)
                {
                    duplicateActions[i].characterName =  duplicateActions[i].Monster.monsterName + postFix[i];
                }
            }
        } 
    }

    void StartMessage(BattleManager battleManager, BattleMessageBox messageBox)
    {
        var monsterActions = battleManager.MonsterActions;  
        var str = "{0}があらわれた！\n";

        for (var i = 0; i < monsterActions.Count; i++)
        {
            messageBox.StringBuilder.AppendFormat(str, monsterActions[i].characterName);
        }

        ActivateMessageBox(messageBox);
    }
}

