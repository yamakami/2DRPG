using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcContact : MonoBehaviour
{
    [SerializeField] NPC npc;

    bool otherNpcTouching;

    public bool OtherNpcTouching { get => otherNpcTouching; }
    public NPC Npc { get => npc; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
             var player = Player.GetPlayer();

            npc.Stop();
            npc.Rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

            // ContactObserver.NpcContact = this;
            npc.FacingTo(npc.ConversationFacingDirection(player.transform));
        }

        if (collision.CompareTag("NPC")) otherNpcTouching = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            npc.StartMove();
            npc.Rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            // ContactObserver.NpcContact = null;
        }

        if (collision.CompareTag("NPC")) otherNpcTouching = false;
    }

    public NpcContact GetInterfaceParent()
    {
        return this;
    }
}
