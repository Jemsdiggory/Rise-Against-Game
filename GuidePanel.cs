using UnityEngine;

public class GuidePanel : MonoBehaviour
{
    public GameObject guidePanel;

    public void OpenGuidePanel()
    {
        guidePanel.SetActive(true);
    }

    public void CloseGuidePanel()
    {
        guidePanel.SetActive(false);
    }
}
