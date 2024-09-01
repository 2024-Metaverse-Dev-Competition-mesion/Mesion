using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    public Vector3 windDirection = new Vector3(1, 0, 0);
    public float windStrength = 5.0f;

	void FixedUpdate()
	{
		// 모든 드론 객체들에 대해 바람의 힘을 가합니다
		GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");

		foreach (GameObject drone in drones)
		{
			Rigidbody rb = drone.GetComponent<Rigidbody>();
			if (rb != null)
			{
				Vector3 windForce = windDirection * windStrength;
				rb.AddForce(windForce);
			}
		}
	}
}
