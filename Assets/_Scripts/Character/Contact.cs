using UnityEngine;

public class Contact : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] NPC npc;
    [SerializeField] Rigidbody2D npcRd2;
    bool otherNpcTouching;

    public bool OtherNpcTouching { get => otherNpcTouching; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            npcRd2.constraints = RigidbodyConstraints2D.FreezeAll;
            player.TouchingToNpc(npc);
        }

        if (collision.CompareTag("NPC")) otherNpcTouching = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            npcRd2.constraints = RigidbodyConstraints2D.FreezeRotation;
            player.TouchingToNpc();
        }

        if (collision.CompareTag("NPC")) otherNpcTouching = false;
    }
}