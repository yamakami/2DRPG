
using UnityEngine;
using DG.Tweening;

public class PlayerAction : BaseAction
{
    [SerializeField] BattleUI battleUI;
    [SerializeField] Camera mainCam;
    [SerializeField] CanvasGroup playerDamageCanvas;

    private int lv;

    public int Lv { get => lv; }
    public CanvasGroup PlayerDamageCanvas { get => playerDamageCanvas; }

    void Start()
    {
        var playerInfo = battleUI.BattleManager.PlayerInfo;

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
}
