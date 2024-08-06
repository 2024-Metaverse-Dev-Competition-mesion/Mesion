using UnityEngine;
using UnityEngine.UI; // UI 컴포넌트 사용을 위해 필요

public class ButtonClickHandler : MonoBehaviour
{
    // Button 컴포넌트에 대한 참조
    public Button myButton;

    void Start()
    {
        // 버튼이 할당되었는지 확인
        if (myButton == null)
        {
            Debug.LogError("Button reference is not assigned.");
            return;
        }

        // 버튼의 onClick 이벤트에 리스너 추가
        myButton.onClick.AddListener(OnButtonClick);
    }

    // 버튼이 클릭되었을 때 호출될 메서드
    void OnButtonClick()
    {
        Debug.Log("Button clicked!");
    }
}
