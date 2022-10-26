using UnityEngine.UIElements;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;


public class Shop : MonoBehaviour, ICustomEventListener
{
    [SerializeField] CustomEventTrigger shopTrigger;
    NpcData npcData;

    VisualElement shoptypeSelect;
    Button buyButton;
    Button sellButton;

    VisualElement shoppingPanel;
    public NpcData NpcData { get => npcData; set => npcData = value; }


    void OnEnable()
    {
        shopTrigger.AddEvent(this);
    }

    void OnDisable()
    {
        shopTrigger.RemoveEvent(this);
    }

    public void OnEventRaised()
    {
        ShopStart();
    }


    public void SetUp(VisualElement _rootUI)
    {
        shoptypeSelect = _rootUI.Q<VisualElement>("shop-type-screen");
        shoppingPanel  = _rootUI.Q<VisualElement>("shop-screen");

        buyButton = shoptypeSelect.Q<Button>("buy-button");
        sellButton = shoptypeSelect.Q<Button>("sell-button");

    
        buyButton.clicked += PlayerBuyShopping;
        sellButton.clicked += PlayerSelllShopping;

    }

    void PlayerBuyShopping()
    {

    }

    void PlayerSelllShopping()
    {

    }

    void ShopStart()
    {
        ShoptypeSelectOpen(true);
    }

    void ShoptypeSelectOpen(bool open)
    {
        shoptypeSelect.style.display = (open)? DisplayStyle.Flex : DisplayStyle.None;
    }

}
