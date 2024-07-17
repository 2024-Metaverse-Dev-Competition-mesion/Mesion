using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
	public float health = 100f; // 불의 초기 체력
	public float extinguishRate = 10f; // 물방울에 맞을 때 감소할 체력량
	public float extinguishTime = 2f; // 불이 완전히 꺼지기까지의 시간

	private bool isExtinguishing = false;
	private ParticleSystem fireParticle;

	void Start()
	{
		fireParticle = GetComponent<ParticleSystem>();
		if (fireParticle == null)
		{
			Debug.LogError("Particle System not found on this GameObject");
		}
	}

	public void ApplyWater()
	{
		if (!isExtinguishing)
		{
			health -= extinguishRate;
			Debug.Log("Fire health: " + health);

			if (health <= 0f)
			{
				StartCoroutine(Extinguish());
			}
		}
	}

	private IEnumerator Extinguish()
	{
		isExtinguishing = true;
		float elapsedTime = 0f;

		while (elapsedTime < extinguishTime)
		{
			float t = elapsedTime / extinguishTime;
			fireParticle.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		fireParticle.Stop();
		gameObject.SetActive(false);
		isExtinguishing = false;
		Debug.Log("Fire Extinguished");
	}
}
