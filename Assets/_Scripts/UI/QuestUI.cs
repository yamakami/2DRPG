using UnityEngine;
using UnityEngine.UIElements;

public class QuestUI : MonoBehaviour
{
    [SerializeField]  UIDocument uiDocument;
    [SerializeField]  Conversation conversation;
    [SerializeField]  MessageBox messageBox;
    [SerializeField]  Shop shop;

    public Conversation Conversation { get => conversation; }
    public MessageBox MessageBox { get => messageBox; }
    public Shop Shop { get => shop; }
    public UIDocument UiDocument { get => uiDocument; }
}
