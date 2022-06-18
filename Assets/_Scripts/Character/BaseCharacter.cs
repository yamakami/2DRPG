using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class BaseCharacter : MonoBehaviour, Updatable
{
    [SerializeField] protected float speed = 0;
    [SerializeField] protected Rigidbody2D rb2d = null;
    [SerializeField] protected Animator anim = null;

    bool freeze;
    protected Vector2 move;
    protected Vector2 lastMove;
    public bool Freeze { get => freeze; set => freeze = value; }

    public void SubstituteUpdate()
    {
        CharaUpdate();
    }

    public void SubstituteFixedUpdate()
    {
        CharaFixedUpdate();
    }

    public virtual void CharaUpdate()
    {
        var moveMagnitude = move.sqrMagnitude;
        anim.SetFloat("move_speed", moveMagnitude);

        if(Mathf.Approximately(moveMagnitude, 0f))
        {
            anim.SetFloat("last_move_x", lastMove.x);
            anim.SetFloat("last_move_y", lastMove.y);
        }
        else
        {
            anim.SetFloat("move_x", move.x);
            anim.SetFloat("move_y", move.y);
            lastMove = new Vector2(move.x, move.y);
        }
    }

    public virtual void CharaFixedUpdate() {}

    protected void MovePosition()
    {
        rb2d.MovePosition(rb2d.position + move.normalized * speed * Time.fixedDeltaTime);
    }

    public Vector2 ConversationFacingDirection(Transform target)
    {
        var diff = target.transform.position - transform.position;
        diff.Normalize();
        var rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        if(rot_z < 0)  rot_z +=360;

        if(35f <= rot_z && rot_z <= 125)
        {
            return Vector2.up;
        }
        else if(125f < rot_z && rot_z < 215)
        {
            return Vector2.left;
        }
        else if(215f <= rot_z && rot_z <= 305f)
        {
            return Vector2.down;
        }
        return Vector2.right;
    }
}
