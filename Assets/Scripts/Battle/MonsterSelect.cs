using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSelect : MonoBehaviour
{
    //[SerializeField] GameObject leftPanel = default;
    //[SerializeField] Button monsterButton = default;

    //BattleCanvas battleCanvas;

    //public BattleCanvas BattleCanvas { set => battleCanvas = value; }

    //public void Open(Command playerCommand)
    //{
    //    battleCanvas.BattleManager.Command = playerCommand;
    //    leftPanel.SetActive(true);
    //    battleCanvas.MagicCommand.CanvasGroup.interactable = false;
    //    battleCanvas.BattleBasicCommand.CanvasGroup.interactable = false;
    //}

    //public void Close() {
    //    leftPanel.SetActive(false);
    //    battleCanvas.MagicCommand.CanvasGroup.interactable = true;
    //    battleCanvas.BattleBasicCommand.CanvasGroup.interactable = true;
    //}

    //void OnEnable()
    //{
    //    var bm = battleCanvas.BattleManager;

    //    int monsterIndex = 0;
    //    foreach (var ma in bm.MonsterActions)
    //    {
    //        ma.monsterIndex = monsterIndex;
    //        Button bt = CreateButton(transform, monsterButton, ma.monster.monsterName);
    //        bt.onClick.AddListener(() => bm.PlayerAction.SelectedCommnad(ma));
    //        monsterIndex++;
    //    }

    //    Button backBt = CreateButton(transform, monsterButton, "もどる");
    //    backBt.onClick.AddListener(() => Close());
    //}

    //public Button CreateButton(Transform tr, Button button, string name, string mp = null)
    //{
    //    Button bt = Instantiate(button);
    //    bt.transform.SetParent(tr);
    //    bt.transform.localScale = Vector3.one;

    //    Text[] textfield = bt.GetComponentsInChildren<Text>();
    //    textfield[0].text = name;

    //    if (mp != null)
    //        textfield[1].text = mp;

    //    return bt;
    //}

    //void OnDisable()
    //{
    //    foreach (Transform t in GetComponentInChildren<Transform>())
    //    {
    //        Destroy(t.gameObject);
    //    }
    //}
}
//public class MonsterSelect : MonoBehaviour
//{
//    [SerializeField] Button monsterButton = default;
//    [HideInInspector] BattleCanvas battleCanvas;

//    public BattleCanvas BattleCanvas { set => battleCanvas = value; }

//    void OnEnable()
//    {
//        var bm = battleCanvas.BattleManager;

//        int monsterIndex = 0;
//        foreach (var ma in bm.MonsterActions)
//        {
//            ma.index = monsterIndex;
//            Button bt = CreateButton(transform, monsterButton, ma.monster.monsterName);
//            bt.onClick.AddListener(() => bm.PlayerAction.SelectedCommnad(ma));
//            monsterIndex++;
//        }

//        Button backBt = CreateButton(transform, monsterButton, "もどる");
//        backBt.onClick.AddListener(() => Close());
//    }

//    public Button CreateButton(Transform tr, Button button, string name, string mp = null)
//    {
//        Button bt = Instantiate(button);
//        bt.transform.SetParent(tr);
//        bt.transform.localScale = Vector3.one;

//        Text[] textfield = bt.GetComponentsInChildren<Text>();
//        textfield[0].text = name;

//        if (mp != null)
//            textfield[1].text = mp;

//        return bt;
//    }

//    void OnDisable()
//    {
//        foreach (Transform t in GetComponentInChildren<Transform>())
//        {
//            Destroy(t.gameObject);
//        }
//    }

//    public void Open(Command playerCommand)
//    {
//        battleCanvas.BattleManager.Command = playerCommand;
//        OpenCloseParent(true);
//        gameObject.SetActive(true);
//    }

//    public void Close()
//    {
//        OpenCloseParent(false);
//        gameObject.SetActive(false);
//    }

//    void OpenCloseParent(bool b)
//    {
//        gameObject.transform.parent.gameObject.SetActive(b);
//    }
//}
