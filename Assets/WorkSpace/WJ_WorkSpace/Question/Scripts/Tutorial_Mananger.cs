using UnityEngine;
using UnityEngine.UI; // Add this for UI elements like Button
using UnityEngine.SceneManagement; // This might be for future use, not needed for this task

public class Tutorial_Manager : MonoBehaviour
{
    public GameObject Tutorial_Panel; // The panel to deactivate
    public GameObject First_Mode_Select;   // The panel to activate
    public Button move_to_First; // The button to trigger the action

    void Start()
    {
        // Ensure the button triggers the OnButtonClick function when clicked
        move_to_First.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Deactivate the Tutorial Panel
        Tutorial_Panel.SetActive(false);

        // Activate the First Mode Select Panel
        First_Mode_Select.SetActive(true);
    }
}
