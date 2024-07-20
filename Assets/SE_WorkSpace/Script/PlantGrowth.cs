using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    public GameObject[] growthStages; // 각 성장 단계를 나타내는 프리팹 배열
    public float timeBetweenStages = 5.0f; // 각 성장 단계 사이의 시간 간격

    private int currentStage = 0; // 현재 성장 단계
    private float timeSinceLastStage = 0.0f; // 마지막 성장 단계 이후 경과 시간

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
            // 다음 성장 단계로 전환
            currentStage++;
            timeSinceLastStage = 0.0f;

            // 기존 프리팹 삭제 및 새로운 프리팹 생성
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            Instantiate(growthStages[currentStage], transform.position, Quaternion.identity, transform);
        }
    }
}
