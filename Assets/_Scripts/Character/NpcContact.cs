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

            if(player.TallkingWithNpc != null) return;

            npc.Stop();
            npc.Rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

            player.TallkingWithNpc = npc;
            npc.FacingTo(npc.ConversationFacingDirection(player.transform));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            var player = Player.GetPlayer();

            npc.StartMove();
            npc.Rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            player.TallkingWithNpc = null;
        }
    }
}
