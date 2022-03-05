using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;


[CreateAssetMenu(fileName = "_AttackItem", menuName = "Command/AttackItem")]

public class AttackItem : CommandItem
{
    [SerializeField] string affectMessage;

    public override string AffectMessage(bool result = true)
    {
        return affectMessage;
    }
}
