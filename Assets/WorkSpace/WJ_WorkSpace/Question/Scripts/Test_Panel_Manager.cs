using UnityEngine;
using UnityEngine.UI;

public class Test_Panel_Manager : MonoBehaviour
{
    // References to the panels you want to activate and deactivate
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;

    // References to the buttons
    public Button one_Next;
    public Button two_Pre;
    public Button two_Next;

    void Start()
    {
        // Assign button click event listeners
        one_Next.onClick.AddListener(() => SwitchPanels(Panel1, Panel2));
        two_Pre.onClick.AddListener(() => SwitchPanels(Panel2, Panel1));
        two_Next.onClick.AddListener(() => SwitchPanels(Panel2, Panel3));
    }

    // Generalized method to handle panel switching
    private void SwitchPanels(GameObject panelToDeactivate, GameObject panelToActivate)
    {
        if (panelToDeactivate != null)
        {
            panelToDeactivate.SetActive(false);
        }

        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true);
        }
    }

    // Called when this GameObject is disabled
    private void OnDisable()
    {
        // Deactivate Panel3 and activate Panel1 when this GameObject is disabled
        if (Panel3 != null)
        {
            Panel3.SetActive(false);
        }

        if (Panel1 != null)
        {
            Panel1.SetActive(true);
        }
    }
}
