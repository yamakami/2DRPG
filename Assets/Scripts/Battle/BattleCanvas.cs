using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCanvas : MonoBehaviour
{
    [SerializeField] CutOffCanvas cutOffCanvas = default;
    [SerializeField] BattleBasicCommand battleBasicCommand = default;
    [SerializeField] MagicBasicCommand magicBasicCommand = default;
    [SerializeField] MagicCommand magicCommand = default;
    [SerializeField] MessageText messageText = default;
    [SerializeField] MonsterSelect monsterSelect = default;
    [SerializeField] Fader fader = default;

    BattleManager battleManager;

    public BattleManager BattleManager { get => battleManager; set => battleManager = value; }
    public CutOffCanvas CutOffCanvas { get => cutOffCanvas; }
    public MessageText MessageText { get => messageText; }
    public BattleBasicCommand BattleBasicCommand { get => battleBasicCommand; }
    public MagicBasicCommand MagicBasicCommand { get => magicBasicCommand; }
    public MagicCommand MagicCommand { get => magicCommand; }
    public MonsterSelect MonsterSelect { get => monsterSelect; }
    public Fader Fader { get => fader; }

    private void Start()
    {
        MonsterSelect.BattleCanvas = this;
        BattleBasicCommand.BattleCanvas = this;
        MagicBasicCommand.BattleCanvas = this;
    }

    public void SceneFadeOut()
    {
        Fader.FadeIn();
    }
}
