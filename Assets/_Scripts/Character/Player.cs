using UnityEngine;

public class Player : BaseCharacter
{
    [SerializeField] QuestManager questManager;
    // [SerializeField] PlayerInfo playerInfo;

    // public PlayerInfo PlayerInfo { get => GameManager.GetPlayerInfo(); }    
    NPC tallkingWithNpc;

    static Player _player;

    public NPC TallkingWithNpc { get => tallkingWithNpc; set => tallkingWithNpc = value; }

    void Start()
    {
        _player = this;
        lastMove = Vector2.down;
        // questManager =  QuestManager.GetQuestManager();
    }

    public override void CharaFixedUpdate()
    {
        if (Time.timeScale < 1 || Freeze) return;
        MovePosition();
    }

    public override void  CharaUpdate()
    { 
        base.CharaUpdate();

        if (Time.timeScale < 1 || Freeze) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (tallkingWithNpc != null)
                StartConversation(tallkingWithNpc);
        }
        else
        {
            move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }

    void StartConversation(NPC npc)
    {
        npc.FacingTo(npc.ConversationFacingDirection(transform));
        lastMove = ConversationFacingDirection(npc.transform);

        StopPlayer();
        questManager.UIQuest.StartConversation();
         // questManager.QuestUI.Conversation.StartConversation(npc.ConversationData());

    }

     public void StopPlayer()
    {
        move = Vector2.zero;
        Freeze = true;
    }
     public void EnableMove()
    {
        Freeze = false;
    }

    static public Player GetPlayer()
    {
        return _player;
    }
}

