using UnityEngine;
using UnityEngine.UI;

public class ObjectPurchase : MonoBehaviour
{
    public GameObject uiPanel; // ModalSingleButton 오브젝트 (UI 패널)
    public GameObject objectToReplaceChildWith; // 구매 후 자식 오브젝트를 대체할 오브젝트

    private Button purchaseButton; // 구매 버튼
    private Collider objectCollider; // 현재 오브젝트의 콜리더

    void Start()
{
    if (uiPanel == null)
    {
        Debug.LogError("uiPanel is not assigned in the inspector");
        return;
    }

    Transform buttonTransform = uiPanel.transform.Find("TextButton");
    if (buttonTransform == null)
    {
        Debug.LogError("TextButton not found under uiPanel");
        return;
    }

    purchaseButton = buttonTransform.GetComponent<Button>();
    if (purchaseButton == null)
    {
        Debug.LogError("Button component not found on TextButton");
        return;
    }

    purchaseButton.onClick.AddListener(OnPurchase);

    uiPanel.SetActive(false);

    objectCollider = GetComponent<Collider>();
    if (objectCollider == null)
    {
        Debug.LogError("Collider component not found on the GameObject");
    }
}


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 들어오면
        {
            // UI 활성화
            uiPanel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 나가면
        {
            // UI 비활성화
            uiPanel.SetActive(false);
        }
    }

    void OnPurchase()
    {
        // 현재 오브젝트의 첫 번째 자식을 가져옵니다
        Transform childTransform = transform.GetChild(0);
        
        // 자식 오브젝트 교체
        if (objectToReplaceChildWith != null && childTransform != null)
        {
            // 새로운 오브젝트를 자식 오브젝트로 교체
            Instantiate(objectToReplaceChildWith, childTransform.position, childTransform.rotation, transform);
            Destroy(childTransform.gameObject);
        }

        // UI 비활성화
        uiPanel.SetActive(false);

        // 콜리더 비활성화
        if (objectCollider != null)
        {
            objectCollider.enabled = false;
        }
    }
}
