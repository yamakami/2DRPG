public interface ICommand
{
    string CommadName { get; }
    CommandType.type CommandType { get; }

    // void Excute();
}
