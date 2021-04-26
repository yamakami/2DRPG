using UnityEngine;
using UnityEngine.Playables;

public class BattleTimeline : MonoBehaviour
{
    [SerializeField] protected PlayableDirector playableDirector = default;
    [SerializeField] protected BattleTimeline nextTimeline = default;

    protected BattleManager battleManager;
    protected BattleCanvas battleCanvas;
    protected MessageText messageText;
    protected LevelUpTable.Level currentLevel;

    bool busy;

    public BattleManager BattleManager { set => battleManager = value; }

    protected virtual void Start()
    {
        battleCanvas = battleManager.BattleCanvas;
        messageText = battleCanvas.MessageText;
        battleManager.PlayableDirector = playableDirector;
        playableDirector.Play();
    }

    void Update()
    {
        ReleaseTimelineBusy();
    }

    public void PlayableStop()
    {
        busy = true;
        playableDirector.Pause();

    }

    void ReleaseTimelineBusy()
    {
        if (!busy)
            return;

        var anim = (battleManager.Defender) ? battleManager.Defender.Animator : null;
        if (!battleCanvas.MessageText.Available || !AnimationNotPlaying(anim) || !FaderIsStopped())
            return;

        busy = false;
        playableDirector.Resume();
    }

    bool FaderIsStopped()
    {
        return !battleCanvas.Fader.FadeAvairable;
    }

    bool AnimationNotPlaying(Animator anim = null)
    {
        if (anim == null)
            return true;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Base Layer.NotPlaying"))
            return true;

        return false;
    }

    public void ChangeTimeline()
    {
        nextTimeline.gameObject.SetActive(true);
        nextTimeline.BattleManager = battleManager;
        Destroy(gameObject);
    }
}

