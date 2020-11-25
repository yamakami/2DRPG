using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public PlayerInfo playerInfo;
    [SerializeField] string initialScene = "SampleScene";
    [SerializeField] string initialQuest = "FirstLand";
    [HideInInspector] public bool talking;
    [HideInInspector] public CharacterMove characterMove;

    Rigidbody2D rb2d;
    Animator anim;
    Vector2 move;
    Vector2 lastMove;
    bool npcTouching;

    void initPlayerInfo()
    {
        playerInfo.freeze = false;
        playerInfo.currentScene = initialScene;
        playerInfo.currentQuest = initialQuest;
    }

    void Start()
    {

        if(!playerInfo.startBattle)
            initPlayerInfo();

        playerInfo.startConversation = false;

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lastMove = Vector2.down;
    }

    void FixedUpdate()
    {
        if (playerInfo.freeze)
            return;

        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        talking = Input.GetKeyDown(KeyCode.Space);

        if (npcTouching && talking)
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

    void StartConversation()
    {
        characterMove.FacingTo(lastMove);
        playerInfo.startConversation = true;
        StopPlayer();
    }

    void StopPlayer()
    {
        move = Vector2.zero;
        playerInfo.freeze = true;
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
        {
            characterMove = chrMove;
            npcTouching = true;
        }
        else
        {
            characterMove = null;
            npcTouching = false;
        }
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
