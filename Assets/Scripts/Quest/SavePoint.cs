using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo = default;
    [SerializeField] GameObject mainGrid = null;
    [SerializeField] GameObject savePointGrid = null;

    void Start()
    {
        if (!playerInfo.dead)
            return;

        if (mainGrid != null)
            mainGrid.SetActive(false);

        if (savePointGrid != null)
            savePointGrid.SetActive(true);
    }
}
