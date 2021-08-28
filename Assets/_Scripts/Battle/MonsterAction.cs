using UnityEngine;
using DG.Tweening;

public class MonsterAction : BaseAction
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Monster monster;
    [SerializeField] Animator allEffect;
    public Monster Monster { get => monster; set => monster = value; }

    void Start()
    {        
        spriteRenderer.sprite = monster.monsterSprite;
        Monster.Status status = monster.status;

        maxHP = hp = Random.Range(status.hp[0], status.hp[1] + 1);
        maxMP = mp = Random.Range(status.mp[0], status.mp[1] + 1);
        attack = Random.Range(status.attack[0], status.attack[1] + 1);
        defence = Random.Range(status.defence[0], status.defence[1] + 1);
        exp = Random.Range(status.exp[0], status.exp[1] + 1);
        gold = Random.Range(status.gold[0], status.gold[1] + 1);
    }

    public void PlayDead(AudioSource audio)
    {
        TweenPlaying = true;
        audio.PlayOneShot(deadClip);

        spriteRenderer.DOColor(new Color32(255, 255, 255, 0) , 1f)
                      .OnComplete(() => TweenEnd()).Play();
    }

    override public void PlayDamage(AudioSource audio)
    {
        TweenPlaying = true;
        var colorRed = new Color32(255, 80, 80, 255);
        spriteRenderer.color = colorRed;

        spriteRenderer.DOColor(Color.white , 1f)
                      .OnComplete(() => TweenEnd())
                      .SetEase(Ease.OutFlash, 1).Play();

        audio.PlayOneShot(damageClip);
    }

    public override void PlayEnemyAttack(AudioSource audio, Command command)
    {
        var anim = enemyAttackAnim;
        if(command.magicCommand.magicTarget == MagicCommand.MAGIC_TARGET.ALL) anim = allEffect;

        anim.Play(command.commandName);
        audio.PlayOneShot(command.audioClip);
    }

}
