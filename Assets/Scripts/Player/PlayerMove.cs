using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public PlayerInfo playerInfo;
    [HideInInspector] public bool talking;
    [HideInInspector] public CharacterMove characterMove;

    [SerializeField] Rigidbody2D rb2d = null;
    [SerializeField] Animator anim = null;

    Vector2 move;
    Vector2 lastMove;
    bool npcTouching;

    void initPlayerInfo()
    {
        playerInfo.freeze = false;
        playerInfo.dead = false;
    }

    void Awake()
    {
        if(!playerInfo.startBattle)
            initPlayerInfo();

        playerInfo.startConversation = false;
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
