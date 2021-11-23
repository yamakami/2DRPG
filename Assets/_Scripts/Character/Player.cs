using UnityEngine;

public class Player : BaseCharacter
{
    QuestManager questManager;
    [SerializeField] NPC contactWith;

    ContactItem contactItem;
    ContactDoor contactDoor;

    public QuestManager QuestManager { get => questManager; set => questManager = value; }
    public NPC ContactWith { get => contactWith; }
    public ContactItem ContactItem { get => contactItem; set => contactItem = value; }
    public ContactDoor ContactDoor { get => contactDoor; set => contactDoor = value; }

    void Awake()
    {
        lastMove = Vector2.down;
        var gameInfo = questManager.GameInfo();
        if(gameInfo.playerFreeze) Freeze = true;
        if(gameInfo.levelUpTable.reCalculate) gameInfo.levelUpTable.Calculate();
    }

    void FixedUpdate()
    {
        if (Freeze) return;

        MovePosition();
    }

    protected　override void Update()
    {
        if (Freeze) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (contactItem) contactItem.SearchItem(); 
            if (contactWith) StartConversation();
        }
        else
        {
            move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        SetPlayerLastMove();
        base.Update();
    }

    void StartConversation()
    {
        contactWith.FacingTo(contactWith.ConversationFacingDirection(transform));
        lastMove = ConversationFacingDirection(contactWith.transform);

        StopPlayer();
        questManager.QuestUI.PlayerSatartConversation();
    }

    public void StopPlayer()
    {
        move = Vector2.zero;
        Freeze = true;
    }

    public void TouchingToNpc(NPC npc = null)
    {
        contactWith = npc;
    }

    public void ResetPosition(Vector2 facingTo, Vector2 position)
    {
        move = Vector2.zero;
        transform.position = position;
        lastMove = facingTo;
    }

    public bool IsPlayerMoving()
    {
        if (move == Vector2.zero)
            return false;

        return true;
    }
    
    public void EnableMove()
    {
        questManager.GameInfo().playerFreeze = false;
        Freeze = false;
    }

    void SetPlayerLastMove()
    {
        var playerInfo = questManager.PlayerInfo();

        playerInfo.playerLastPosition = transform.position;
        playerInfo.playerLastFacing = lastMove;
    }
}

