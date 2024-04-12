using UnityEngine;
using UnityEngine.EventSystems;

public class TogglePanelButton : MonoBehaviour
{
    public GameObject togglePanel;
    private Animator panelAnimator;

    private void Start()
    {
        panelAnimator = togglePanel.GetComponent<Animator>();
    }

    public void ToggleButton()
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);

        if (panelAnimator.GetBool("TogglePanelActive"))
            HideLevelSelectionPanel();
        else
            ShowLevelSelectionPanel();
    }

    public void ShowLevelSelectionPanel()
    {
        panelAnimator.SetBool("TogglePanelActive", true);
    }

    public void HideLevelSelectionPanel()
    {
        panelAnimator.SetBool("TogglePanelActive", false);
    }

}
