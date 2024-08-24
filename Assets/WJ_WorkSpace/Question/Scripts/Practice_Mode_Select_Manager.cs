using UnityEngine;

public class Practice_Mode_Select_Manager : MonoBehaviour
{
    // Reference to the other GameObject you want to activate
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;
    public GameObject Quiz_Random;
    public GameObject Quiz_Part;
    public GameObject Part_Text;

    public void OnButtonClick()
    {
        Quiz_Part.SetActive(false);
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
        Quiz_Random.SetActive(false);
        Part_Text.SetActive(false);
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
}