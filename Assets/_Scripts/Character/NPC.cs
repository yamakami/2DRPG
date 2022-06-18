using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class NPC : BaseCharacter
{
    // [SerializeField] NpcData npcData;
    // [SerializeField] NpcContact npcContact;
    int yMaxStep = 0;
    int xMaxStep = 0;
    float randomInterval = 3f;
    float currentTime;
    int xCurrentStep;
    int yCurrentStep;
    int moveDistance;
    int pixelUnit = 16;

    int randomRange = 4;
    bool inCamera;

    const int MOVE_UP = 1;
    const int MOVE_DOWN = 2;
    const int MOVE_LEFT = 3;
    const int MOVE_RIGHT = 4;

    void Start()
    {
        lastMove = Vector2.down;
        // xMaxStep = npcData.xMaxStep;
        // yMaxStep = npcData.yMaxStep;
        xMaxStep = 4;
        yMaxStep = 4;
        randomInterval = 3f;
    }

    void OnBecameVisible()
    {
        inCamera = true;
        enabled = true;
        anim.enabled = true;
    }

    void OnBecameInvisible()
    {
        inCamera = false;
        enabled = false;
        anim.enabled = false;
    }

    public override void CharaFixedUpdate()
    {
        if (!inCamera) return;

        if (Freeze)
            return;

        MovePosition();
    }

    public override void CharaUpdate()
    { 
        if (!inCamera) return;

        base.CharaUpdate();

        if (Freeze)
            return;

        currentTime += Time.deltaTime;

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
                    moveDistance = yRandomSteps * pixelUnit;
                    move = Vector2.up;
                    break;

                case MOVE_DOWN:
                    if ((yCurrentStep - yRandomSteps) < -yMaxStep)
                        return;

                    yCurrentStep -= yRandomSteps;
                    moveDistance = yRandomSteps * pixelUnit;
                    move = Vector2.down;
                    break;

                case MOVE_LEFT:
                    if ((xCurrentStep - xRandomSteps) < -xMaxStep)
                        return;

                    xCurrentStep -= xRandomSteps;
                    moveDistance = xRandomSteps * pixelUnit;
                    move = Vector2.left;
                    break;

                case MOVE_RIGHT:
                    if (xMaxStep < (xCurrentStep + xRandomSteps))
                        return;

                    xCurrentStep += xRandomSteps;
                    moveDistance = xRandomSteps * pixelUnit;
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

        // if (npcContact.OtherNpcTouching)
        // {
        //     move *= -1;
        // }
    }

    public void FacingTo(Vector2 direction)
    {
         lastMove = direction;
    }

    public void Stop()
    {
        move = Vector2.zero;
        Freeze = true;
    }

    // public ConversationData ConversationData()
    // {
    //     return npcData.conversationData;
    // }
}
