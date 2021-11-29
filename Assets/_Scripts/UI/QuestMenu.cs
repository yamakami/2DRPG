using UnityEngine;
using UnityEngine.UI;

public class QuestMenu : MonoBehaviour
{
    [SerializeField] Image coverImage;
    [SerializeField] Image quitConfirm;
    [SerializeField] Image menuOption;
    [SerializeField] Image statusData;
    [SerializeField] Image magicsSelect;
    [SerializeField] Image itemsSelect;
    [SerializeField] Image equipment;

    public void GameQuitYes()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
        #endif
    }

    public void ConfirmGameQuit(bool active)
    {
        ActivateCoverImage(active);
        quitConfirm.gameObject.SetActive(active);
    }

    public void Show(bool active)
    {
        gameObject.SetActive(active);
    }

    public void ShowMenuOption(bool active)
    {
        menuOption.gameObject.SetActive(active);
    }

    public void ShowStatusData(bool active)
    {
        statusData.gameObject.SetActive(active);
    }

    public void ShowMagicsSelect(bool active)
    {
        magicsSelect.gameObject.SetActive(active);
    }

    public void ShowItemsSelect(bool active)
    {
        itemsSelect.gameObject.SetActive(active);
    }

    public void ShowEquipment(bool active)
    {
        equipment.gameObject.SetActive(active);
    }

    public void ActivateCoverImage(bool active)
    {
        coverImage.gameObject.SetActive(active);

        var timeScale = (active)? 0 : 1; 
        GamePause(timeScale);
    }

    public void GamePause(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
