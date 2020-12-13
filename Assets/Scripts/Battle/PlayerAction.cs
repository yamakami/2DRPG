using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : CharacterAction
{
    public PlayerInfo playerInfo = default;
    public LevelUpTable levelUpTable;
    public int lv;
    float luck;


    void Start()
    {
        characterName = playerInfo.playerName;
        lv = playerInfo.status.lv;
        maxHP = playerInfo.status.maxHP;
        maxMP = playerInfo.status.maxMP;
        hp = playerInfo.status.hp;
        attack = playerInfo.status.attack;
        defence = playerInfo.status.defence;
        mp = playerInfo.status.mp;
        exp = playerInfo.status.exp;
        gold = playerInfo.status.gold;

        calculateLuck();
        speed = attack + defence + luck;

        //StatusBar.SetInitialValues(maxHP);
        //StatusBar.SubtructValue(hp);
    }

    public void calculateLuck()
    {
        luck = 1 + Random.Range(0.0f, 0.5f);
    }

    void OnDestroy()
    {
        playerInfo.status.lv = lv;
        playerInfo.status.maxHP = maxHP;
        playerInfo.status.maxMP = maxMP;
        playerInfo.status.hp = hp;
        playerInfo.status.mp = mp;
        playerInfo.status.attack = attack;
        playerInfo.status.defence = defence;
        playerInfo.status.exp = exp;
        playerInfo.status.gold = gold;
        AddItemToPlayerInfo();
    }

    void AddItemToPlayerInfo()
    {
        foreach (BattleReward reward in BattleManager.Rewards)
        {
            if (reward.item == null)
                continue;

            Item item = playerInfo.items.Find(i => i.itemName == reward.item.itemName);
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
    }

    public void SelectedCommnad(CharacterAction monster)
    {
        var bm = BattleManager;
        switch (bm.Command.commandType)
        {
            case Command.COMMAND_TYPE.ATTACK_PLAYER:
                PhysicalAttack(monster);
                break;
            case Command.COMMAND_TYPE.MAGIC_ATTACK:
                MagicAttack(monster);
                break;
        }

        bm.PlayableDirector.Resume();
    }

    void PhysicalAttack(CharacterAction monster)
    {
        BattleManager.Defender = monster;

        int damage = Mathf.Abs(monster.defence - attack);
        BattleManager.Damage = damage;

        var c = BattleManager.BattleCanvas;

        c.MonsterSelect.Close();
        c.BattleBasicCommand.Close();

        string str = "{0}の攻撃！\n{1}を攻撃した！!";
        BattleManager.BattleMessage.AppendFormat(str, characterName, monster.characterName);

        str = "{0}に{1}HPのダメージをあたえた！";
        BattleManager.ResultMessage.AppendFormat(str, monster.characterName, damage);
    }

    void MagicAttack(CharacterAction monster)
    {
        var command = BattleManager.Command;
        BattleManager.Defender = monster;

        int damage = Mathf.Abs(monster.defence - command.magicInfo.MagicAttack());
        BattleManager.Damage = damage;

        this.mp -= command.magicInfo.consumptionMp;

        var c = BattleManager.BattleCanvas;

        c.MonsterSelect.Close();
        c.MagicCommand.Close();
        c.MagicBasicCommand.CloseWithOutBasicCommand();

        string str = "{0}の攻撃！\n{1}に{2}を唱えた！";
        BattleManager.BattleMessage.AppendFormat(str, characterName, monster.characterName, command.nameKana);

        str = "{0}に{1}HPのダメージをあたえた！";
        BattleManager.ResultMessage.AppendFormat(str, monster.characterName, damage);
    }

    public void MagicHeal(Command command)
    {
        BattleManager.Command = command;

        var recoveredHp = command.magicInfo.Heal(hp, maxHP);
        this.mp -= command.magicInfo.consumptionMp;

        BattleManager.Damage = -recoveredHp;

        BattleManager.Defender = this;

        var c = BattleManager.BattleCanvas;

        c.MonsterSelect.Close();
        c.MagicCommand.Close();
        c.MagicBasicCommand.CloseWithOutBasicCommand();

        string str = "{0}は{1}を唱えた！";
        BattleManager.BattleMessage.AppendFormat(str, characterName, command.nameKana);

        str = "{0}は{1}HP回復した。";
        BattleManager.ResultMessage.AppendFormat(str, characterName, recoveredHp);

        BattleManager.PlayableDirector.Resume();
    }
}
