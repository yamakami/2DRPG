using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveActors : MonoBehaviour
{
    [SerializeField] BaseCharacter[] characters;

    public BaseCharacter[] Characters { get => characters; }

    void Start()
    {
        QuestManager.GetQuestManager().Actors = this;
    }
}
