using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialTextController : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public string[] tutorialSteps;
    private int currentStep = 0;

    public Button leftArrowButton;
    public Button rightArrowButton;

    public void PreviousStep()
    {
        if (currentStep > 0)
        {
			UpdateButtonVisibility();
			currentStep--;
            UpdateTutorialText();
		}
    }

    public void NextStep()
    {
        if (currentStep < tutorialSteps.Length - 1)
        {
            currentStep++;
			UpdateTutorialText();
            UpdateButtonVisibility();
		}
    }

	void Start()
	{
		if (currentStep == 0)
		{
			leftArrowButton.gameObject.SetActive(false);
		}

		tutorialSteps = new string[]
	    {
		    "드론 실기 시험 \n튜토리얼에 오신 여러분을 환영합니다.",
		    "이제부터 드론 실기 시험의 순서에 맞춰 \n튜토리얼이 진행됩니다."
	    };

		UpdateTutorialText();
	}

    private void UpdateTutorialText()
    {
        tutorialText.text = tutorialSteps[currentStep];
    }

    private void UpdateButtonVisibility()
    {
        leftArrowButton.gameObject.SetActive(currentStep > 0);
        rightArrowButton.gameObject.SetActive(currentStep < tutorialSteps.Length - 1);

		// 디버깅 메시지
		Debug.Log("Current Step: " + currentStep);
		Debug.Log("Left Button Active: " + leftArrowButton.gameObject.activeSelf);
		Debug.Log("Right Button Active: " + rightArrowButton.gameObject.activeSelf);
	}
}
