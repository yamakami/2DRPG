using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Text text;
    public Text Text { get => text; }
    public Button Button { get => button; }

    [SerializeField] EventTrigger eventTrigger;

    Color32 ButtonActiveColor = new Color32(255, 255, 255, 255);
    Color32 ButtonDisableColor = new Color32(135, 135, 135, 255);

    public EventTrigger EventTrigger { get => eventTrigger; }

    public void EnableButton()
    {
        button.interactable = true;
        eventTrigger.enabled = true;
        text.color = ButtonActiveColor;
    }

    public void DisableButton()
    {
        button.interactable = false;
        eventTrigger.enabled = false;
        text.color = ButtonDisableColor;
    }
}
