using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject panelA;
    public GameObject panelB;
    public GameObject panelC;

    // This method will be called when the Confirm button is clicked
    public void OnConfirm()
    {
        panelA.SetActive(false); // Deactivate Panel A
        panelB.SetActive(true);  // Activate Panel B
        panelC.SetActive(true);  // Activate Panel C
    }

    // This method will be called when the Cancel button is clicked
    public void OnCancel()
    {
        panelA.SetActive(false); // Deactivate Panel A
        // Panels B and C remain unchanged
    }
}
