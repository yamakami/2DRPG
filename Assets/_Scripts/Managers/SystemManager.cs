using UnityEngine;

public class SystemManager : MonoBehaviour
{
    [SerializeField] DataManager dataManager;
    [SerializeField] SoundManager soundManager;

    static DataManager _dataManager;
    static SoundManager _soundManager;

    public void Awake()
    {
        _soundManager = soundManager;
        _dataManager = dataManager;
    }

    public static SoundManager SoundManager()
    {
        return _soundManager;
    }

    public static DataManager DataManager()
    {
        return _dataManager;
    }
}
