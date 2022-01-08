public interface ICommand
{
    public string getName();

    public void Consume();

    public string ActionMessage();
     public string AffectMessage();
}
