using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Transform statusBar;

    float maxValue;

    public void SetInitialValues(float maxValue)
    {
        this.maxValue = maxValue;
    }

    public void SubtructValue(float currentValue)
    {
        float ratio = currentValue / maxValue * 10;

        statusBar.transform.localScale = new Vector2(
            Mathf.Clamp(ratio, 0, 10),
            statusBar.transform.localScale.y
        );
    }
}
