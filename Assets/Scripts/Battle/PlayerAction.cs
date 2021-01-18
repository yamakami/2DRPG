using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : CharacterAction
{
    //public PlayerInfo playerInfo = default;
    //public LevelUpTable levelUpTable;
    //public int lv;
    //float luck;


    //void Start()
    //{
    //    characterName = playerInfo.playerName;
    //    lv = playerInfo.status.lv;
    //    maxHP = playerInfo.status.maxHP;
    //    maxMP = playerInfo.status.maxMP;
    //    hp = playerInfo.status.hp;
    //    attack = playerInfo.status.attack;
    //    defence = playerInfo.status.defence;
    //    mp = playerInfo.status.mp;
    //    exp = playerInfo.status.exp;
    //    gold = playerInfo.status.gold;

    //    speed = attack + defence + CalculateLuck();

    //    //StatusBar.SetInitialValues(maxHP);
    //    //StatusBar.SubtructValue(hp);
    //}

    //public float CalculateLuck()
    //{
    //    return 1 + Random.Range(0.0f, 0.5f);
    //}

    //void OnDestroy()
    //{
    //    playerInfo.status.lv = lv;
    //    playerInfo.status.maxHP = maxHP;
    //    playerInfo.status.maxMP = maxMP;
    //    playerInfo.status.hp = hp;
    //    playerInfo.status.mp = mp;
    //    playerInfo.status.attack = attack;
    //    playerInfo.status.defence = defence;
    //    playerInfo.status.exp = exp;
    //    playerInfo.status.gold = gold;
    //    AddItemToPlayerInfo();
    //}

    //void AddItemToPlayerInfo()
    //{
    //    foreach (BattleReward reward in BattleManager.Rewards)
    //    {
    //        if (reward.item == null)
    //            continue;

    //        Item item = playerInfo.items.Find(i => i.itemName == reward.item.itemName);
    //        if (item != null)
    //        {
    //            var limit = item.player_possession_limit;
    //            var ct = item.player_possession_count;
    //            if (limit == 0 || ct < limit)
    //                item.player_possession_count++;
    //        }
    //        else
    //        {
    //            reward.item.player_possession_count++;
    //            playerInfo.items.Add(reward.item);
    //        }
    //    }
    //}

    //public void SelectedCommnad(CharacterAction monster)
    //{
    //    var bm = BattleManager;
    //    switch (bm.Command.commandType)
    //    {
    //        case Command.COMMAND_TYPE.ATTACK_PLAYER:
    //            PhysicalAttack(monster);
    //            break;
    //        case Command.COMMAND_TYPE.MAGIC_ATTACK:
    //            MagicAttack(monster);
    //            break;
    //    }

    //    bm.PlayableDirector.Resume();
    //}

    //void PhysicalAttack(CharacterAction monster)
    //{
    //    var bm = BattleManager;

    //    bm.Defender = monster;

    //    int damage = bm.DamageCalculation(attack, monster.defence);
    //    bm.Damage = damage;

    //    var c = bm.BattleCanvas;

    //    c.MonsterSelect.Close();
    //    c.BattleBasicCommand.Close();

    //    string str = "{0}の攻撃！\n{1}を攻撃した！!";
    //    bm.BattleMessage.AppendFormat(str, characterName, monster.characterName);

    //    str = "{0}に{1}HPのダメージをあたえた！";
    //    bm.ResultMessage.AppendFormat(str, monster.characterName, damage);
    //}

    //void MagicAttack(CharacterAction monster)
    //{
    //    var bm = BattleManager;
    //    var command = BattleManager.Command;

    //    bm.Defender = monster;

    //    int damage = bm.DamageCalculation(command.magicInfo.MagicAttack(), monster.defence);
    //    bm.Damage = damage;

    //    this.mp -= command.magicInfo.consumptionMp;

    //    var c = bm.BattleCanvas;

    //    c.MonsterSelect.Close();
    //    c.MagicCommand.Close();
    //    c.MagicBasicCommand.CloseWithOutBasicCommand();

    //    string str = "{0}の攻撃！\n{1}に{2}を唱えた！";
    //    bm.BattleMessage.AppendFormat(str, characterName, monster.characterName, command.nameKana);

    //    str = "{0}に{1}HPのダメージをあたえた！";
    //    bm.ResultMessage.AppendFormat(str, monster.characterName, damage);
    //}

    //public void MagicHeal(Command command)
    //{
    //    var bm = BattleManager;

    //    bm.Command = command;

    //    var recoveredHp = command.magicInfo.Heal(hp, maxHP);
    //    this.mp -= command.magicInfo.consumptionMp;

    //    bm.Damage = -recoveredHp;

    //    bm.Defender = this;

    //    var c = bm.BattleCanvas;

    //    c.MonsterSelect.Close();
    //    c.MagicCommand.Close();
    //    c.MagicBasicCommand.CloseWithOutBasicCommand();

    //    string str = "{0}は{1}を唱えた！";
    //    bm.BattleMessage.AppendFormat(str, characterName, command.nameKana);

    //    str = "{0}は{1}HP回復した。";
    //    bm.ResultMessage.AppendFormat(str, characterName, recoveredHp);

    //    bm.PlayableDirector.Resume();
    //}
}
