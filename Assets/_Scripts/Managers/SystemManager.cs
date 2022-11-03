using UnityEngine;
using System.Threading;

public class SystemManager : MonoBehaviour
{
    [SerializeField] DataManager dataManager;
    [SerializeField] SoundManager soundManager;

     CancellationTokenSource tokenSource;

    static DataManager _dataManager;
    static SoundManager _soundManager;
    static CancellationToken _unitaskToken;

    public void Awake()
    {
        _soundManager = soundManager;
        _dataManager = dataManager;
        tokenSource = new CancellationTokenSource();
        _unitaskToken = tokenSource.Token;
    }

    public static SoundManager SoundManager => _soundManager;

    public static DataManager DataManager() => _dataManager;

    public static CancellationToken UnitaskToken() => _unitaskToken;

    void TaskCancel()
    {
        if(tokenSource == null) return; 

        tokenSource?.Cancel();
        tokenSource?.Dispose();
        tokenSource = null;
    }

    void OnDisable() => TaskCancel();
}
