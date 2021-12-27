using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Linq;
using System.Threading;

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
        var playerAction   = battleManager.PlayerAction;
        var monsterActions = battleManager.MonsterActions;
        var messageBox     = battleUI.BattleMessageBox;
        var audioSource    = battleManager.AudioSource;

        var command = attacker?.SelectedCommand;
        var defendersLoopCount = 0;
        var affectPoint = 0;
        var attackerName = "";
        var defenderName = "";

        while(true)
        {            
            AttackOrder(playerAction, monsterActions);

            SelectTurn(playerAction, monsterActions, messageBox);

            if (turn == TURN.PLAYER)
            {
                battleUI.BattleSelector.ActivateBasicCommands(true);
                await UniTask.WaitUntil(() => playerInput, cancellationToken: cancelToken);
            }
            else
            {
                attacker.SelectedCommand = SelectMonsterCommand(monsterActions);
            }

            command = attacker.SelectedCommand;
            defendersLoopCount = 0;

            foreach(var defender in defenders)
            {
                attackerName = attacker.characterName;
                defenderName = defender.characterName;
                affectPoint = 0;

                switch(command.commandType)
                {
                    case Command.COMMAND_TYPE.FIST_ATTACK:
                        DisplayMessage(messageBox, command.ActionMessage(), attackerName, defenderName);
                        await UniTask.WaitUntil(() => messageBox.Available(), cancellationToken: cancelToken);
                        await UniTask.Delay(delaytime + 300, cancellationToken: cancelToken);

                        affectPoint = attacker.Attack(defender);
                        break;
                    case Command.COMMAND_TYPE.ITEM:
                        var item = attacker.SelectedItem;
                        var actionMessage = item.ActionMessage();
                        actionMessage = (actionMessage != null)? actionMessage : command.ActionMessage();

                        DisplayMessage(messageBox, actionMessage, attackerName, item.nameKana);
                        await UniTask.WaitUntil(() => messageBox.Available(), cancellationToken: cancelToken);
                        await UniTask.Delay(delaytime + 250, cancellationToken: cancelToken);   

                        if(attacker.SelectedItem.itemType == Item.ITEM_TYPE.ATTACK_ITEM) command = playerAction.PlayerInfo.battleCommands[0];

                        item.Consume(playerAction.PlayerInfo);
                        affectPoint = item.AffectValue(attacker);

                        playerAction.PlayItemConsumption(audioSource, attacker.SelectedItem, defender as MonsterAction);
                        await UniTask.WaitUntil(() => !defender.AnimationPlaying(), cancellationToken: cancelToken);
                        await UniTask.Delay(delaytime + 150, cancellationToken: cancelToken);
                        break;
                    case Command.COMMAND_TYPE.ESCAPE:
                        DisplayMessage(messageBox, command.ActionMessage(), attackerName);
                        await UniTask.WaitUntil(() => messageBox.Available(), cancellationToken: cancelToken);
                        audioSource.PlayOneShot(command.audioClip);
                        await UniTask.Delay(delaytime + 150, cancellationToken: cancelToken);   
                        
                        if(playerAction.Escape(defender))
                        {
                            playerAction.ReflectToPlayerInfoHpMp();
                            battleUI.BackToQuestScene();
                            enabled = false;
                            return;
                        }
                        break;
                    case Command.COMMAND_TYPE.MAGIC_ATTACK:
                    case Command.COMMAND_TYPE.MAGIC_HEAL:
                        var delay = delaytime;
                        if(defendersLoopCount == 0)
                        {
                            DisplayMessage(messageBox, command.ActionMessage(), attackerName, command.nameKana);
                            await UniTask.WaitUntil(() => messageBox.Available(), cancellationToken: cancelToken);
                            await UniTask.Delay(delaytime + 250, cancellationToken: cancelToken);   

                            attacker.mp -= command.magicCommand.consumptionMp;
                            defender.PlayEnemyAttack(audioSource, command);
    
                            if(typeof(PlayerAction) == attacker.GetType()) playerAction.MpBar.AffectValueToBar(attacker.mp, attacker.maxMP);

                            await UniTask.WaitUntil(() => !defender.AnimationPlaying(), cancellationToken: cancelToken);
                            delay = delaytime + 150;
                        }

                        affectPoint = command.magicCommand.AffectedValue(command.commandType, defender);

                        await UniTask.Delay(delay, cancellationToken: cancelToken);
                        break;
                }

                defendersLoopCount++;

                switch(command.commandType)
                {
                    case Command.COMMAND_TYPE.FIST_ATTACK:
                    case Command.COMMAND_TYPE.MAGIC_ATTACK:
                        if(0 < affectPoint)
                        {
                            defender.hp -= affectPoint;
                            defender.PlayDamage(audioSource);
                            await UniTask.WaitUntil(() => !attacker.TweenPlaying, cancellationToken: cancelToken);

                            defender.HpBar.AffectValueToBar(defender.hp, defender.maxHP);
                            DisplayMessage(messageBox, command.AffectMessage(), defenderName, affectPoint);
                        }
                        else
                        {
                            defender.AttackNoDamage(audioSource);
                            DisplayMessage(messageBox, Command.NoDamagedMessage(), defenderName);
                        }
                        break;
                    case Command.COMMAND_TYPE.MAGIC_HEAL:
                        defender.hp += affectPoint;
                        defender.HpBar.AffectValueToBar(defender.hp, defender.maxHP);
                        DisplayMessage(messageBox, command.AffectMessage(), defenderName, affectPoint);

                        break;
                     case Command.COMMAND_TYPE.ITEM:
                        if(attacker.SelectedItem.healingType == Item.HEALING_TYPE.HP)
                        {
                            defender.hp += affectPoint;
                            defender.HpBar.AffectValueToBar(defender.hp, defender.maxHP);
                        }
                        else
                        {
                            defender.mp += affectPoint;
                            playerAction.MpBar.AffectValueToBar(defender.mp, defender.maxMP);
                        }
                        DisplayMessage(messageBox, attacker.SelectedItem.AffectMessage(), defenderName, attacker.SelectedItem.healingType, affectPoint);
                        break;
                    case Command.COMMAND_TYPE.ESCAPE:
                        DisplayMessage(messageBox, Command.FailedEscapeMessage());
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
                    defenders.Clear();
                    foreach(var monster in monsters)
                    {
                        defenders.Add(monster as BaseAction);
                    }
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
