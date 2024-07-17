using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
	public float health = 100f; // ���� �ʱ� ü��
	public float extinguishRate = 10f; // ����￡ ���� �� ������ ü�·�
	public float extinguishTime = 2f; // ���� ������ ����������� �ð�

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
