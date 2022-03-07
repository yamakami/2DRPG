using UnityEngine;
using UnityEngine.UI;
public abstract class CommandPager : MonoBehaviour
{
    [SerializeField] protected int pageButtonNum;
    [SerializeField] Text currentPageText;
    [SerializeField] Text totalPageText;
    protected int pageNum = 1;
    protected int lastPage;

    abstract protected void CreateButton();

    protected int GetStartIndex()
    {
        return (pageNum - 1) * pageButtonNum;
    }

    protected void SetTotalPageNum(int totalCount)
    {
        lastPage = Mathf.CeilToInt( totalCount / (float) pageButtonNum);
    }

    protected void PageNumDisplay()
    {
        currentPageText.text = pageNum.ToString();
        totalPageText.text = lastPage.ToString();
    }

    public void PageTop()
    {
        pageNum = 1;
        CreateButton();
    }

    public void PageBack()
    {
        if(pageNum == 1) return;
        pageNum--;
        CreateButton();
    }

    public void PageNext()
    {
        if(lastPage == pageNum) return;

        pageNum++;
        CreateButton();
    }

    public void PageLast()
    {
        pageNum = lastPage;
        CreateButton();
    }
}
