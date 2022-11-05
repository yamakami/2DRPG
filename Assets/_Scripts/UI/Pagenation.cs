using UnityEngine;
using UnityEngine.UIElements;

public class Pagenation
{
    int rowsPerPage;
    int lastPageNumber;
    public int currentPage = 1;
    public int itemIndex;

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

        pageFirst = rootElement.Q<Button>("page-first");
        pagePrev  = rootElement.Q<Button>("page-prev");
        pageNext  = rootElement.Q<Button>("page-next");
        pageLast  = rootElement.Q<Button>("page-last");

        pageFirst.clicked += PageFirst;
        pageFirst.RegisterCallback<MouseEnterEvent>( ev => iselectButton.HoverSound());

        pagePrev.clicked += PagePrev;
        pagePrev.RegisterCallback<MouseEnterEvent>( ev => iselectButton.HoverSound());

        pageNext.clicked += PageNext;
        pageNext.RegisterCallback<MouseEnterEvent>( ev => iselectButton.HoverSound());

        pageLast.clicked += PageLast;
        pageLast.RegisterCallback<MouseEnterEvent>( ev => iselectButton.HoverSound());
    }

    public void SetMaxPageNumber(int maxItemCount) => lastPageNumber = Mathf.CeilToInt(maxItemCount / (float)rowsPerPage);

    public void RestCurrentPage() => currentPage = 1;

    void PageFirst()
    {
        iselectButton.ClickSound();
        currentPage = 1;
        iselectButton.BindButtonEvent();
    }

    void PagePrev()
    {
        iselectButton.ClickSound();
        if(currentPage == 1) return;
        currentPage--;
        iselectButton.BindButtonEvent();
    }

    void PageNext()
    {
        iselectButton.ClickSound();
        if(currentPage == lastPageNumber) return;
        currentPage++;
        iselectButton.BindButtonEvent();
    }

    void PageLast()
    {
        iselectButton.ClickSound();
        currentPage = lastPageNumber;
        iselectButton.BindButtonEvent();
    }
}
