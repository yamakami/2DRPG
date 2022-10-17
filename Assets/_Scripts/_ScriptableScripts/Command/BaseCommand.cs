using UnityEngine;

public class BaseCommand : ScriptableObject
{
    public AudioClip effectSound = null;
    public string commandName;
    public string nameKana;

    [TextArea(2, 5)]
    public string description;

    public string CommandName => commandName;
}
