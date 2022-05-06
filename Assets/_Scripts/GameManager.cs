using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static PlayerInfo playerInfo;

    public static PlayerInfo PlayerInfo { get => playerInfo; set => playerInfo = value; }
}
