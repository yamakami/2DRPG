using UnityEngine;

public class Player : BaseCharacter
{
    [SerializeField] PlayerInfo playerInfo;

    QuestManager questManager;

    public PlayerInfo PlayerInfo { get => GameManager.GetPlayerInfo(); }

    void Awake()
    {
        lastMove = Vector2.down;
        questManager =  QuestManager.GetQuestManager();
    }

    void FixedUpdate()
    {
        if (Time.timeScale < 1 || Freeze) return;
        MovePosition();
    }

    protected override void Update()
    { 
        base.Update();

        if (Time.timeScale < 1 || Freeze) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (ContactObserver.NpcContact != null)
                StartConversation(ContactObserver.NpcContact.GetInterfaceParent());
        }
        else
        {
            move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }

     void StartConversation(NpcContact contact)
    {
        var npc = contact.Npc;

        npc.FacingTo(npc.ConversationFacingDirection(transform));
        lastMove = ConversationFacingDirection(npc.transform);

        StopPlayer();
        questManager.QuestUI.Conversation.StartConversation(npc.ConversationData());
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
}

