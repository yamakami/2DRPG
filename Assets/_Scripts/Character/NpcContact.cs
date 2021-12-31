using UnityEngine;

public class NpcContact : MonoBehaviour, IContactObject<NpcContact>
{
    [SerializeField] NPC npc;
    [SerializeField] Rigidbody2D npcRd2;

    Player player;
    bool otherNpcTouching;

    public bool OtherNpcTouching { get => otherNpcTouching; }
    public NPC Npc { get => npc; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = QuestManager.GetQuestManager().Player;

            npc.Stop();
            npcRd2.constraints = RigidbodyConstraints2D.FreezeAll;

            ContactObserver.NpcContact = this;
            npc.FacingTo(npc.ConversationFacingDirection(player.transform));
        }

        if (collision.CompareTag("NPC")) otherNpcTouching = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            npc.Freeze = false;
            npcRd2.constraints = RigidbodyConstraints2D.FreezeRotation;
            ContactObserver.NpcContact = null;
        }

        if (collision.CompareTag("NPC")) otherNpcTouching = false;
    }

    public NpcContact GetInterfaceParent()
    {
        return this;
    }
}
