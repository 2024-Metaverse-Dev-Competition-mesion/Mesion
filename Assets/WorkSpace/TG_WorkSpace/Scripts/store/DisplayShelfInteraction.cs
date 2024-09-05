using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DisplayShelfInteraction : MonoBehaviour
{
    public GameObject uiPanel;  // UI 패널 참조 (유니티 에디터에서 설정)
    public XRRayInteractor rayInteractor;  // XR Ray Interactor 참조 (유니티 에디터에서 설정)
    public LayerMask interactionLayerMask;  // 상호작용할 레이어 설정

    private bool isUiActive = false;

    void Start()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);  // 초기에는 UI 패널 비활성화
        }

        // Ray Interactor에 SelectEntered와 SelectExited 이벤트 리스너 등록
        if (rayInteractor != null)
        {
            rayInteractor.selectEntered.AddListener(OnRayHoverEnter);
            rayInteractor.selectExited.AddListener(OnRayHoverExit);
        }
    }

    // Ray Interactor가 상호작용 가능한 오브젝트에 닿으면 호출
    public void OnRayHoverEnter(SelectEnterEventArgs args)
    {
        // 해당 오브젝트가 상호작용할 수 있는 레이어에 있는지 확인
        if (args.interactableObject.transform.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            if (!isUiActive)
            {
                if (uiPanel != null)
                {
                    uiPanel.SetActive(true);  // UI 활성화
                    Debug.Log("UI 패널이 활성화되었습니다.");
                    isUiActive = true;
                }
            }
        }
    }

    // Ray Interactor가 상호작용 가능한 오브젝트에서 벗어나면 호출
    public void OnRayHoverExit(SelectExitEventArgs args)
    {
        if (isUiActive)
        {
            if (uiPanel != null)
            {
                uiPanel.SetActive(false);  // UI 비활성화
                Debug.Log("UI 패널이 비활성화되었습니다.");
                isUiActive = false;
            }
        }
    }

    private void OnDestroy()
    {
        // 이벤트 리스너 해제
        if (rayInteractor != null)
        {
            rayInteractor.selectEntered.RemoveListener(OnRayHoverEnter);
            rayInteractor.selectExited.RemoveListener(OnRayHoverExit);
        }
    }
}
