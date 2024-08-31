using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
	public CheckpointController checkpointController;  // CheckpointController 참조

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Drone"))  // 드론이 체크포인트에 도달했을 때
		{
			// 현재 체크포인트를 비활성화하고 다음 체크포인트를 활성화
			checkpointController.OnCheckpointReached();

			// 현재 체크포인트는 더 이상 필요 없으므로 비활성화
			gameObject.SetActive(false);
		}
	}
}
