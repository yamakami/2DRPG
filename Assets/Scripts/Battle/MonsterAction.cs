using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAction : CharacterAction
{
    [SerializeField] SpriteRenderer spriteRenderer = default;

    [HideInInspector] public Monster monster;
    [HideInInspector] public int monsterIndex;

    Command command;
    int damage;
    CharacterAction target;

    void Start()
    {
        spriteRenderer.sprite = monster.monsterSprite;

        Monster.Status status = monster.status;

        characterName = monster.monsterName;

        maxHP = hp = Random.Range(status.hp[0], status.hp[1] + 1);
        maxMP = mp = Random.Range(status.mp[0], status.mp[1] + 1);

        attack = Random.Range(status.attack[0], status.attack[1] + 1);
        defence = Random.Range(status.defence[0], status.defence[1] + 1);
        exp = Random.Range(status.exp[0], status.exp[1] + 1);
        gold = Random.Range(status.gold[0], status.gold[1] + 1);
        speed = attack + defence;

        //StatusBar.SetInitialValues(maxHP);
    }

    public void Attack(PlayerAction player)
    {
        if (!IsCommandHeal())
            command = monster.commands[Random.Range(0, monster.commands.Length)];

        var bm = BattleManager;
        int damage = 0;
        string str = "";
        switch (command.commandType)
        {
            case Command.COMMAND_TYPE.MAGIC_ATTACK:
                target = player;
                damage = bm.DamageCalculation(command.magicInfo.MagicAttack(), player.defence);
                this.mp -= command.magicInfo.consumptionMp;

                str = "{0}は{1}の呪文を唱えた！\n";
                bm.BattleMessage.AppendFormat(str, monster.monsterName, command.nameKana);

                str = "{0}は{1}HPのダメージをうけた！";
                bm.ResultMessage.AppendFormat(str, player.characterName, damage);
                break;

            case Command.COMMAND_TYPE.MAGIC_HEAL:

                var recoveredHp = command.magicInfo.Heal(target.hp, target.maxHP);
                this.mp -= command.magicInfo.consumptionMp;

                damage = -recoveredHp;

                bm.Defender = target;

                MonsterAction ma = (MonsterAction)target;
                if (this.monsterIndex == ma.monsterIndex)
                {
                    str = "{0}は{1}を唱えた！";
                    bm.BattleMessage.AppendFormat(str, characterName, command.nameKana);
                }
                else
                {
                    str = "{0}は{1}に{2}を唱えた！";
                    bm.BattleMessage.AppendFormat(str, characterName, target.characterName, command.nameKana);
                }

                str = "{0}は{1}HP回復した。";
                bm.ResultMessage.AppendFormat(str, target.characterName, recoveredHp);
                break;

            default:
                target = player;
                damage = bm.DamageCalculation(attack, player.defence);
                str = "{0}の攻撃！\n";
                bm.BattleMessage.AppendFormat(str, monster.monsterName);

                str = "{0}は{1}HPのダメージをうけた！";
                bm.ResultMessage.AppendFormat(str, player.characterName, damage);
                break;
        }

        bm.Defender = target;
        bm.Damage = damage;
        bm.Command = command;
    }

    bool IsCommandHeal()
    {
        if (monster.healCommands.LongLength < 1)
            return false;

        MonsterAction lowLifeMonster = null;
        float lowestLife = 999999f;

        for (var i = 0; i < BattleManager.MonsterActions.Count; i++)
        {
            MonsterAction ma = BattleManager.MonsterActions[i];
            float healPoint = ma.maxHP / 4f;

            if (ma.hp <= healPoint)
            {
                if (ma.hp < lowestLife)
                {
                    lowLifeMonster = ma;
                    lowestLife = ma.hp;
                }
            }
        }

        if (lowLifeMonster == null)
            return false;

        if (this.mp < monster.healCommands[0].magicInfo.consumptionMp)
            return false;

        command = monster.healCommands[0];
        target = lowLifeMonster;

        return true;
    }
}