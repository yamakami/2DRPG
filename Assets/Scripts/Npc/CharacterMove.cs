using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NpcTouching))]

public class CharacterMove : MonoBehaviour
{
    [SerializeField] float speed = 0;
    [SerializeField] int yMaxStep = 0;
    [SerializeField] int xMaxStep = 0;
    [SerializeField] float randomInterval = 3f;
    public ConversationData conversationData;

    [SerializeField] Rigidbody2D rb2d = null;
    [SerializeField] Animator anim = null;
    [SerializeField] NpcTouching npcTouching = null;
    [SerializeField] MovePoint shopInnMovePoint = default;

    public bool freeze;
    [HideInInspector] public Vector2 move;

    Vector2 lastMove;
    float currentTime;
    int xCurrentStep;
    int yCurrentStep;
    int moveDistance;
    int pixelUnit = 16;

    int randomRange = 4;

    const int MOVE_UP = 1;
    const int MOVE_DOWN = 2;
    const int MOVE_LEFT = 3;
    const int MOVE_RIGHT = 4;

    public MovePoint ShopInnMovePoint { get => shopInnMovePoint; }

    protected virtual void Start()
    {
        lastMove = Vector2.down;
    }

    void FixedUpdate()
    {
        if (freeze)
            return;

        currentTime += Time.fixedDeltaTime;

        var moveType = Random.Range(0, randomRange + 1);
        var yRandomSteps = Random.Range(1, yMaxStep + 1);
        var xRandomSteps = Random.Range(1, xMaxStep + 1);

        if (currentTime > randomInterval && moveDistance <= 0)
        {
            switch (moveType)
            {
                case MOVE_UP:
                    if (yMaxStep < (yCurrentStep + yRandomSteps))
                        return;

                    yCurrentStep += yRandomSteps;
                    moveDistance  = yRandomSteps * pixelUnit;
                    move = Vector2.up;
                    break;

                case MOVE_DOWN:
                    if ((yCurrentStep - yRandomSteps) < -yMaxStep)
                        return;

                    yCurrentStep -= yRandomSteps;
                    moveDistance  = yRandomSteps * pixelUnit;
                    move = Vector2.down;
                    break;

                case MOVE_LEFT:
                    if ((xCurrentStep - xRandomSteps) < -xMaxStep)
                        return;

                    xCurrentStep -= xRandomSteps;
                    moveDistance  = xRandomSteps * pixelUnit;
                    move = Vector2.left;
                    break;

                case MOVE_RIGHT:
                    if (xMaxStep < (xCurrentStep + xRandomSteps))
                        return;

                    xCurrentStep += xRandomSteps;
                    moveDistance  = xRandomSteps * pixelUnit;
                    move = Vector2.right;
                    break;

                default:
                    move = Vector2.zero;
                    return;
            }
            currentTime = 0f;
        }

        moveDistance--;
        if (moveDistance <= 0)
        {
            move = Vector2.zero;
            return;
        }

        if (npcTouching.otherNpcTouching)
        {
            move = move * -1;
        }

        rb2d.MovePosition(rb2d.position + new Vector2(move.x, move.y).normalized * speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        float moveMagnitude = move.sqrMagnitude;

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

    public void FacingTo(Vector2 playerLastMove)
    {
        move = Vector2.zero;
        lastMove = playerLastMove * -1;
        freeze = true;
    }
}
