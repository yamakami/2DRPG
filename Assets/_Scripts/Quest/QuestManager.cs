using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    [SerializeField] GameInfo gameInfo;
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] BattleInfo battleInfo;
    [SerializeField] Player player;
    [SerializeField] QuestUI questUI;
    [SerializeField] Quest quest;
    [SerializeField] float monsterEncounterInterval = 1f;

    public Player Player { get => player; }
    public QuestUI QuestUI { get => questUI; }
    public Quest Quest { get => quest; }
    float monsterEncounterTimer = 0f;

    void Awake()
    {
        questUI.QuestManager = this;
        player.QuestManager  = this;
        InitializePlayerInfo();
    }

    void InitializePlayerInfo()
    {
        var currentLocation = quest.locations[playerInfo.currentQuestLocationIndex];
        if(currentLocation.noBattle) battleInfo.livingMonsterList = null;
    }

    void Update()
    {
        MonsterEncounteringCalc();
    }

    void MonsterEncounteringCalc()
    {
        var battleInfo = BattleInfo();

        if (battleInfo.livingMonsterList == null)
            return;

        if (!player.IsPlayerMoving())
            return;

        if (monsterEncounterInterval < monsterEncounterTimer)
        {
            monsterEncounterTimer = 0f;

            var livingMonsterList = battleInfo.livingMonsterList;
            var density = livingMonsterList.monsterDensity;
            var encountered = Random.Range(0, 101);

           if (encountered < density)
           {
                BattleStart(battleInfo);
                return;
           }
        }
        monsterEncounterTimer += Time.deltaTime;
    }

    void BattleStart(BattleInfo battleInfo)
    {
        questUI.LocationManager.MoveToBattleScene();
    }

    public void SetCurrentQuest(string sceneName, int locationIndex, int areaIndex)
    {
        playerInfo.currentScene = sceneName;
        var location = quest.locations[locationIndex];
        playerInfo.currentQuestLocationIndex = locationIndex;
        playerInfo.currentMonsterAreaIndex = areaIndex;

        if(location.noBattle) battleInfo.livingMonsterList = null;

        SetLivingMonster(locationIndex, areaIndex);
    }

    void SetLivingMonster(int locationIndex, int areaIndex)
    {
        if (quest.locations[locationIndex].monsterArea.Length == 0) return;
        BattleInfo().livingMonsterList = quest.locations[locationIndex].monsterArea[areaIndex];
    }

    public Quest.Location FindTargetLocation(int locationIndex)
    {
        var targetLocation = new Quest.Location();
        var locations = quest.locations;

        for (var i=0; i < quest.locations.Length; i++)
        {
            if (i == locationIndex)
            {
                targetLocation = locations[i];
                targetLocation.questLocation.gameObject.SetActive(true);
            }
            else
            {
                locations[i].questLocation.gameObject.SetActive(false);
            }
        }
        return targetLocation;
    }

    public PlayerInfo PlayerInfo()
    {
        return playerInfo;
    }

    public BattleInfo BattleInfo()
    {
        return battleInfo;
    }

    public GameInfo GameInfo()
    {
        return gameInfo;
    }
}
