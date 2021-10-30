using UnityEngine;
using UnityEngine.UI;

public class QuestMenu : MonoBehaviour
{
    [SerializeField] Image coverImage;
    [SerializeField] Image quitConfirm;

    public void GameQuitNo()
    {
        ConfirmGameQuit(false);
        Time.timeScale = 1f;
    }

    public void GameQuitYes()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
        #endif
    }

    public void ConfirmGameQuit(bool activate)
    {
        coverImage.gameObject.SetActive(activate);
        quitConfirm.gameObject.SetActive(activate);
        Time.timeScale = 0f;
    }
}
