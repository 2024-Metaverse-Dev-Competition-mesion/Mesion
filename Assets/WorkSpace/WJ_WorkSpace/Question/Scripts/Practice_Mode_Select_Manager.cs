using UnityEngine;
using UnityEngine.UI;  // Add this for Button component

public class Practice_Mode_Select_Manager : MonoBehaviour
{
    // References to the GameObjects you want to activate and deactivate
    public GameObject objectToActivate;
    public GameObject ObjectToDeactivate;
    public GameObject Quiz_Random;
    public GameObject Quiz_Part;
    public GameObject Test_Quiz;
    public GameObject Part_Text;

    // References to the buttons
    public Button Random;
    public Button Regulations;
    public Button Meteorological;
    public Button Theory_and_Applications;

    void Start()
    {
        // Assign button click event listeners
        Random.onClick.AddListener(() => OnButtonClick(Quiz_Part, Test_Quiz));
        Regulations.onClick.AddListener(() => OnButtonClick2(Quiz_Random, Part_Text, Test_Quiz));
        Meteorological.onClick.AddListener(() => OnButtonClick2(Quiz_Random, Part_Text, Test_Quiz));
        Theory_and_Applications.onClick.AddListener(() => OnButtonClick2(Quiz_Random, Part_Text, Test_Quiz));
    }

    private void OnButtonClick(GameObject quizPart, GameObject testQuiz)
    {
        quizPart.SetActive(false);
        testQuiz.SetActive(false);
        ObjectToDeactivate.SetActive(false);
        objectToActivate.SetActive(true);
    }

    // Generalized method to handle activation/deactivation for button2
    private void OnButtonClick2(GameObject quizRandom, GameObject partText, GameObject testQuiz)
    {
        quizRandom.SetActive(false);
        partText.SetActive(false);
        testQuiz.SetActive(false);
        ObjectToDeactivate.SetActive(false);
        objectToActivate.SetActive(true);
    }
}
