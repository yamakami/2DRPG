using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;

    static GameManager gameManager;

    void Awake()
    {
        gameManager = this;
    }

    public static PlayerInfo GetPlayerInfo()
    {
        return gameManager.playerInfo;
    }
}
