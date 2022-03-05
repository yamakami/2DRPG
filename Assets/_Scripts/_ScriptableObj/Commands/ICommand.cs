using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public interface ICommand
{
    public string GetName();
    public string GetNameKana();
    public string GetDescription();
    public AudioClip GetAudioClip();
    public int AvailableAmount();
    public string ActionMessage();
    public string AffectMessage(bool result = true);
    public int AffectValue { get; }
    public void Consume(IStatus userStatus, IStatus targetStatus = null);
}
