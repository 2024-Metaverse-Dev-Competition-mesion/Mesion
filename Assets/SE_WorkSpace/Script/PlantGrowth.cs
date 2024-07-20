using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    public GameObject[] growthStages; // �� ���� �ܰ踦 ��Ÿ���� ������ �迭
    public float timeBetweenStages = 5.0f; // �� ���� �ܰ� ������ �ð� ����

    private int currentStage = 0; // ���� ���� �ܰ�
    private float timeSinceLastStage = 0.0f; // ������ ���� �ܰ� ���� ��� �ð�

    void Start()
    {
        if (growthStages.Length > 0)
        {
            Instantiate(growthStages[currentStage], transform.position, Quaternion.identity, transform);
        }
    }

    void Update()
    {
        timeSinceLastStage += Time.deltaTime;

        if (timeSinceLastStage >= timeBetweenStages && currentStage < growthStages.Length - 1)
        {
            // ���� ���� �ܰ�� ��ȯ
            currentStage++;
            timeSinceLastStage = 0.0f;

            // ���� ������ ���� �� ���ο� ������ ����
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            Instantiate(growthStages[currentStage], transform.position, Quaternion.identity, transform);
        }
    }
}
