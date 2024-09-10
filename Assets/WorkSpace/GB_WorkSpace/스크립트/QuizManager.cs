using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro ���� ���ӽ����̽� �߰�

public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel; // 4������ ��ư���� ���Ե� �г�
    public List<HighlightPart> droneParts; // ��� ��ǰ ����Ʈ
    public TextMeshProUGUI questionText; // ���� �ؽ�Ʈ
    public Button[] answerButtons; // 4������ ��ư�� (TextMeshProUGUI ���)
    private HighlightPart correctPart; // ���� ��ǰ
    public TextMeshProUGUI resultText; // ����/���� �޽��� �ؽ�Ʈ

    private List<int> randomOrder = new List<int>(); // ������ ��ǰ ����
    private int quizIndex = 0; // ���� ������ �ε���

    void Start()
    {
        quizPanel.SetActive(false); // ó���� �г��� �����
        resultText.gameObject.SetActive(false); // ó���� ��� �ؽ�Ʈ �����
    }

    // ���� ���� ��ư�� ������ ȣ��Ǵ� �Լ�
    public void StartQuiz()
    {
        quizPanel.SetActive(true); // �г��� Ȱ��ȭ�Ͽ� ���� ����
        InitializeQuiz();
        DisplayNextQuestion();
    }

    void InitializeQuiz()
    {
        // ��ǰ�� �������� ����
        for (int i = 0; i < droneParts.Count; i++)
        {
            randomOrder.Add(i);
        }

        // ������ ������ ����
        randomOrder.Shuffle(); // List�� ���� Ȯ�� �޼��带 ����� �� ����
    }

    public void DisplayNextQuestion()
    {
        if (quizIndex >= randomOrder.Count)
        {
            Debug.Log("���� �Ϸ�!");
            quizPanel.SetActive(false); // ���� �Ϸ� �� �г��� ����
            return;
        }

        // ���� ��ǰ ���� �� ����
        correctPart = droneParts[randomOrder[quizIndex]];
        correctPart.Highlight();

        // ���� ���� �ؽ�Ʈ ����
        questionText.text = "�� ��ǰ�� �̸��� �����ϱ��?";

        // 4������ ���� ����
        GenerateMultipleChoiceQuestion();

        quizIndex++;
    }

    void GenerateMultipleChoiceQuestion()
    {
        // ���� ��ư�� ���� ��ġ�� ��ġ
        int correctAnswerIndex = Random.Range(0, answerButtons.Length);
        answerButtons[correctAnswerIndex].GetComponentInChildren<TextMeshProUGUI>().text = correctPart.gameObject.name;

        // ���� ����
        List<int> remainingParts = new List<int>(randomOrder);
        remainingParts.Remove(randomOrder[quizIndex]); // ���� ����

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i == correctAnswerIndex) continue; // ������ �̹� ������

            int randomWrongIndex = Random.Range(0, remainingParts.Count);
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = droneParts[remainingParts[randomWrongIndex]].gameObject.name;
            remainingParts.RemoveAt(randomWrongIndex); // �ߺ� ������ ���� ����
        }
    }

    // ����ڰ� ���� �������� �� ȣ��
    public void OnAnswerSelected(int selectedIndex)
    {
        // ���� Ȯ��
        if (answerButtons[selectedIndex].GetComponentInChildren<TextMeshProUGUI>().text == correctPart.gameObject.name)
        {
            Debug.Log("����!");
            resultText.text = "����!";
            // ���� ó�� �� ���� ������ �Ѿ��
            correctPart.RemoveHighlight();
            DisplayNextQuestion();
        }
        else
        {
            Debug.Log("����! ������ " + correctPart.gameObject.name);
            resultText.text = "����! ������ " + correctPart.gameObject.name;
            correctPart.RemoveHighlight();
            DisplayNextQuestion(); // ���� �ÿ��� ���� ������ �Ѿ��
        }
    }
}
public static class ListExtensions
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
