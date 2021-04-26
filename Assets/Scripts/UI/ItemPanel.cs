using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] Button itemButton;
    [SerializeField] Text buttonText;
    [SerializeField] Text leftText;
    [SerializeField] Dropdown dropdown;
    [SerializeField] InputField amountInput;

    public Button ItemButton { get => itemButton; }
    public Text ButtonText { get => buttonText; }
    public Text LeftText { get => leftText; }
    public Dropdown Dropdown { get => dropdown; }
    public InputField AmountInput { get => amountInput; }

    public void InputLimit()
    {
        int parsedInt;
        if (int.TryParse(amountInput.text, out parsedInt))
        {
            var minVal = 1;
            var maxVal = 100;
            amountInput.text = Mathf.Clamp(parsedInt, minVal, maxVal).ToString();
            return;
        }

        var defaultIntValue = "1";
        amountInput.text = defaultIntValue;
    }
}
