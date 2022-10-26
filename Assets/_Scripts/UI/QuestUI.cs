using UnityEngine;
using UnityEngine.UIElements;

public class QuestUI : MonoBehaviour
{
    [SerializeField]  UIDocument uiDocument;
    [SerializeField]  Conversation conversation;
    [SerializeField]  MessageBox messageBox;
    [SerializeField]  Shop shop;

    public UIDocument UiDocument { get => uiDocument; }
    public Conversation Conversation { get => conversation; }
    public MessageBox MessageBox { get => messageBox; }
    public Shop Shop { get => shop; }

    public void SetUP()
    {
        var rootUI = uiDocument.rootVisualElement;

        messageBox.SetUp(rootUI);
        shop.SetUp(rootUI);
    }
}
