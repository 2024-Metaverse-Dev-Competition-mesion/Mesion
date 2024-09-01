using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneController : MonoBehaviour
{
	public WindZone windZone;
	public float maxWindStrength = 10f;

	void Update()
	{
		// 바람의 세기와 방향을 동적으로 변화시키고 싶을 때
		windZone.windMain = Mathf.PingPong(Time.time, maxWindStrength);
	}
}