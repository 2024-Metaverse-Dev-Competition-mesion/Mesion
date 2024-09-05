using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRTriggerUI : MonoBehaviour
{
    public GameObject uiPanel; // UI 패널을 참조
    public XROrigin xrOrigin;  // XR Origin을 참조

    // 콜라이더에 들어갔을 때 호출되는 함수
    private void OnTriggerEnter(Collider other)
    {
        // XR Origin이 콜라이더에 들어간 경우
        if (other.gameObject == xrOrigin.gameObject)
        {
            // UI 활성화
            uiPanel.SetActive(true);
            // 로그 출력
            Debug.Log("XR Origin이 콜라이더에 진입했습니다. UI를 활성화합니다.");
        }
    }

    // 콜라이더에서 나왔을 때 호출되는 함수
    private void OnTriggerExit(Collider other)
    {
        // XR Origin이 콜라이더에서 나간 경우
        if (other.gameObject == xrOrigin.gameObject)
        {
            // UI 비활성화
            uiPanel.SetActive(false);
            // 로그 출력
            Debug.Log("XR Origin이 콜라이더에서 나갔습니다. UI를 비활성화합니다.");
        }
    }
}
