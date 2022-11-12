using UnityEngine;
using UnityEngine.UIElements;

public class Pagenation
{
    int rowsPerPage;
    int lastPageNumber;
    public int currentPage = 1;
    public int itemIndex;

    Label pagerPosition;

    VisualElement pagerBlock;
    Button pageFirst;
    Button pagePrev;
    Button pageNext;
    Button pageLast;

    ISelectButton iselectButton;

    public int ItemIndex { get => (currentPage - 1) * rowsPerPage; set => itemIndex = value; }

    public Pagenation(int _rowsPerPage, VisualElement rootElement,ISelectButton _iselectButton)
    {
        rowsPerPage   = _rowsPerPage;
        iselectButton = _iselectButton;

        var shopScreen = rootElement.Q<VisualElement>("shop-screen");

        pagerPosition = shopScreen.Q<Label>("pager-position");

        pagerBlock = shopScreen.Q<VisualElement>("pager");

        pageFirst = shopScreen.Q<Button>("page-first");
        pagePrev  = shopScreen.Q<Button>("page-prev");
        pageNext  = shopScreen.Q<Button>("page-next");
        pageLast  = shopScreen.Q<Button>("page-last");

        pageFirst.clicked += PageFirst;
        pageFirst.RegisterCallback<MouseEnterEvent>( ev => iselectButton.HoverSound());

        pagePrev.clicked += PagePrev;
        pagePrev.RegisterCallback<MouseEnterEvent>( ev => iselectButton.HoverSound());

        pageNext.clicked += PageNext;
        pageNext.RegisterCallback<MouseEnterEvent>( ev => iselectButton.HoverSound());

        pageLast.clicked += PageLast;
        pageLast.RegisterCallback<MouseEnterEvent>( ev => iselectButton.HoverSound());

        Reset();
    }

    public void SetMaxPageNumber(int maxItemCount) => lastPageNumber = Mathf.CeilToInt(maxItemCount / (float)rowsPerPage);

    public void Reset()
    {
        currentPage = 1;
        ButtonEnabled(new Button[]{pageFirst, pagePrev}, false);
        ButtonEnabled(new Button[]{pageNext, pageLast}, true);
    }

    public void DisplayPagerBlockAndPositionText()
    {
        pagerPosition.text = $"{currentPage}/{lastPageNumber}";

        if(lastPageNumber == 1)
            pagerBlock.style.display = DisplayStyle.None;
        else
            pagerBlock.style.display = DisplayStyle.Flex;
    }

    void ButtonEnabled(Button[] buttons, bool enable)
    {
        foreach(var bt in buttons)
            bt.pickingMode = (enable)? PickingMode.Position: PickingMode.Ignore ;
    }

    void PageFirst()
    {
        iselectButton.ClickSound();
        currentPage = 1;
        iselectButton.BindButtonEvent();

        ButtonEnabled(new Button[]{pageFirst, pagePrev}, false);
        ButtonEnabled(new Button[]{pageNext, pageLast}, true);
    }

    void PagePrev()
    {
        iselectButton.ClickSound();
        if(currentPage == 1) return;
        currentPage--;
        iselectButton.BindButtonEvent();

        if(currentPage == 1) ButtonEnabled(new Button[]{pageFirst, pagePrev}, false);
        ButtonEnabled(new Button[]{pageNext, pageLast}, true);
    }

    void PageNext()
    {
        iselectButton.ClickSound();
        if(currentPage == lastPageNumber) return;
        currentPage++;
        iselectButton.BindButtonEvent();

        ButtonEnabled(new Button[]{pageFirst, pagePrev}, true);
        if(currentPage == lastPageNumber) ButtonEnabled(new Button[]{pageNext, pageLast}, false);
    }

    void PageLast()
    {
        iselectButton.ClickSound();
        currentPage = lastPageNumber;
        iselectButton.BindButtonEvent();

        ButtonEnabled(new Button[]{pageFirst, pagePrev}, true);
        ButtonEnabled(new Button[]{pageNext, pageLast}, false);
    }
}
