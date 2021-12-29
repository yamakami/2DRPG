using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Command/Item")]
public class CommandItem : ScriptableObject, ICommand
{
    public string getName()
    {
        throw new System.NotImplementedException();
    }
}
