using UnityEngine;

public class Player : BaseCharacter
{ 
    QuestManager questManager;
    NPC talkToNpc;

    public PlayerData PlayerData { get => SystemManager.DataManager().PlayerData; }
    public NPC TalkToNpc { get => talkToNpc; set => talkToNpc = value; }

    void Start()
    {
        questManager = QuestManager.GetQuestManager();
        lastMove = Vector2.down;
    }

    public override void CharaFixedUpdate()
    {
        if (Freeze) return;
        MovePosition();
    }

    public override void  CharaUpdate()
    { 
        base.CharaUpdate();

        if (Time.timeScale < 1 || Freeze) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (talkToNpc != null)
                StartConversation(talkToNpc);
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

        StopMove();
        questManager.QuestUI.Conversation.StartConversation();
    }

     public void StopMove()
    {
        move = Vector2.zero;
        Freeze = true;
    }
     public void EnableMove()
    {
        Freeze = false;
    }
}

