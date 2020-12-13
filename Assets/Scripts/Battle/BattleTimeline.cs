using UnityEngine;
using UnityEngine.Playables;

public class BattleTimeline : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector = default;
    [SerializeField] protected BattleTimeline nextTimeline = default;

    protected BattleManager battleManager;
    protected BattleCanvas battleCanvas;
    protected MessageBox messageBox;
    protected LevelUpTable.Level currentLevel;

    public BattleManager BattleManager { set => battleManager = value; }

    protected virtual void Start()
    {
        battleCanvas = battleManager.BattleCanvas;
        messageBox = battleCanvas.MessageBox;
        battleManager.PlayableDirector = playableDirector;
        battleManager.PlayableDirector.Play();
    }

    public void ChangeTimeline()
    {
        nextTimeline.gameObject.SetActive(true);
        nextTimeline.BattleManager = battleManager;
        Destroy(gameObject);
    }
}

