using UnityEngine;

public class NpcContact : MonoBehaviour
{
    [SerializeField] NPC npc;

    bool otherNpcTouching;

    public bool OtherNpcTouching { get => otherNpcTouching; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = QuestManager.GetQuestManager().Player;

            if(player.TalkToNpc != null) return;

            npc.Stop();
            npc.Rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

            player.TalkToNpc = npc;
            npc.FacingTo(npc.ConversationFacingDirection(player.transform));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            var player = QuestManager.GetQuestManager().Player;

            npc.StartMove();
            npc.Rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            player.TalkToNpc = null;
        }
    }
}
