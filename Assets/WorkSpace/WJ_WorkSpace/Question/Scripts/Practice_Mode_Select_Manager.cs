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

    // Wrapper methods with no parameters for Button "On Click" event
    public void ActivateQuizPart()
    {
        OnButtonClick(Quiz_Part, Test_Quiz);
    }

    public void DeactivateRandomQuiz()
    {
        OnButtonClick2(Quiz_Random, Part_Text, Test_Quiz);
    }

    // The original methods you wrote
    public void OnButtonClick(GameObject quizPart, GameObject testQuiz)
    {
        quizPart.SetActive(false);
        testQuiz.SetActive(false);
        ObjectToDeactivate.SetActive(false);
        objectToActivate.SetActive(true);
    }

    public void OnButtonClick2(GameObject quizRandom, GameObject partText, GameObject testQuiz)
    {
        quizRandom.SetActive(false);
        partText.SetActive(false);
        testQuiz.SetActive(false);
        ObjectToDeactivate.SetActive(false);
        objectToActivate.SetActive(true);
    }
}
