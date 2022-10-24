using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] QuestUI questUI;

    public Player Player { get => player; }
    public QuestUI QuestUI { get => questUI; }

    static NPC[] _npcActors;
    static QuestManager _questManager;

    public static NPC[] NpcActors { set => _npcActors = value; }

    public static QuestManager GetQuestManager()
    {
        return _questManager;
    }

    void Start()
    {
        _questManager = this;
        questUI.SetUP();
    }

    void Update()
    {
        player.CharaUpdate();
        MoveNpc();        
    }

    void FixedUpdate()
    {
        player.CharaFixedUpdate();
        MoveNpc(true);
    }

    void MoveNpc(bool fixedUpdate = false)
    {
        var npcActors = QuestManager._npcActors;

        if(npcActors == null) return;

        foreach(var npc in npcActors)
        {
            if(fixedUpdate)
                npc.CharaFixedUpdate();
            else
                npc.CharaUpdate();
        }
    }

    public void StopPlayer()
    {
        player.StopMove();
    }

     public void PlayerEnableMove()
    {
        player.EnableMove();
    }

    public void PlayerStartConversation()
    {
        QuestUI.Conversation.StartConversation();
    }
}
