using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public PlayerInfo playerInfo;

    [SerializeField] Rigidbody2D rb2d = null;
    [SerializeField] Animator anim = null;

    Vector2 move;
    Vector2 lastMove;
    CharacterMove contactWith;
    QuestUIManager questUIManager;

    public CharacterMove ContactWith { get => contactWith; }
    public QuestUIManager QuestUIManager { get => questUIManager; set => questUIManager = value; }

    void initPlayerInfo()
    {
        playerInfo.freeze = false;
        playerInfo.dead = false;
    }

    void Awake()
    {
        if(!playerInfo.startBattle)
            initPlayerInfo();

        lastMove = Vector2.down;
    }

    void FixedUpdate()
    {
        if (playerInfo.freeze)
            return;

        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool talking = Input.GetKeyDown(KeyCode.Space);

        if (contactWith && talking)
        {
            StartConversation();
            return;
        }

        rb2d.MovePosition(rb2d.position + new Vector2(move.x, move.y).normalized * speed * Time.fixedDeltaTime);
    }

    void Update()
    {        
        float moveMagnitude = move.sqrMagnitude;
        anim.SetFloat("move_speed", moveMagnitude);

        if(0.5 < moveMagnitude)
        {
            anim.SetFloat("move_x", move.x);
            anim.SetFloat("move_y", move.y);
            lastMove = new Vector2(move.x, move.y);
        }
        else
        {
            anim.SetFloat("last_move_x", lastMove.x);
            anim.SetFloat("last_move_y", lastMove.y);
        }
    }

    void StopPlayer()
    {
        move = Vector2.zero;
        playerInfo.freeze = true;
    }

    void StartConversation()
    {
        ContactWith.FacingTo(lastMove);
        StopPlayer();
        questUIManager.InTalk = true;
        contactWith.freeze = true;
    }

    public void StopConversation()
    {
        questUIManager.InTalk = false;
        contactWith.freeze = false;
        playerInfo.freeze = false;
    }

    public void ResetPosition(Vector2 direction, Vector2 position)
    {
        move = Vector2.zero;
        transform.position = position;
        lastMove = direction;
    }

    public void TouchingToNpc(CharacterMove chrMove)
    {
        if(chrMove != null)
            contactWith = chrMove;
        else
            contactWith = null;
    }

    public void RunIntoMonster()
    {
        StopPlayer();

        playerInfo.lastPosition = transform.position;
        playerInfo.lastMove = lastMove;
        playerInfo.startBattle = true;
    }

    public bool IsPlayerMoving()
    {
        if (move == Vector2.zero)
            return false;        

        return true;
    }
}
