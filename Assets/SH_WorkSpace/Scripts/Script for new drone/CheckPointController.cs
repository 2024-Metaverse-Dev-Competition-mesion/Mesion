using UnityEngine;

public class CheckpointController : MonoBehaviour
{
	public GameObject[] checkpoints;  // 체크포인트 블록들의 배열
	private int currentCheckpointIndex = 0;  // 현재 활성화할 체크포인트 인덱스

	void Start()
	{
		// 모든 체크포인트를 비활성화 상태로 설정
		foreach (GameObject checkpoint in checkpoints)
		{
			checkpoint.SetActive(false);
		}

		// 첫 번째 체크포인트 활성화
		ActivateCheckpoint(currentCheckpointIndex);
	}

	// 특정 인덱스의 체크포인트 활성화
	void ActivateCheckpoint(int index)
	{
		if (index >= 0 && index < checkpoints.Length)
		{
			checkpoints[index].SetActive(true);
		}
	}

	// 현재 체크포인트를 비활성화하고, 다음 체크포인트 활성화
	public void OnCheckpointReached()
	{
		// 현재 체크포인트 비활성화
		checkpoints[currentCheckpointIndex].SetActive(false);

		// 다음 체크포인트가 있는 경우 다음 체크포인트 활성화
		if (currentCheckpointIndex < checkpoints.Length - 1)
		{
			currentCheckpointIndex++;
			ActivateCheckpoint(currentCheckpointIndex);
		}
	}
}
