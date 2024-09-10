using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro ���� ���ӽ����̽� �߰�

public class ToDoListManager : MonoBehaviour
{
    public List<HighlightPart> droneParts; // ��� ��ǰ ����Ʈ
    public List<GameObject> toDoItems; // ToDo ����Ʈ �׸�� (TextMeshProUGUI�� ���Ե� UI ������Ʈ)
    public QuizManager quizManager; // ���� �Ŵ��� ����
    public Button quizStartButton; // ���� ���� ��ư
    public InteractionManager interactionManager; // InteractionManager ����

    private int currentIndex = 0;
    private Color originalTextColor;

    void Start()
    {
        quizStartButton.gameObject.SetActive(false); // ���� �� ��ư �����
        // ù ��° �׸� ���� ����
        HighlightNextPart();
    }

    // Ư�� ��ǰ�� Ŭ���Ǿ��� �� ȣ��
    public void OnPartClicked(HighlightPart clickedPart)
    {
        // Ŭ���� ��ǰ�� ���� ������ ��ǰ���� Ȯ��
        if (droneParts[currentIndex] == clickedPart)
        {
            CompleteCurrentItem();
        }
        else
        {
            Debug.Log("�߸��� ��ǰ�� Ŭ���Ǿ����ϴ�. ���� ������ ��ǰ�� Ŭ���ϼ���.");
        }
    }

    public void CompleteCurrentItem()
    {
        // ���� ��ǰ�� ���� ���� �� ToDo ����Ʈ �׸��� ���� ����
        if (currentIndex < droneParts.Count)
        {
            droneParts[currentIndex].RemoveHighlight();
            RestoreToDoItemColor(currentIndex);
            currentIndex++; // ���� ��ǰ���� �̵�
        }

        // ���� ��ǰ�� ������ ��� ����
        if (currentIndex < droneParts.Count)
        {
            HighlightNextPart();
        }
        else
        {
            Debug.Log("��� ��ǰ�� �Ϸ�Ǿ����ϴ�.");
            quizStartButton.gameObject.SetActive(true);
        }
    }
    public void OnQuizStartButtonClicked()
    {
        quizManager.StartQuiz(); // ���� ����
        quizStartButton.gameObject.SetActive(false); // ���� ���� �� ��ư �����
        interactionManager.enabled = false;
    }

    private void HighlightNextPart()
    {
        // ��� ��ǰ ����
        droneParts[currentIndex].Highlight();

        // ToDo ����Ʈ �׸��� �Ͼ������ ����
        ChangeToDoItemColor(currentIndex, Color.white);
    }

    private void ChangeToDoItemColor(int index, Color color)
    {
        // ToDo ����Ʈ �׸��� TextMeshProUGUI ������Ʈ ��������
        TextMeshProUGUI itemText = toDoItems[index].GetComponent<TextMeshProUGUI>();

        if (itemText != null)
        {
            originalTextColor = itemText.color; // ���� ���� ����
            itemText.color = color; // �Ͼ������ ����
        }
    }

    private void RestoreToDoItemColor(int index)
    {
        // ToDo ����Ʈ �׸��� TextMeshProUGUI ������Ʈ�� ������ ���� �������� ����
        TextMeshProUGUI itemText = toDoItems[index].GetComponent<TextMeshProUGUI>();

        if (itemText != null)
        {
            itemText.color = originalTextColor; // ���� �������� ����
        }
    }
}
