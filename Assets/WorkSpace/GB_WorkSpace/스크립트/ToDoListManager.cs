using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro ���� ���ӽ����̽� �߰�

public class ToDoListManager : MonoBehaviour
{
    public List<HighlightPart> droneParts; // ��� ��ǰ ����Ʈ
    public List<GameObject> toDoItems; // ToDo ����Ʈ �׸�� (TextMeshProUGUI�� ���Ե� UI ������Ʈ)

    private int currentIndex = 0;
    private Color originalTextColor;

    void Start()
    {
        // ù ��° �׸� ���� ����
        HighlightNextPart();
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
        }
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