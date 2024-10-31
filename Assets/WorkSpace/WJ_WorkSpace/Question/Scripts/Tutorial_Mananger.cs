using UnityEngine;
using TMPro; // TextMeshPro 사용
using UnityEngine.UI;
using System.Collections;

public class Tutorial_Manager : MonoBehaviour
{
    public GameObject Tutorial_Panel; // Tutorial 패널
    public GameObject First_Mode_Select; // Mode Select 패널
    public TextMeshProUGUI initialTextMeshPro; // 처음 3초간 보여줄 TextMeshPro
    public TextMeshProUGUI functionalTextMeshPro; // 그 이후에 사용할 TextMeshPro
    public Button nextButton; // 다음 버튼
    public Button prevButton; // 이전 버튼
    public TextMeshProUGUI nextButtonTextMeshPro; // Next 버튼의 TextMeshPro (문구 변경용)

    private string[] messages = { "필기 모드에 오신 걸 환영합니다.", "퀴즈 모드와 퀴즈 모드에서 푼 문제를 저장하는 문제 기록이 있습니다." }; // 보여줄 메시지 배열
    private int currentMessageIndex = 0; // 현재 메시지 인덱스
    private int nextButtonClickCount = 0; // 다음 버튼 클릭 횟수
    private int prevButtonClickCount = 0; // 이전 버튼 클릭 횟수

    void Start()
    {
        // 처음 시작할 때
        initialTextMeshPro.gameObject.SetActive(true); // 처음 3초 텍스트 보여주기
        functionalTextMeshPro.gameObject.SetActive(false); // 나머지 텍스트는 숨김
        prevButton.gameObject.SetActive(false); // 이전 버튼 비활성화
        nextButton.gameObject.SetActive(false); // 다음 버튼도 비활성화

        // 첫 텍스트 보여주기 및 코루틴 시작
        StartCoroutine(ShowInitialTextSequence());
        
        // 버튼 클릭 이벤트 추가
        nextButton.onClick.AddListener(OnNextButtonClick);
        prevButton.onClick.AddListener(OnPrevButtonClick);
    }

    // 처음 텍스트를 3초간 보여주는 코루틴
    IEnumerator ShowInitialTextSequence()
    {
        // 3초 동안 첫 번째 텍스트를 보여줌
        yield return new WaitForSeconds(3f);

        // 첫 텍스트 숨기고, functionalTextMeshPro 활성화
        initialTextMeshPro.gameObject.SetActive(false);
        functionalTextMeshPro.gameObject.SetActive(true);

        // functionalTextMeshPro가 활성화되면서 첫 번째 메시지 출력 및 버튼 활성화
        functionalTextMeshPro.text = messages[currentMessageIndex];
        
        // 다음 버튼 활성화
        nextButton.gameObject.SetActive(true);
    }

    // 다음 버튼 클릭 시 호출되는 함수
    void OnNextButtonClick()
    {
        // 클릭 횟수 증가
        nextButtonClickCount++;

        // 메시지 배열 범위를 넘지 않도록 설정
        if (currentMessageIndex < messages.Length - 1)
        {
            currentMessageIndex++;
            functionalTextMeshPro.text = messages[currentMessageIndex];
            prevButton.gameObject.SetActive(true); // 이전 버튼 활성화
        }

        // 버튼 상태 업데이트
        UpdateButtonStates();

        // 마지막 메시지에 도달했을 때 (다음 버튼 클릭 횟수와 이전 버튼 클릭 횟수의 차이가 텍스트 배열 크기 - 1일 때)
        if (nextButtonClickCount - prevButtonClickCount == messages.Length - 1)
        {
            nextButtonTextMeshPro.text = "이동하기"; // Next 버튼의 텍스트를 "이동하기"로 변경
        }

        // 마지막 메시지에 도달한 후 다시 클릭하면 기능 수행
        if (nextButtonClickCount - prevButtonClickCount == messages.Length)
        {
            OnButtonClick(); // 기존 기능 실행
        }
    }

    // 이전 버튼 클릭 시 호출되는 함수
    void OnPrevButtonClick()
    {
        // 클릭 횟수 증가
        prevButtonClickCount++;

        // 메시지 배열 범위를 넘지 않도록 설정
        if (currentMessageIndex > 0)
        {
            currentMessageIndex--;
            functionalTextMeshPro.text = messages[currentMessageIndex];
        }

        // 버튼 상태 업데이트
        UpdateButtonStates();

        // 마지막 메시지에서 벗어났을 때 버튼 텍스트를 원래대로 돌림 (다시 차이가 messages.Length - 1보다 작을 때)
        if (nextButtonClickCount - prevButtonClickCount < messages.Length - 1)
        {
            nextButtonTextMeshPro.text = "다음"; // Next 버튼의 텍스트를 "다음"으로 변경
        }
    }

    // 버튼 상태 업데이트 함수
    void UpdateButtonStates()
    {
        // 다음 버튼과 이전 버튼의 클릭 횟수를 비교
        if (nextButtonClickCount == prevButtonClickCount)
        {
            prevButton.gameObject.SetActive(false); // 이전 버튼 비활성화
        }
        else
        {
            prevButton.gameObject.SetActive(true); // 이전 버튼 활성화
        }

        // 클릭 차이가 설정된 텍스트의 개수와 같으면 다음 버튼 기존 기능을 사용
        if (nextButtonClickCount - prevButtonClickCount == messages.Length)
        {
            nextButton.gameObject.SetActive(false); // 다음 버튼 비활성화
        }
        else
        {
            nextButton.gameObject.SetActive(true); // 다음 버튼 활성화
        }
    }

    // 기존 onButtonClick 기능 (Tutorial_Panel 비활성화, First_Mode_Select 활성화)
    void OnButtonClick()
    {
        Tutorial_Panel.SetActive(false);
        First_Mode_Select.SetActive(true);
    }
}
