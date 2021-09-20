using UnityEngine;

public class MonsterArea : MonoBehaviour
{
    [SerializeField] int questLocationIndex;
    [SerializeField] int monsterAreaIndex;

    Player player;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("Player")) return;

        if (!player) player = collision.gameObject.GetComponent<Player>();

        ChangeQuestArea(questLocationIndex, monsterAreaIndex);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("Player")) return;

        var defaultMonsterAreaIndex = 0;
        ChangeQuestArea(questLocationIndex, defaultMonsterAreaIndex);
    }

    void ChangeQuestArea(int questLocationIndex, int monsterAreaIndex)
    {
        var qm = player.QuestManager;
        var currentScene = qm.PlayerInfo().currentScene;

        qm.SetCurrentQuest(currentScene, questLocationIndex, monsterAreaIndex);
    }
}
