using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public GameObject firePrefab;
    public Transform[] fireLocations;
    public float fireInterval = 5f;

    // Ȱ��ȭ�� �� ��ġ��
    private HashSet<Transform> activeFireLocations = new HashSet<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ActivateRandomFire", fireInterval, fireInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateRandomFire()
    {
        if (activeFireLocations.Count >= fireLocations.Length)
        {
            // ��� ��ġ�� ���� ���� ���� ���, �� �̻� ���� ���� ����
            return;
        }

        Transform fireLocation = null;
        do
        {
            int randomIndex = Random.Range(0, fireLocations.Length); // ���� �ε��� ����
            fireLocation = fireLocations[randomIndex];
        } while (activeFireLocations.Contains(fireLocation)); // �̹� ���� ���� ��ġ���� Ȯ��

        Instantiate(firePrefab, fireLocation.position, fireLocation.rotation); // �� ����
        activeFireLocations.Add(fireLocation); // Ȱ��ȭ�� �� ��ġ �߰�
    }
}
