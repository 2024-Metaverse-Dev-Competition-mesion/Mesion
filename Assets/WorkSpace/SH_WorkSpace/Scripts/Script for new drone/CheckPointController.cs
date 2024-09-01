using UnityEngine;

public class CheckpointController : MonoBehaviour
{
	public GameObject[] checkpoints;  // üũ����Ʈ ��ϵ��� �迭
	private int currentCheckpointIndex = 0;  // ���� Ȱ��ȭ�� üũ����Ʈ �ε���

	void Start()
	{
		// ��� üũ����Ʈ�� ��Ȱ��ȭ ���·� ����
		foreach (GameObject checkpoint in checkpoints)
		{
			checkpoint.SetActive(false);
		}

		// ù ��° üũ����Ʈ Ȱ��ȭ
		ActivateCheckpoint(currentCheckpointIndex);
	}

	// Ư�� �ε����� üũ����Ʈ Ȱ��ȭ
	void ActivateCheckpoint(int index)
	{
		if (index >= 0 && index < checkpoints.Length)
		{
			checkpoints[index].SetActive(true);
		}
	}

	// ���� üũ����Ʈ�� ��Ȱ��ȭ�ϰ�, ���� üũ����Ʈ Ȱ��ȭ
	public void OnCheckpointReached()
	{
		// ���� üũ����Ʈ ��Ȱ��ȭ
		checkpoints[currentCheckpointIndex].SetActive(false);

		// ���� üũ����Ʈ�� �ִ� ��� ���� üũ����Ʈ Ȱ��ȭ
		if (currentCheckpointIndex < checkpoints.Length - 1)
		{
			currentCheckpointIndex++;
			ActivateCheckpoint(currentCheckpointIndex);
		}
	}
}
