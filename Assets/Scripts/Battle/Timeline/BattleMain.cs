using UnityEngine;

public class BattleMain : BattleTimeline
{
    //[SerializeField] BattleTimeline playerDied = default;

    //TURN turn;
    //enum TURN { PLAYER, MONSTER }

    //public void PickTurn()
    //{
    //    DetermineTurn(battleManager);
    //    AttackSelect(battleManager);
    //}

    //public void AttackMessage()
    //{
    //    PlayableStop();
    //    messageBox.DisplayMessage(battleManager.BattleMessage);
    //}

    //public void CloseMessageBox()
    //{
    //    messageBox.Close();
    //}

    //public void AttackEffect()
    //{
    //    var bm = battleManager;
    //    Animator animator = bm.Defender.Animator;
    //    AudioSource audio = bm.Audio;
    //    Command command = bm.Command;

    //    PlayableStop();
    //    animator.Play(command.commandName, 0, 0);
    //    audio.PlayOneShot(command.audioClip);
    //}

    //public void AttackResultMessage()
    //{
    //    PlayableStop();
    //    messageBox.Open();
    //    messageBox.DisplayMessage(battleManager.ResultMessage);
    //}

    //public void TurnResult()
    //{
    //    var bm = battleManager;

    //    bm.Defender.hp -= bm.Damage;
    //    //bm.Defender.StatusBar.SubtructValue(bm.Defender.hp);

    //    if (turn == TURN.MONSTER)
    //    {
    //        if (bm.Defender.hp <= 0)
    //            ChangeToDied();
    //    }
    //    else
    //    {
    //        if (bm.Defender.hp <= 0)
    //        {
    //            MonsterAction monster = (MonsterAction)bm.Defender;

    //            string str = "{0}を退治した！";
    //            bm.BattleMessage.AppendFormat(str, monster.characterName);

    //            bm.KeepReward(monster);

    //            RemoveFromAttackOrder(monster);

    //            bm.MonsterActions.RemoveAt(monster.monsterIndex);

    //            Destroy(bm.Defender.gameObject);

    //            PlayableStop();

    //            messageBox.DisplayMessage(bm.BattleMessage);
    //        }
    //    }
    //}

    //void ChangeToDied()
    //{
    //    playerDied.gameObject.SetActive(true);
    //    playerDied.BattleManager = battleManager;
    //    Destroy(gameObject);
    //}

    //public void BattleResult()
    //{
    //    var bm = battleManager;

    //    if (bm.MonsterActions.Count <= 0)
    //        ChangeTimeline();
    //}

    //void DetermineTurn(BattleManager bm)
    //{
    //    AttackOrder(bm);

    //    bm.Attacker = bm.AttackOrder[0];
    //    bm.AttackOrder.RemoveAt(0);

    //    if (bm.Attacker is PlayerAction)
    //    {
    //        turn = TURN.PLAYER;
    //    }
    //    else
    //    {
    //        turn = TURN.MONSTER;
    //        bm.Defender = bm.PlayerAction;
    //    }
    //}

    //void AttackSelect(BattleManager bm)
    //{
    //    if (turn == TURN.PLAYER)
    //    {
    //        playableDirector.Pause();
    //        bm.BattleCanvas.BattleBasicCommand.Open();
    //    }
    //    else
    //    {
    //        MonsterAction monster = (MonsterAction)bm.Attacker;
    //        monster.Attack((PlayerAction)bm.Defender);
    //    }
    //}

    //void AttackOrder(BattleManager bm)
    //{
    //    if (0 < bm.AttackOrder.Count)
    //        return;

    //    bm.AttackOrder.Add(bm.PlayerAction);

    //    foreach (MonsterAction monsterAction in bm.MonsterActions)
    //    {
    //        bm.AttackOrder.Add(monsterAction);
    //    }

    //    bm.AttackOrder.Sort((v1, v2) => v2.speed.CompareTo(v1.speed));
    //}

    //void RemoveFromAttackOrder(MonsterAction monster)
    //{
    //    var bm = battleManager;
    //    for (var i = 0; i < bm.AttackOrder.Count; i++)
    //    {
    //        if (bm.AttackOrder[i].characterName == monster.characterName)
    //        {
    //            bm.AttackOrder.RemoveAt(i);
    //            break;
    //        }
    //    }
    //}
}
