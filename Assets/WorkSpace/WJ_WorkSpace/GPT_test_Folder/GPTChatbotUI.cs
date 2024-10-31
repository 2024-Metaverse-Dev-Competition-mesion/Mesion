using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GPTChatbotUI : MonoBehaviour
{
    public GPTChatbot gptChatbot;
    public TMP_InputField inputField;
    public TMP_Text responseText;
    public Button[] choiceButtons;
    public Button arrowLeftButton; // New button for simulating "A" key
    public Button arrowRightButton; // New button for simulating "D" key

    private Coroutine displayCoroutine; 
    private string[] sentences; 
    private int currentSentenceIndex = 0; 

    private bool isProcessingInput = false; 
    private int keyPressCount = 0; 

    private void Start()
    {
        foreach (Button button in choiceButtons)
        {
            if (button != null)
            {
                Button localButton = button;
                localButton.onClick.AddListener(() => OnChoiceButtonClicked(localButton));
            }
        }

        // Add listeners for the new arrow buttons
        arrowLeftButton.onClick.AddListener(OnArrowLeftButtonClicked);
        arrowRightButton.onClick.AddListener(OnArrowRightButtonClicked);

        arrowLeftButton.gameObject.SetActive(false);
        arrowRightButton.gameObject.SetActive(false);
    }

    /*    
    public void OnSendButtonClicked()
    {
        if (!isProcessingInput)
        {
            string userInput = inputField.text;
            Debug.Log("Send Button Clicked. User Input: " + userInput);
            isProcessingInput = true; 
            gptChatbot.SendInitialRequest(userInput);

            SetChoiceButtonsActive(false);
        }
    }
    */

    public void OnChoiceButtonClicked(Button clickedButton)
    {
        if (!isProcessingInput)
        {
            string choiceText = clickedButton.GetComponentInChildren<TMP_Text>().text;
            Debug.Log("Button clicked with text: " + choiceText);
            isProcessingInput = true; 
            gptChatbot.SendFollowUpRequest(choiceText);

            SetChoiceButtonsActive(false);
            arrowRightButton.gameObject.SetActive(true);

            arrowLeftButton.gameObject.SetActive(false);
        }
    }

    public void DisplayGPTResponse(string gptResponse)
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }
        sentences = gptResponse.Split(new[] { '.' }, System.StringSplitOptions.RemoveEmptyEntries);
        currentSentenceIndex = 0;
        keyPressCount = 0; 
        displayCoroutine = StartCoroutine(DisplayResponseCoroutine());
        isProcessingInput = false; 
    }

    private IEnumerator DisplayResponseCoroutine()
    {
        while (currentSentenceIndex < sentences.Length)
        {
            responseText.text = sentences[currentSentenceIndex].Trim() + ".";
            yield return null; // Wait until the buttons are clicked
        }
    }

    private void OnArrowLeftButtonClicked()
    {
        if (currentSentenceIndex > 0)
        {
            currentSentenceIndex--;
            keyPressCount--;

            responseText.text = sentences[currentSentenceIndex].Trim() + ".";

            if (keyPressCount < sentences.Length - 1)
            {
                SetChoiceButtonsActive(false);
            }
            UpdateArrowButtonStates();
        }
    }

    private void OnArrowRightButtonClicked()
    {
        if (currentSentenceIndex < sentences.Length - 1)
        {
            currentSentenceIndex++;
            keyPressCount++;

            responseText.text = sentences[currentSentenceIndex].Trim() + ".";

            if (keyPressCount == sentences.Length - 1)
            {
                SetChoiceButtonsActive(true);
            }
            UpdateArrowButtonStates();
        }
    }

    private void UpdateArrowButtonStates()
    {
        // Enable or disable arrow buttons based on keyPressCount
        arrowLeftButton.gameObject.SetActive(keyPressCount > 0);
        arrowRightButton.gameObject.SetActive(keyPressCount < sentences.Length - 1);
    }

    private void SetChoiceButtonsActive(bool isActive)
    {
        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(isActive);
        }
    }

    public void DisplayChoices(string[] choices)
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Length)
            {
                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = choices[i];
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
