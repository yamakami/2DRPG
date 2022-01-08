using UnityEngine;

[CreateAssetMenu(fileName = "_Magic", menuName = "Command/Magic")]
public class CommandMagic : ScriptableObject, ICommand
{
    public string getName()
    {
        throw new System.NotImplementedException();
    }
    public string ActionMessage()
    {
        throw new System.NotImplementedException();
    }

    public string AffectMessage()
    {
        throw new System.NotImplementedException();
    }

    public void Consume()
    {
        throw new System.NotImplementedException();
    }
}
