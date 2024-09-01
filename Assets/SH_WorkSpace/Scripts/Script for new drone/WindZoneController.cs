using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneController : MonoBehaviour
{
	public WindZone windZone;
	public float maxWindStrength = 10f;

	void Update()
	{
		// �ٶ��� ����� ������ �������� ��ȭ��Ű�� ���� ��
		windZone.windMain = Mathf.PingPong(Time.time, maxWindStrength);
	}
}