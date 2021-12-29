using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] GameSetting gameSetting;
    [SerializeField] PlayData playData;
    [SerializeField] BattleInfo battleInfo;
    [SerializeField] Player player;
    [SerializeField] QuestUI questUI;

    static QuestManager questManager;

    public Player Player { get => player; }
    public QuestUI QuestUI { get => questUI; }

    public static QuestManager GetQuestManager() { return questManager; }

    void Awake()
    {
        questManager = this;
    }
}
