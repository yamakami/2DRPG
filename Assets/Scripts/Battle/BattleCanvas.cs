using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCanvas : MonoBehaviour
{
    [SerializeField] MessageBox messageBox = default;
    [SerializeField] BattleBasicCommand battleBasicCommand = default;
    [SerializeField] MagicBasicCommand magicBasicCommand = default;
    [SerializeField] MagicCommand magicCommand = default;
    [SerializeField] MonsterSelect monsterSelect = default;
    [SerializeField] FaderBlack faderBlack = default;

    [HideInInspector] BattleManager battleManager;

    public BattleManager BattleManager { get => battleManager; set => battleManager = value; }
    public MessageBox MessageBox { get => messageBox; }
    public BattleBasicCommand BattleBasicCommand { get => battleBasicCommand; }
    public MagicBasicCommand MagicBasicCommand { get => magicBasicCommand; }
    public MagicCommand MagicCommand { get => magicCommand; }
    public MonsterSelect MonsterSelect { get => monsterSelect; }

    private void Start()
    {
        MonsterSelect.BattleCanvas     = this;
        MagicBasicCommand.BattleCanvas = this;
    }

    public void SceneFadeOut()
    {
        faderBlack.transform.parent.gameObject.SetActive(true);
    }

    public bool SceneFadeOutFinished()
    {
        if (faderBlack.Fading)
            return false;

        return true;
    }
}
