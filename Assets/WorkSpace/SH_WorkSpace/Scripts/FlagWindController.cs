using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagWindController : MonoBehaviour
{
    public Transform flagTransform; // 깃발의 Transform
    public Vector3 windDirection = new Vector3(1, 0, 0); // 바람의 방향
    public float windStrength = 5.0f; // 바람의 세기
    public float swayAmount = 15.0f; // 깃발의 흔들림 정도
    public float swaySpeed = 2.0f;   // 깃발의 흔들림 속도

    void Update()
    {
        // 바람의 방향에 따라 깃발의 회전 설정
        Quaternion targetRotation = Quaternion.LookRotation(windDirection);

        // 바람의 방향으로 천천히 회전시키기
        flagTransform.rotation = Quaternion.Slerp(flagTransform.rotation, targetRotation, Time.deltaTime * windStrength);

        // 깃발의 흔들림 추가 (사인 함수를 이용해 자연스러운 흔들림 효과)
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

        // 흔들림을 y축 회전에 추가
        flagTransform.rotation *= Quaternion.Euler(0, sway, 0);
    }
}