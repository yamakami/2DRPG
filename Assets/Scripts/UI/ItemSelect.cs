using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemSelect : UIBase
{
    [SerializeField] GraphicRaycaster graphicRaycaster;
    [SerializeField] Text golds;
    [SerializeField] Text pageText;
    [SerializeField] Image pagePanel;
    [SerializeField] int perPage = 5;
    [SerializeField] GameObject itemPagePref;
    [SerializeField] ItemPanel itemPanelPref;
    [SerializeField] Button pageForwardButton;
    [SerializeField] Button pageBackButton;

    ShopManager shopManager;
    public ShopManager ShopManager { set => shopManager = value; }
    public GraphicRaycaster GraphicRaycaster { get => graphicRaycaster; set => graphicRaycaster = value; }
    public Text Golds { get => golds; }

    int maxPageNum;
    int currentPage = 1;
    float pageWidth;
    bool available;

    void OnEnable()
    {
        available = true;
        currentPage = 1;
        CreateMenu(shopManager.ShopItemList());
    }

    public void CreateMenu(List<Item> shopItems)
    {
        int itemIndex = 0;

        maxPageNum = getPages(shopItems);
        pageWidth = pagePanel.rectTransform.rect.width;
        float pageHeight = pagePanel.rectTransform.rect.height;

        pageText.text = PageDisplay();

        var stBuilder  = shopManager.QuestUIManager.StringBuilder;
        var playerInfo = shopManager.PlayerMove.playerInfo;

        stBuilder.Clear();
        Golds.text = stBuilder.AppendFormat("{0} G", playerInfo.status.gold).ToString();

        for (var i = 1; i <= maxPageNum; i++)
        {
            GameObject gameObject1 = Instantiate(itemPagePref);
            GameObject page = gameObject1;

            pagePanel.rectTransform.anchoredPosition = new Vector2(0, pageHeight * -1);
            page.transform.SetParent(pagePanel.transform);
            page.transform.localPosition = new Vector2((i - 1) * pageWidth, 0);

            for (var j = 1; j <= perPage; j++)
            {
                ItemPanel panel = Instantiate(itemPanelPref);
                Item item = shopItems[itemIndex];

                panel.ButtonText.text = item.nameKana;

                stBuilder.Clear();
                if(shopManager.ShopType == ShopManager.SHOP_TYPE.Buy)
                    panel.LeftText.text = stBuilder.AppendFormat("{0} G", item.price).ToString();
                else
                    panel.LeftText.text = stBuilder.AppendFormat("{0} 個", item.player_possession_count).ToString();


                panel.ItemButton.onClick.AddListener(() => OnClickItem(panel, item));

                panel.transform.SetParent(page.transform);

                itemIndex++;
                if (shopItems.Count - 1 < itemIndex) break;
            }
        }

        pageBackButton.gameObject.SetActive(false);
        if (maxPageNum == currentPage)
            pageForwardButton.gameObject.SetActive(false);
        else
            pageForwardButton.gameObject.SetActive(true);
    }

    void OnClickItem(ItemPanel itemPanel, Item item)
    {
        graphicRaycaster.enabled = false;

        //int dropdownIndex = itemPanel.Dropdown.value;
        //int amount = int.Parse(itemPanel.Dropdown.options[dropdownIndex].text);
        var amount = int.Parse(itemPanel.AmountInput.text);
        shopManager.ShopConfirm(item, amount);
    }

    string PageDisplay()
    {
        return currentPage + "/" + maxPageNum;
    }

    int getPages(List<Item> shopItems)
    {
        int pageNum = shopItems.Count / perPage;
        int extra = shopItems.Count % perPage;
        if (extra != 0) pageNum++;

        return pageNum;
    }

    public void PageForward()
    {
        if (!available) return;

        currentPage++;
        MovePagePanel(pageWidth * -1);
    }

    public void PageBack()
    {
        if (!available) return;

        currentPage--;
        MovePagePanel(pageWidth);
    }

    void MovePagePanel(float moveX)
    {
        available = false;

        pageText.text = PageDisplay();

        RectTransform trans = pagePanel.rectTransform;
        float posiX = trans.position.x;
        trans.DOMoveX(posiX + moveX, 1f)
            .SetEase(Ease.OutExpo)
            .OnComplete(() => MovePageAvalable())
            .Play();

        if (currentPage < maxPageNum)
            pageForwardButton.gameObject.SetActive(true);
        else
            pageForwardButton.gameObject.SetActive(false);

        if (1 < currentPage)
            pageBackButton.gameObject.SetActive(true);
        else
            pageBackButton.gameObject.SetActive(false);
    }

    void MovePageAvalable()
    {
        available = true;
    }

    void OnDisable()
    {
        graphicRaycaster.enabled = true;

        foreach (Transform child in pagePanel.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}