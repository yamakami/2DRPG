using UnityEngine;
using UnityEngine.UIElements;

public class ShopErrorMessage : MonoBehaviour
{
    VisualElement shopErrorScreen;
    Label messageBox;
    Button backButton;

    void SetUP()
    {
        var rootUI = QuestManager.GetQuestManager().QuestUI.UiDocument.rootVisualElement;

        shopErrorScreen = rootUI.Q<VisualElement>("shop-error-screen");
        messageBox = shopErrorScreen.Q<Label>("message-box");
        backButton = shopErrorScreen.Q<Button>("back-button");

        var soundManager = SystemManager.SoundManager;
        backButton.clicked += BackButtonClick;
        backButton.RegisterCallback<ClickEvent>(ev => soundManager.PlayButtonClick());
        backButton.RegisterCallback<MouseEnterEvent>(ev => soundManager.PlayButtonHover());
    }

    void BackButtonClick()
    {
        ShopErrorScreenOpen(false);
    }

    public void Open(string message)
    {
        if(shopErrorScreen == null) SetUP();

        ShopErrorScreenOpen(true);
        messageBox.text = message;
    }

    void ShopErrorScreenOpen(bool open) => shopErrorScreen.style.display = (open) ? DisplayStyle.Flex : DisplayStyle.None;
}
