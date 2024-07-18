using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public GameObject firePrefab;
    public Transform[] fireLocations;
    public float fireInterval = 5f;

    // 활성화된 불 위치들
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
            // 모든 위치에 불이 켜져 있을 경우, 더 이상 불을 켜지 않음
            return;
        }

        Transform fireLocation = null;
        do
        {
            int randomIndex = Random.Range(0, fireLocations.Length); // 랜덤 인덱스 선택
            fireLocation = fireLocations[randomIndex];
        } while (activeFireLocations.Contains(fireLocation)); // 이미 불이 켜진 위치인지 확인

        Instantiate(firePrefab, fireLocation.position, fireLocation.rotation); // 불 생성
        activeFireLocations.Add(fireLocation); // 활성화된 불 위치 추가
    }
}
