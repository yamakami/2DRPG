using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Linq;

public class FlowMain : FlowBase
{
    [SerializeField] FlowResult flowResult;

    BaseAction attacker;
    List<BaseAction> defenders = new List<BaseAction>(3);
    List<BaseAction> attackOrder = new List<BaseAction>(3);

    bool playerInput;
    TURN turn;

    enum TURN { PLAYER, MONSTER }

    public bool PlayerInput { get => playerInput; set => playerInput = value; }
    public List<BaseAction> Defenders { get => defenders; set => defenders = value; }
    public BaseAction Attacker { get => attacker; set => attacker = value; }

    void OnEnable()
    {
        battleUI.BattleSelector.FlowMain = this;
        flowResult.BattleUI = battleUI;

        BattleLoop().Forget();
    }

    async UniTaskVoid BattleLoop()
    {
        var cancelToken    = cancellationTokenSource.Token;
        var battleManager  = battleUI.BattleManager;
        var playerAction   =  battleManager.PlayerAction;
        var monsterActions = battleManager.MonsterActions;
        var messageBox     = battleUI.BattleMessageBox;
        var audioSource    = battleManager.AudioSource;

        var attackerName = "";
        var defenderName = "";

        while(true)
        {            
            AttackOrder(playerAction, monsterActions);

            SelectTurn(playerAction, monsterActions, messageBox);

            if (turn == TURN.PLAYER)
            {
                var open = true;
                battleUI.BattleSelector.ActivateBasicCommands(open);
                await UniTask.WaitUntil(() => playerInput, cancellationToken: cancelToken);
            }
            else
            {
                attacker.SelectedCommand = SelectMonsterCommand(monsterActions);
            }

            var command = attacker.SelectedCommand;
            var defendersLoopCount = 0;

            foreach(var defender in defenders)
            {
                var damage = 0;
                var heal   = 0;
                attackerName = attacker.characterName;
                defenderName = defender.characterName;

                switch(command.commandType)
                {
                    case Command.COMMAND_TYPE.FIST_ATTACK:
                        DisplayMessage(messageBox, "{0}は{1}を攻撃した！", attackerName, defenderName);
                        await UniTask.WaitUntil(() => messageBox.Available(), cancellationToken: cancelToken);
                        await UniTask.Delay(delaytime + 300, cancellationToken: cancelToken);

                        damage = attacker.Attack(defender);
                        break;

                    default:
                        if(defendersLoopCount == 0)
                        {
                            DisplayMessage(messageBox, "{0}は{1}を唱えた！", attackerName, command.nameKana);
                            await UniTask.WaitUntil(() => messageBox.Available(), cancellationToken: cancelToken);
                            await UniTask.Delay(delaytime + 250, cancellationToken: cancelToken);   

                            defender.PlayEnemyAttack(audioSource, command);

                            await UniTask.WaitUntil(() => !defender.AnimationPlaying(), cancellationToken: cancelToken);
                            await UniTask.Delay(delaytime + 150, cancellationToken: cancelToken);
                        }

                        if(attacker.SelectedCommand.commandType == Command.COMMAND_TYPE.MAGIC_ATTACK)
                            damage = command.magicCommand.MagicAttack();
                        else
                            heal = attacker.SelectedCommand.magicCommand.Heal(defender.hp, defender.maxMP);

                        await UniTask.Delay(delaytime, cancellationToken: cancelToken);
                        attacker.mp -= command.magicCommand.consumptionMp;

                        break;
                }

                defendersLoopCount++;

                switch(command.commandType)
                {
                    case Command.COMMAND_TYPE.MAGIC_HEAL:
                        defender.hp += heal;
                        DisplayMessage(messageBox, "{0}は{1}HP回復した！" , defenderName, heal);
                        break;
                    default:
                        if(0 < damage)
                        {
                            defender.hp -= damage;
                            defender.PlayDamage(audioSource);
                            await UniTask.WaitUntil(() => !attacker.TweenPlaying, cancellationToken: cancelToken);

                            DisplayMessage(messageBox, "{0}は{1}HPのダメージを受けた！！！" , defenderName, damage);
                        }
                        else
                        {
                            defender.AttackNoDamage(audioSource);
                            DisplayMessage(messageBox, "{0}はダメージを受けてない！！！！" , defenderName);
                        }
                        break;
                }

                await UniTask.WaitUntil(() => messageBox.Available(), cancellationToken: cancelToken);
                await UniTask.Delay(delaytime + 300, cancellationToken: cancelToken);

                if(defender.hp <= 0)
                {
                    if(turn == TURN.PLAYER)
                    {
                        var monster = defender as MonsterAction;

                        battleManager.KeepReward(monster);

                        monster.PlayDead(audioSource);
                        await UniTask.WaitUntil(() => !attacker.TweenPlaying, cancellationToken: cancelToken);

                        MonsterDead(messageBox, monsterActions, monster, attacker as PlayerAction);
                        await UniTask.WaitUntil(() => messageBox.Available(), cancellationToken: cancelToken);
                        await UniTask.Delay(delaytime + 300, cancellationToken: cancelToken);
                        GameObject.Destroy(defender);
                    }

                    if(turn == TURN.MONSTER)
                    {
                        flowResult.BattleFail(messageBox, defender as PlayerAction);
                        enabled = false;
                    }
                }
            }

            if(monsterActions.Count < 1)
            {
                flowResult.BattleSuccess(messageBox, attacker as PlayerAction);
                enabled = false;
            }

            await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancelToken);
        }
    }

    Command SelectMonsterCommand(List<MonsterAction> monsters)
    {
        var monsterAttacker = attacker as MonsterAction;
        var commands = monsterAttacker.Monster.commands.ToList().FindAll(c => c.magicCommand.consumptionMp <= attacker.mp);
        var healCommands = monsterAttacker.Monster.healCommands;

        if(healCommands.Length < 1)
            return commands[ Random.Range(0, commands.Count) ];

        var healTargets = MonsterHealTarget(monsters);

        if( healTargets.Count < 1)
            return commands[ Random.Range(0, commands.Count) ];

        var commandSelected = commands[ Random.Range(0, commands.Count) ];

        var availablehealCommands = healCommands.ToList().FindAll(c => c.magicCommand.consumptionMp <= attacker.mp);
        var ct = availablehealCommands.Count;
        if(0 < ct)
            commandSelected = availablehealCommands[ Random.Range(0, ct) ];

        switch(commandSelected.commandType)
        {
            case Command.COMMAND_TYPE.MAGIC_HEAL:
                if(commandSelected.magicCommand.magicTarget == MagicCommand.MAGIC_TARGET.ALL)
                {
                    defenders = healTargets;
                }

                if(commandSelected.magicCommand.magicTarget == MagicCommand.MAGIC_TARGET.ONE)
                {
                    defenders = healTargets;
                    defenders.Sort((v1, v2) => v2.hp.CompareTo(v1.hp));
                    defenders.RemoveRange(1, defenders.Count - 1);
                }
                break;
 
            default:
                break;

        }

       return commandSelected;
    }

    List<BaseAction> MonsterHealTarget(List<MonsterAction> monsterActions)
    {
        var healTargets = new List<BaseAction>(3);
        var ratios = new float[] {2f, 3f, 4f};
        var ratio = ratios[Random.Range(0, ratios.Length)];

        foreach (var monster in monsterActions)
        {
            var healPoint = monster.maxHP / ratio;
            if (monster.hp <= healPoint)
            {
                healTargets.Add(monster);
            }
        }

        return healTargets;
    }

    void AttackOrder(PlayerAction playerAction, List<MonsterAction> monsterActions)
    {
        if (0 < attackOrder.Count) return;

        attackOrder.Add(playerAction);

        playerAction.GenerteLuckAndSpeed();
        foreach (var monsterAction in monsterActions)
        {
            monsterAction.GenerteLuckAndSpeed();
            attackOrder.Add(monsterAction);
        }

        attackOrder.Sort((v1, v2) => v2.speed.CompareTo(v1.speed));
    }

    void SelectTurn(PlayerAction playerAction, List<MonsterAction> monsterActions, BattleMessageBox messageBox)
    {
        defenders.Clear();

        attacker = attackOrder.First();
        attackOrder.RemoveAt(0);

        if (attacker is PlayerAction)
        {
            turn = TURN.PLAYER;
            defenders.AddRange(monsterActions);
            playerInput = false;
            messageBox.Deactivate();
        }
        else
        {
            turn = TURN.MONSTER;
            defenders.Add(playerAction);
            playerInput = true;
        }
    }

    void MonsterDead(BattleMessageBox messageBox, List<MonsterAction> monsterActions, MonsterAction defender, PlayerAction attacker)
    {
        var str = "{0}を退治した！";
        DisplayMessage(messageBox, str, defender.characterName);

        attackOrder.Remove(defender);
        monsterActions.Remove(defender);
    }
}
