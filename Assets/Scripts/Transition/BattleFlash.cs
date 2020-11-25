using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFlash : MonoBehaviour
{
    bool playing = true;

    public bool Playing { get => playing; }

    public void FlashEnd()
    {
        playing = false;
    }
}
