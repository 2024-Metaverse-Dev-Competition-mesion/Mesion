using UnityEngine;

public class First_Mode_Select_Manager : MonoBehaviour
{
    // Reference to the other GameObject you want to activate
    public GameObject objectToActivate;
    public GameObject objectToActivate2;
    public GameObject objectToDeactivate;

    public void OnButtonClick()
    {
        // Deactivate the specified GameObject
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }

        // Activate the other GameObject
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }

    public void OnButtonClick2()
    {
        // Deactivate the specified GameObject
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }

        // Activate the other GameObject
        if (objectToActivate2 != null)
        {
            objectToActivate2.SetActive(true);
        }
    }
}
