using UnityEngine;
using System.Threading;

public class QuestManager : MonoBehaviour
{
    [SerializeField] GameSetting gameSetting;
    [SerializeField] PlayData playData;
    [SerializeField] BattleInfo battleInfo;
    [SerializeField] Player player;
    [SerializeField] QuestUI questUI;
    [SerializeField] LocationManager locationManager;

    public Player Player { get => player; }
    public QuestUI QuestUI { get => questUI; }

    static QuestManager questManager;
    static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    public static QuestManager GetQuestManager() { return questManager; }
    public static CancellationTokenSource CancellationTokenSource { get => cancellationTokenSource; }
    public LocationManager LocationManager { get => locationManager; }

    void Awake()
    {

        questManager = this;
    }

    void OnDisable()
    {
        cancellationTokenSource.Cancel();
    }
}
