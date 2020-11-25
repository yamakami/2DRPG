using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChange : MonoBehaviour
{
    [SerializeField] int areaIndex = default;
    [SerializeField] int backIndex = default;

    QuestManager questManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            questManager = transform.root.GetComponent<QuestManager>();
            questManager.ChangeQuestArea(areaIndex);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        questManager = transform.root.GetComponent<QuestManager>();
        questManager.ChangeQuestArea(backIndex);
    }
}
