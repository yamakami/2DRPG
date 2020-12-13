using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.Text;


public class BattleReward
{
    public string monsterName;
    public Item item;
    public int exp;
    public int gold;
}

public class BattleManager : MonoBehaviour
{    
    [SerializeField] BattleInfo battleInfo = default;
    [SerializeField] PlayerAction playerAction = default;
    [SerializeField] BattleCanvas battleCanvas = default;
    [SerializeField] BattleStart battleStart = default;
    [SerializeField] GameObject[] monsterUnit = default;
    [SerializeField] AudioSource bmAudio = default;
    [SerializeField] LevelUpTable levelUpTable = default;

    List<MonsterAction> monsterActions;
    List<CharacterAction> attackOrder;
    CharacterAction attacker;
    CharacterAction defender;
    int damage;
    Command command;
    PlayableDirector playableDirector;
    StringBuilder battleMessage;
    StringBuilder resultMessage;
    public bool busy;
    List<BattleReward> rewards = new List<BattleReward>();

    public BattleInfo BattleInfo { get => battleInfo; }
    public PlayerAction PlayerAction { get => playerAction; }
    public BattleCanvas BattleCanvas { get => battleCanvas; }
    public PlayableDirector PlayableDirector { get => playableDirector; set => playableDirector = value; }
    public BattleStart BattleStart { get => battleStart; }
    public GameObject[] MonsterUnit { get => monsterUnit; }
    public List<MonsterAction> MonsterActions { get => monsterActions; set => monsterActions = value; }
    public List<CharacterAction> AttackOrder { get => attackOrder; set => attackOrder = value; }
    public CharacterAction Attacker { get => attacker; set => attacker = value; }
    public CharacterAction Defender { get => defender; set => defender = value; }
    public int Damage { get => damage; set => damage = value; }
    public StringBuilder BattleMessage { get => battleMessage; set => battleMessage = value; }
    public StringBuilder ResultMessage { get => resultMessage; set => resultMessage = value; }
    public Command Command { get => command; set => command = value; }
    public AudioSource Audio { get => bmAudio; }
    public List<BattleReward> Rewards { get => rewards; }
    public LevelUpTable LevelUpTable { get => levelUpTable; }

    void Awake()
    {
        BattleCanvas.BattleManager = this;
        BattleStart.BattleManager  = this;

        PlayerAction.BattleManager = this;

        AttackOrder = new List<CharacterAction>();
        BattleMessage = new StringBuilder();
        ResultMessage = new StringBuilder();

        LevelUpTable.Calculate();
    }

    void Update()
    {
        ReleaseBusy();
    }

    public void PlayableStop()
    {
        PlayableDirector.Pause();
        busy = true;
    }

    void ReleaseBusy()
    {
        if (!busy)
            return;

        var anim = (defender) ? defender.Animator : null;
        if (!battleCanvas.MessageBox.MessageAcceptable() || !AnimationNotPlaying(anim) || !FaderIsStopped())
            return;

        busy = false;
        PlayableDirector.Resume();
    }

    bool FaderIsStopped()
    {
        return !battleCanvas.FaderBlack.Fading;
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


    public void KeepReward(MonsterAction monsterAction)
    {
        BattleReward reward = new BattleReward();
        reward.monsterName = monsterAction.characterName;
        reward.item = monsterAction.monster.DropItem();
        reward.exp = monsterAction.exp;
        reward.gold = monsterAction.gold;

        Rewards.Add(reward);
    }

    public void ReflectRwardToPlayer()
    {
        int exp = 0;
        int gold = 0;
        string str = "";
        foreach (var r in rewards)
        {
            exp += r.exp;
            gold += r.gold;

            if (r.item != null)
            {
                str = "{0}は{1}を持っていた。\n";
                battleMessage.AppendFormat(str, r.monsterName, r.item.nameKana);
            }
        }

        playerAction.exp += exp;
        playerAction.gold += gold;

        str = "{0}は経験値{1}EXと{2}ゴールドを得た。";
        battleMessage.AppendFormat(str, playerAction.characterName, exp, gold);
    }

    public bool IsLevelUp()
    {
        LevelUpTable.Level currentLevel = LevelUpTable.levels[PlayerAction.lv];
        if (currentLevel.goalExp <= playerAction.exp)
            return true;

        return false;
    }
}
