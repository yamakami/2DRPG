public class BaseItemCommand : BaseCommand
{
    public enum ITEM_TYPE
    {
        HEAL,
        KEY,
    }

    public ITEM_TYPE itemType;
}
