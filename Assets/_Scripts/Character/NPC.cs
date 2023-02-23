using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class NPC : BaseCharacter
{
    [SerializeField] NpcData npcData;
    [SerializeField] NpcContact npcContact;

    public NpcData NpcData { get => npcData; }
    int steps;

    void Start()
    {
        if(!npcData) throw new ArgumentNullException ("npc data is emty!");
        if(!npcContact) throw new ArgumentNullException ("npc contact is emty!");

        lastMove = Vector2.down;
    }

    void OnBecameVisible()
    {
        npcData.InCamera = true;
        enabled = true;
        anim.enabled = true;
    }

    void OnBecameInvisible()
    {
        npcData.InCamera = false;
        enabled = false;
        anim.enabled = false;
    }

    public override void CharaFixedUpdate()
    {
        if (!npcData.InCamera) return;

        if (Freeze)
            return;

        if (npcData.MoveDistance <= 0)
        {
            move = Vector2.zero;
            return;
        }

        MovePosition();
        npcData.MoveDistance--;
    }

    public override void CharaUpdate()
    { 
        if (!npcData.InCamera) return;

        base.CharaUpdate();

        if (Freeze)
            return;

        npcData.CurrentTime += Time.deltaTime;

        var moveType = UnityEngine.Random.Range(0, npcData.RandomRange + 1);
        var yRandomSteps = UnityEngine.Random.Range(1, npcData.YMaxStep + 1);
        var xRandomSteps = UnityEngine.Random.Range(1, npcData.XMaxStep + 1);

        if (npcData.CurrentTime > npcData.RandomInterval && npcData.MoveDistance <= 0)
        {
            switch (moveType)
            {
                case NpcData.MOVE_UP:
                    if (npcData.YMaxStep < (npcData.YCurrentStep + yRandomSteps))
                        return;

                    npcData.YCurrentStep += yRandomSteps;
                   npcData.MoveDistance = yRandomSteps * npcData.PixelUnit;
                    move = Vector2.up;
                    break;

                case NpcData.MOVE_DOWN:
                    if ((npcData.YCurrentStep - yRandomSteps) < -npcData.YMaxStep)
                        return;

                    npcData.YCurrentStep -= yRandomSteps;
                    npcData.MoveDistance = yRandomSteps * npcData.PixelUnit;
                    move = Vector2.down;
                    break;

                case NpcData.MOVE_LEFT:
                    if ((npcData.XCurrentStep - xRandomSteps) < -npcData.XMaxStep)
                        return;

                    npcData.XCurrentStep -= xRandomSteps;
                    npcData.MoveDistance = xRandomSteps * npcData.PixelUnit;
                    move = Vector2.left;
                    break;

                case NpcData.MOVE_RIGHT:
                    if (npcData.XMaxStep < (npcData.XCurrentStep + xRandomSteps))
                        return;

                    npcData.XCurrentStep += xRandomSteps;
                    npcData.MoveDistance = xRandomSteps * npcData.PixelUnit;
                    move = Vector2.right;
                    break;

                default:
                    move = Vector2.zero;
                    return;
            }
            npcData.CurrentTime = 0f;
        }

        if (npcContact.OtherNpcTouching)
        {
            move *= -1;
        }
    }

    public void FacingTo(Vector2 direction)
    {
         lastMove = direction;
    }

    public void StartMove()
    {
        Freeze = false;
    }

    public void Stop()
    {
        move = Vector2.zero;
        Freeze = true;
    }
}
