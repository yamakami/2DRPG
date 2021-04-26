using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] Rigidbody2D rb2d = null;
    [SerializeField] Animator anim = null;

    Vector2 move;
    Vector2 lastMove;

    void Awake()
    {

        lastMove = Vector2.down;
    }

    void FixedUpdate()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool talking = Input.GetKeyDown(KeyCode.Space);

        rb2d.MovePosition(rb2d.position + new Vector2(move.x, move.y).normalized * speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        var moveMagnitude = move.sqrMagnitude;
        anim.SetFloat("move_speed", moveMagnitude);

        if (0.5 < moveMagnitude)
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
}
