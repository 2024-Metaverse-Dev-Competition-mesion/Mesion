using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagWindController : MonoBehaviour
{
    public Transform flagTransform; // ����� Transform
    public Vector3 windDirection = new Vector3(1, 0, 0); // �ٶ��� ����
    public float windStrength = 5.0f; // �ٶ��� ����
    public float swayAmount = 15.0f; // ����� ��鸲 ����
    public float swaySpeed = 2.0f;   // ����� ��鸲 �ӵ�

    void Update()
    {
        // �ٶ��� ���⿡ ���� ����� ȸ�� ����
        Quaternion targetRotation = Quaternion.LookRotation(windDirection);

        // �ٶ��� �������� õõ�� ȸ����Ű��
        flagTransform.rotation = Quaternion.Slerp(flagTransform.rotation, targetRotation, Time.deltaTime * windStrength);

        // ����� ��鸲 �߰� (���� �Լ��� �̿��� �ڿ������� ��鸲 ȿ��)
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

        // ��鸲�� y�� ȸ���� �߰�
        flagTransform.rotation *= Quaternion.Euler(0, sway, 0);
    }
}