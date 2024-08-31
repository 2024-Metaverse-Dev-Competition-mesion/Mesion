using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSystemController : MonoBehaviour
{
	public Vector3 windDirection = new Vector3(-1, 0, 0);  // 바람의 방향
	public float windStrength = 5.0f;  // 바람의 세기

	// 드론과 깃발 오브젝트
	public Transform droneTransform;
	public Cloth flagCloth;

	private bool isGPSModeActive = false;  // GPS 모드 플래그

	void Update()
	{
		// T 키를 눌렀을 때 GPS 모드를 전환
		if (Input.GetKeyDown(KeyCode.T))
		{
			isGPSModeActive = !isGPSModeActive;  // GPS 모드 활성화/비활성화 전환
			Debug.Log("GPS 모드: " + (isGPSModeActive ? "활성화" : "비활성화"));
		}

		// GPS 모드가 활성화된 경우에만 바람의 힘을 적용
		if (isGPSModeActive)
		{
			ApplyWindEffect();
		}
	}

	// 바람의 영향을 적용하는 함수
	void ApplyWindEffect()
	{
		// 1. 드론에 바람의 힘 적용
		Rigidbody droneRigidbody = droneTransform.GetComponent<Rigidbody>();
		if (droneRigidbody != null)
		{
			Vector3 windForce = windDirection.normalized * windStrength;
			droneRigidbody.AddForce(windForce);
		}

		// 2. 깃발에 바람의 힘 적용 (External Acceleration)
		if (flagCloth != null)
		{
			flagCloth.externalAcceleration = windDirection.normalized * windStrength;
		}
	}
}
