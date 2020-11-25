using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] BattleCanvas battleCanvas = default;
    [SerializeField] BattleMain battleMain = default;
    [SerializeField] PlayerAction playerAction = default;
    [SerializeField] GameObject[] monsterUnit = default;
    [SerializeField] Animator bmAnimator = default;
    [SerializeField] AudioSource bmAudio = default;
    [SerializeField] LevelUpTable levelUpTable = default;

    [HideInInspector] List<MonsterAction> monsterActions;
    [HideInInspector] List<CharacterAction> attackOrder;
    [HideInInspector] CharacterAction attacker;
    [HideInInspector] CharacterAction defender;
    [HideInInspector] int damage;
    [HideInInspector] Command command;
    [HideInInspector] List<BattleReward> rewards = new List<BattleReward>();

    StringBuilder battleMessage;
    StringBuilder resultMessage;

    public BattleInfo BattleInfo { get => battleInfo; }
    public BattleCanvas BattleCanvas { get => battleCanvas; }
    public BattleMain BattleMain { get => battleMain; }
    public PlayerAction PlayerAction { get => playerAction; }
    public GameObject[] MonsterUnit { get => monsterUnit; }
    public List<MonsterAction> MonsterActions { get => monsterActions; set => monsterActions = value; }
    public List<CharacterAction> AttackOrder { get => attackOrder; set => attackOrder = value; }
    public CharacterAction Attacker { get => attacker; set => attacker = value; }
    public CharacterAction Defender { get => defender; set => defender = value; }
    public int Damage { get => damage; set => damage = value; }
    public Command Command { get => command; set => command = value; }
    public StringBuilder BattleMessage { get => battleMessage; set => battleMessage = value; }
    public StringBuilder ResultMessage { get => resultMessage; set => resultMessage = value; }
    public Animator Animator { get => bmAnimator; }
    public AudioSource Audio { get => bmAudio; }
    public List<BattleReward> Rewards { get => rewards; }

    void Awake()
    {
        BattleCanvas.BattleManager = this;
        PlayerAction.BattleManager = this;

        AttackOrder = new List<CharacterAction>();
        BattleMessage = new StringBuilder();
        ResultMessage = new StringBuilder();

        //levelUpTable.Calculate();
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

        playerAction.exp = exp;
        playerAction.gold = gold;

        str = "{0}は経験値{1}EXと{2}ゴールドを得た。";
        battleMessage.AppendFormat(str, playerAction.characterName, exp, gold);
    }

    public void PlayPause(string stateName = null)
    {
        if (stateName == null)
            stateName = "pause_short";

        Animator.Play(stateName);
    }

    public bool AnimationNotPlaying(Animator anim)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Base Layer.NotPlaying"))
            return true;

        return false;
    }
}
