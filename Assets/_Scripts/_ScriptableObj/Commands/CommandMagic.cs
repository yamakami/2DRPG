using UnityEngine;

[CreateAssetMenu(fileName = "Magic", menuName = "Command/Item")]
public class CommandMagic : ScriptableObject, ICommand
{
    public string getName()
    {
        throw new System.NotImplementedException();
    }
}
