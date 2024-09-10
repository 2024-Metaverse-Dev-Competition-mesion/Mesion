using UnityEngine;

public class HomeButton_Manager : MonoBehaviour
{
    public GameObject First_Mode_Select;
    public GameObject Mode_Select;
    public GameObject Practice_Mode_Select;
    public GameObject Quiz_Panel;
    public GameObject Test_Panel;
    public GameObject Test_Quiz_Panel;
    public GameObject Quiz_History_Store;
    public GameObject QuizManager_Part;
    public GameObject QuizManager_Random;
    public GameObject failureSound;
    public GameObject correctSound;
    public GameObject wrongSound;
    public GameObject cheersSound;

    // Method to handle button click and manage object activation/deactivation
    public void OnButtonClick()
    {
        // Activate specific objects
        First_Mode_Select.SetActive(true);
        QuizManager_Part.SetActive(true);
        QuizManager_Random.SetActive(true);

        // Deactivate other objects
        Mode_Select.SetActive(false);
        Practice_Mode_Select.SetActive(false);
        Quiz_Panel.SetActive(false);
        Test_Panel.SetActive(false);
        Test_Quiz_Panel.SetActive(false);
        Quiz_History_Store.SetActive(false);
        failureSound.SetActive(false);
        correctSound.SetActive(false);
        wrongSound.SetActive(false);
        cheersSound.SetActive(false);
    }
}