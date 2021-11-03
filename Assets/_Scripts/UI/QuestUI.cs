using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] LocationManager locationManager;
    [SerializeField] Fader fader;
    [SerializeField] MessageBox messageBox;
    QuestManager questManager;

    public QuestManager QuestManager { get => questManager; set => questManager = value; }
    public LocationManager LocationManager { get => locationManager; }
    public MessageBox MessageBox { get => messageBox; }
    public Fader Fader { get => fader; }

    void Start()
    {
        LocationManager.QuestManager = questManager;
        LocationManager.Fader = fader;
        messageBox.QuestManager = questManager;

        if (QuestManager.GameInfo().loadingSceneWithFade == true) LocationManager.FadeIn();
    }

    public void PlayerSatartConversation()
    {
        messageBox.Activate();
    }
}
