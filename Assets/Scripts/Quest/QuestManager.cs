using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove = default;
    [SerializeField] BattleFlash battleFlash = default;
    [SerializeField] FaderBlack faderBlack = default;
    [SerializeField] Quest quest = default;
    [SerializeField] BattleInfo battleInfo = default;

    float encounteringRestTimer = 0f;
    float encounteringRestInterval = 2f;
    Quest.QuestLocation location;

    void Start()
    {
        if (playerMove.playerInfo.startBattle)
            StartCoroutine(RestoreFromBattle());

        ChangeQuest(playerMove.playerInfo.currentQuest);
    }

    IEnumerator RestoreFromBattle()
    {
        faderBlack.SkipFadeOut = true;
        faderBlack.Alpha = 1;
        faderBlack.gameObject.SetActive(true);
        playerMove.ResetPosition(playerMove.playerInfo.lastMove, playerMove.playerInfo.lastPosition);
        playerMove.playerInfo.startBattle = false;

        yield return new WaitForSeconds(1f);

        faderBlack.FaderIn();

        Invoke("ReleasePlayerFreeze", 1f);
    }

    void ReleasePlayerFreeze()
    {
        playerMove.playerInfo.freeze = false;
    }

    void Update()
    {
        MonsterEncounteringCalc();
    }

    public void ChangeQuest(string questName)
    {
        playerMove.playerInfo.currentQuest = questName;
        battleInfo.questName = questName;
        battleInfo.areas = null;

        location = quest.questLocations.Find(l => (l.questName == questName));
        if (location.areas != null)
        {
            battleInfo.areaIndex = 0;
            battleInfo.areas = location.areas;
        }
    }

    public void ChangeQuestArea(int areaIndex)
    {
        battleInfo.areaIndex = areaIndex;
    }

    void MonsterEncounteringCalc()
    {
        if (location.areas == null)
            return;

        if (playerMove.playerInfo.startBattle)
            return;

        if (!playerMove.IsPlayerMoving())
            return;

        if (encounteringRestInterval < encounteringRestTimer)
        {
            encounteringRestTimer = 0f;

            var index = battleInfo.areaIndex;
            var area = battleInfo.areas[index];
            var density = area.monsterDensity;
            var encountered = Random.Range(0, 101);

            if (encountered < density)
            {
                playerMove.RunIntoMonster();
                PickupMonster(area);
                LoadBattleScene();
                return;
            }
        }
        encounteringRestTimer += Time.deltaTime;
    }

    public void LoadBattleScene()
    {
        // debug button用 
        //var index = battleInfo.areaIndex;
        //var area = battleInfo.areas[index];
        //playerMove.RunIntoMonster();
        //PickupMonster(area);
        // debug button用 

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        while (battleFlash.Playing)
            yield return null;

        SceneManager.LoadSceneAsync("Battle");
    }

    void PickupMonster(Quest.Area area)
    {
        var unitNum = Random.Range(1, area.maxUnit + 1);
        List<Monster> monstersList = new List<Monster>(area.monsters);
        battleInfo.monsters.Clear();

        for (var i = 1; i <= unitNum; i++)
        {
            var index = Random.Range(0, monstersList.Count);
            battleInfo.monsters.Add(monstersList[index]);
            monstersList.RemoveAt(index);
        }
        monstersList = null;
    }
}
