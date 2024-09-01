using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSystemController : MonoBehaviour
{
	public Vector3 windDirection = new Vector3(-1, 0, 0);  // �ٶ��� ����
	public float windStrength = 5.0f;  // �ٶ��� ����

	// ��а� ��� ������Ʈ
	public Transform droneTransform;
	public Cloth flagCloth;

	private bool isGPSModeActive = false;  // GPS ��� �÷���

	void Update()
	{
		// T Ű�� ������ �� GPS ��带 ��ȯ
		if (Input.GetKeyDown(KeyCode.T))
		{
			isGPSModeActive = !isGPSModeActive;  // GPS ��� Ȱ��ȭ/��Ȱ��ȭ ��ȯ
			Debug.Log("GPS ���: " + (isGPSModeActive ? "Ȱ��ȭ" : "��Ȱ��ȭ"));
		}

		// GPS ��尡 Ȱ��ȭ�� ��쿡�� �ٶ��� ���� ����
		if (isGPSModeActive)
		{
			ApplyWindEffect();
		}
	}

	// �ٶ��� ������ �����ϴ� �Լ�
	void ApplyWindEffect()
	{
		// 1. ��п� �ٶ��� �� ����
		Rigidbody droneRigidbody = droneTransform.GetComponent<Rigidbody>();
		if (droneRigidbody != null)
		{
			Vector3 windForce = windDirection.normalized * windStrength;
			droneRigidbody.AddForce(windForce);
		}

		// 2. ��߿� �ٶ��� �� ���� (External Acceleration)
		if (flagCloth != null)
		{
			flagCloth.externalAcceleration = windDirection.normalized * windStrength;
		}
	}
}
