using UnityEngine;
using System.Threading;

public class QuestManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] QuestUI questUI;

    public Player Player { get => player; }
    public QuestUI QuestUI { get => questUI; }
    ActiveActors actors;

    static QuestManager questManager;
    static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    public static QuestManager GetQuestManager() { return questManager; }
    public static CancellationTokenSource CancellationTokenSource { get => cancellationTokenSource; }
    public ActiveActors Actors { set => actors = value; }


    void Awake()
    {
        questManager = this;
    }

    void OnDisable()
    {
        cancellationTokenSource.Cancel();
    }

    void FixedUpdate()
    {
        foreach(var character in actors.Characters)
        {
            character.CharaFixedUpdate();
        }
    }

    void Update()
    {
         foreach(var character in actors.Characters)
        {
            character.CharaUpdate();
        }       
    }
}
