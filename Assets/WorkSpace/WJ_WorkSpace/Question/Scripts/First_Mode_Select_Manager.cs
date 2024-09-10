using UnityEngine;
using UnityEngine.UI;  // Add this for Button component

public class First_Mode_Select_Manager : MonoBehaviour
{
    // Reference to the GameObjects you want to activate and deactivate
    public GameObject objectToActivate;
    public GameObject objectToActivate2;
    public GameObject objectToDeactivate;

    // References to the buttons
    public Button Quiz_Mode;
    public Button Quiz_History;

    void Start()
    {
        Quiz_Mode.onClick.AddListener(() => OnButtonClick(objectToActivate));
        Quiz_History.onClick.AddListener(() => OnButtonClick(objectToActivate2));
    }

    // Generalized method to handle activation/deactivation
    private void OnButtonClick(GameObject objectToActivate)
    {
        // Deactivate the specified GameObject
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }

        // Activate the specified GameObject
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}