using UnityEngine;

public class Practice_Mode_Select_Manager : MonoBehaviour
{
    // Reference to the other GameObject you want to activate
    public GameObject objectToActivate;
    public GameObject ObjectToDeactivate;
    public GameObject Quiz_Random;
    public GameObject Quiz_Part;
    public GameObject Test_Quiz;
    public GameObject Part_Text;

    public void OnButtonClick()
    {
        Quiz_Part.SetActive(false);
        Test_Quiz.SetActive(false);
        ObjectToDeactivate.SetActive(false);
        objectToActivate.SetActive(true);
    }

    public void OnButtonClick2()
    {
        Quiz_Random.SetActive(false);
        Part_Text.SetActive(false);
        Test_Quiz.SetActive(false);
        ObjectToDeactivate.SetActive(false);
        objectToActivate.SetActive(true);
    }
}