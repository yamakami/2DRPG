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

    public EventTrigger EventTrigger { get => eventTrigger; }
}
