using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRTriggerUI : MonoBehaviour
{
    public GameObject uiPanel; // UI �г��� ����
    public XROrigin xrOrigin;  // XR Origin�� ����

    // �ݶ��̴��� ���� �� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        // XR Origin�� �ݶ��̴��� �� ���
        if (other.gameObject == xrOrigin.gameObject)
        {
            // UI Ȱ��ȭ
            uiPanel.SetActive(true);
            // �α� ���
            Debug.Log("XR Origin�� �ݶ��̴��� �����߽��ϴ�. UI�� Ȱ��ȭ�մϴ�.");
        }
    }

    // �ݶ��̴����� ������ �� ȣ��Ǵ� �Լ�
    private void OnTriggerExit(Collider other)
    {
        // XR Origin�� �ݶ��̴����� ���� ���
        if (other.gameObject == xrOrigin.gameObject)
        {
            // UI ��Ȱ��ȭ
            uiPanel.SetActive(false);
            // �α� ���
            Debug.Log("XR Origin�� �ݶ��̴����� �������ϴ�. UI�� ��Ȱ��ȭ�մϴ�.");
        }
    }
}
