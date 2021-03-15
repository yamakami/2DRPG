using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] Button itemButton;
    [SerializeField] Text buttonText;
    [SerializeField] Text leftText;
    [SerializeField] Dropdown dropdown;

    public Button ItemButton { get => itemButton; }
    public Text ButtonText { get => buttonText; }
    public Text LeftText { get => leftText; }
    public Dropdown Dropdown { get => dropdown; }
}
