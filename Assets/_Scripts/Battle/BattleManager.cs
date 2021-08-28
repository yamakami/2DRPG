using System.Collections.Generic;
using UnityEngine;

public class BattleReward
{
    public string monsterName;
    public Item item;
    public int exp;
    public int gold;
}

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameInfo gameInfo;
    [SerializeField] BattleInfo battleInfo;
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] PlayerAction playerAction;
    [SerializeField] List<MonsterAction> monsterActions;
    [SerializeField] AudioSource audioSource;

    [SerializeField] List<BattleReward> rewards = new List<BattleReward>();

    public GameInfo GameInfo { get => gameInfo; }
    public BattleInfo BattleInfo { get => battleInfo; }
    public PlayerInfo PlayerInfo { get => playerInfo; }
    public PlayerAction PlayerAction { get => playerAction; }
    public List<MonsterAction> MonsterActions { get => monsterActions; set => monsterActions = value; }
    public AudioSource AudioSource { get => audioSource; }
    public List<BattleReward> Rewards { get => rewards; }

    public void KeepReward(MonsterAction monsterAction)
    {
        var reward = new BattleReward();
        reward.monsterName = monsterAction.characterName;
        reward.item = monsterAction.Monster.DropItem();
        reward.exp = monsterAction.exp;
        reward.gold = monsterAction.gold;

        Rewards.Add(reward);
    }
}
