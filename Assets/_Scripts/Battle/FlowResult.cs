using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class FlowResult : FlowBase
{
    [SerializeField] AudioClip playerWin;
    [SerializeField] AudioClip playerDead;
    [SerializeField] AudioClip levelUpClip;
    [SerializeField] MasterData masterData;

    public void BattleFail(BattleMessageBox messageBox, PlayerAction player)
    {
        enabled = true;

        player.PlayerDamageCanvas.alpha = 0.45f;
        ResultFail(messageBox, player).Forget();
    }

    async UniTaskVoid ResultFail(BattleMessageBox messageBox, PlayerAction player)
    {
        var cancelToken = cancellationTokenSource.Token;
        var battleManager = battleUI.BattleManager;
        var audio = PlayAudio(playerDead, battleManager);
        var playerInfo = player.PlayerInfo;

        var str = "{0}はモンスター討伐に失敗した、、、";
        DisplayMessage(messageBox, str, player.characterName);
        await UniTask.Delay(delaytime + 5000, cancellationToken: cancelToken);

        var volume = 0;
        var to = 5;
        audio.DOFade(volume, to).Play();
        await UniTask.Delay(delaytime + 5500, cancellationToken: cancelToken);

        battleManager.BattleInfo.isQuestFail = true;

        player.gold = Mathf.FloorToInt(player.gold / 2);
        player.hp = player.maxHP;
        player.mp = player.maxMP;
        player.ReflectToPlayerInfoAll();

        battleUI.BackToQuestScene();
    }

    public void BattleSuccess(BattleMessageBox messageBox, PlayerAction player)
    {
        enabled = true;
        ResultSuccess(messageBox, player).Forget();
    }

    async UniTaskVoid ResultSuccess(BattleMessageBox messageBox, PlayerAction player)
    {
        var cancelToken = cancellationTokenSource.Token;
        var battleManager = battleUI.BattleManager;
        var playerInfo = battleManager.PlayerAction.PlayerInfo;
        var playerName = player.characterName;

        var audio = PlayAudio(playerWin, battleManager);

        DisplayMessage(messageBox, "{0}はモンスター討伐に成功した！！！\n", playerName);
        await UniTask.WaitUntil(() => messageBox.Available(), cancellationToken: cancelToken);
        await UniTask.Delay(delaytime + 1000, cancellationToken: cancelToken);

        ReflectRewardToPlayer(battleManager, messageBox, player, playerInfo);
        player.ReflectToPlayerInfoAll();
        await UniTask.WaitUntil(() => messageBox.Available(), cancellationToken: cancelToken);
        await UniTask.Delay(delaytime + 2000, cancellationToken: cancelToken);

        var levelUpTable = battleManager.GameInfo.levelUpTable;
        var levels = levelUpTable.levels;

        while (true)
        {
            var playerStatus = playerInfo.status;
            var nextLevel = levels[ playerStatus.lv ];

            if(playerStatus.exp < nextLevel.minimumExp) break;

            PlayAudio(levelUpClip, battleManager);

            playerStatus.lv++;

            playerInfo.status = SetLevelUpValue(playerStatus, messageBox, nextLevel, playerName);
            await UniTask.WaitUntil(() => messageBox.Available(), cancellationToken: cancelToken);
            await UniTask.Delay(delaytime + 1000, cancellationToken: cancelToken);

            var magicCommands = nextLevel.magicCommands;
            if(0 < magicCommands.Length)
            {
                SetLevelUpMagic(messageBox, magicCommands, playerInfo);
                await UniTask.Delay(delaytime + 1000, cancellationToken: cancelToken);
            }

            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancelToken);
        }

        battleUI.BackToQuestScene();
    }

    void SetLevelUpMagic(BattleMessageBox messageBox, string[] magicCommands, PlayerInfo playerInfo)
    {
        messageBox.StringBuilder.Clear();
        var str = "{0}の呪文を覚えた\n";

        foreach(var magicName in magicCommands)
        {
            var command =  masterData.FindMagicCommandFromMaster(magicName);
            messageBox.StringBuilder.AppendFormat(str, command.nameKana);
            playerInfo.magicCommands.Add(command);
        }
        messageBox.DisplayMessage();
    }

    PlayerInfo.Status SetLevelUpValue(PlayerInfo.Status playerStatus, BattleMessageBox messageBox, LevelUpTable.Level levelUp, string playerName)
    {
        messageBox.StringBuilder.Clear();
        messageBox.StringBuilder.AppendFormat("{0}はレベルが{1}になった\n", playerName, playerStatus.lv);

        if (0 < levelUp.hp)
        {
            playerStatus.maxHP += levelUp.hp;
            messageBox.StringBuilder.AppendFormat("HPが{0}上がった\n", levelUp.hp);
        }

        if (0 < levelUp.mp)
        {
            playerStatus.maxMP += levelUp.mp;
            messageBox.StringBuilder.AppendFormat("MPが{0}上がった\n", levelUp.mp);
        }

        if (0 < levelUp.attack)
        {
            playerStatus.attack += levelUp.attack;
            messageBox.StringBuilder.AppendFormat("攻撃力が{0}上がった\n", levelUp.attack);
        }

        if (0 < levelUp.defence)
        {
            playerStatus.defence += levelUp.defence;
            messageBox.StringBuilder.AppendFormat("守備力が{0}上がった\n", levelUp.defence);
        }
        messageBox.DisplayMessage();

        return playerStatus;
    }

    void ReflectRewardToPlayer(BattleManager battleManager, BattleMessageBox messageBox, PlayerAction player, PlayerInfo playerInfo)
    {
        var exp = 0;
        var gold = 0;

        messageBox.StringBuilder.Clear();
        foreach (var r in battleManager.Rewards)
        {
            exp += r.exp;
            gold += r.gold;

            if (r.item != null)
            {
                messageBox.StringBuilder.AppendFormat("{0}は{1}を持っていた。\n", r.monsterName, r.item.nameKana);
                AddItemToPlayerInfo(r, playerInfo);
            }
        }

        player.exp += exp;
        player.gold += gold;

        messageBox.StringBuilder.AppendFormat("{0}は経験値{1}EXと{2}ゴールドを得た。", player.characterName, exp, gold);
        messageBox.DisplayMessage();
    }

    void AddItemToPlayerInfo(BattleReward reward, PlayerInfo playerInfo)
    {
        if (reward.item == null) return;

        var item = playerInfo.items.Find(i => i.itemName == reward.item.itemName);

        if (item != null)
        {
            var limit = item.player_possession_limit;
            var ct = item.player_possession_count;
            if (limit == 0 || ct < limit)
                item.player_possession_count++;
        }
        else
        {
            reward.item.player_possession_count++;
            playerInfo.items.Add(reward.item);
        }
    }

    AudioSource PlayAudio(AudioClip clip, BattleManager battleManager)
    {
        var audio = battleManager.AudioSource;
        audio.clip = clip;
        audio.loop = false;
        audio.Play();
        return audio;
    }
}
