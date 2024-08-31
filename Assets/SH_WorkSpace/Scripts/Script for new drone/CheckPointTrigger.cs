using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
	public CheckpointController checkpointController;  // CheckpointController ����

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Drone"))  // ����� üũ����Ʈ�� �������� ��
		{
			// ���� üũ����Ʈ�� ��Ȱ��ȭ�ϰ� ���� üũ����Ʈ�� Ȱ��ȭ
			checkpointController.OnCheckpointReached();

			// ���� üũ����Ʈ�� �� �̻� �ʿ� �����Ƿ� ��Ȱ��ȭ
			gameObject.SetActive(false);
		}
	}
}
