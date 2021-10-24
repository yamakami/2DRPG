using UnityEngine;
using DG.Tweening;

public class PlayerAction : BaseAction
{
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] Camera mainCam;
    [SerializeField] CanvasGroup playerDamageCanvas;

    [SerializeField] LifeBar mpBar;

    int lv;

    public int Lv { get => lv; }
    public CanvasGroup PlayerDamageCanvas { get => playerDamageCanvas; }
    public PlayerInfo PlayerInfo { get => playerInfo; }
    public LifeBar MpBar { get => mpBar; }

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

        HpBar.PlayerName.text = characterName;
        HpBar.AffectValueToBar(hp, maxHP, 0f);
        MpBar.AffectValueToBar(mp, maxMP, 0f);
    }

    override public void PlayDamage(AudioSource audio)
    {
        var alphaMin = 0.18f;
        var alphaMax = 0.375f;
        playerDamageCanvas.alpha = alphaMin;

        TweenPlaying = true;

        playerDamageCanvas.DOFade(alphaMax , 0.3f)
                          .OnComplete(()=> playerDamageCanvas.alpha = 0f)
                          .SetEase(Ease.OutFlash, 1).Play();

        audio.PlayOneShot(damageClip);
        CameraShake();
    }

    void CameraShake()
    {
        var duration = 0.5f;  // 揺れる時間
        var strength = 0.2f; // 揺れる幅
        var vibrato = 20;     // 揺れる回数
        var randomness = 90f;

        mainCam.transform.DOShakePosition(duration, strength, vibrato, randomness)
                         .OnComplete(() => TweenEnd()).Play();
    }

    public void PlayItemConsumption(AudioSource audio, Item item, MonsterAction defender)
    {
        if(item.hasAnimationClip)
            defender.Animator().Play(item.itemName);

        audio.PlayOneShot(item.audioClip);
    }

    public bool Escape(BaseAction enemy)
    {
        if(enemy.speed <= speed) return true;

        return false;
    }

    public void ReflectToPlayerInfoHpMp()
    {
        playerInfo.status.hp = hp;
        playerInfo.status.mp = mp;
    }

    public void ReflectToPlayerInfoAll()
    {
        ReflectToPlayerInfoHpMp();
        playerInfo.status.exp = exp;
        playerInfo.status.gold = gold;
    }
}
