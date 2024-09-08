using UnityEngine;
using TMPro; // TextMeshPro 관련 네임스페이스 추가
using System.Collections; // IEnumerator를 사용하기 위해 추가

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // TextMeshProUGUI 컴포넌트 연결
    public GameObject Test_Panel; // 비활성화할 게임 오브젝트
    public GameObject Test_Quiz; // 활성화할 게임 오브젝트
    public GameObject QuizManager_Part;
    public GameObject QuizManager_Random;

    private int initialCountdownValue = 5; // 카운트다운 초기값
    private Coroutine countdownCoroutine; // 카운트다운 코루틴을 추적하기 위한 변수

    void Start()
    {
        // 카운트다운 시작
        countdownCoroutine = StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        int countdownValue = initialCountdownValue;
        
        while (countdownValue > 0)
        {
            countdownText.text = countdownValue + "초 후 시험을 시작합니다\n\n준비해주세요"; // TextMeshPro의 텍스트 업데이트
            yield return new WaitForSeconds(1f); // 1초 대기
            countdownValue--; // 카운트 감소
        }

        countdownText.text = "시험을 시작합니다.";  // 카운트다운 종료 후 메시지

        yield return new WaitForSeconds(3f); // 3초 대기 후

        Test_Panel.SetActive(false);
        Test_Quiz.SetActive(true);
        QuizManager_Part.SetActive(false);
        QuizManager_Random.SetActive(false);
    }

    // 이 스크립트가 속한 GameObject가 비활성화될 때 초기화
    private void OnDisable()
    {
        // 코루틴이 진행 중이면 중지
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

        // GameObject 상태를 초기화하지 않고 카운트다운 텍스트만 초기화
        countdownText.text = initialCountdownValue + "초 후 시험을 시작합니다\n\n준비해주세요";
    }

    // GameObject가 다시 활성화될 때 초기화가 필요하다면 OnEnable에서 처리 가능
    private void OnEnable()
    {
        // 필요한 경우 활성화될 때 초기화 작업을 추가
        countdownCoroutine = StartCoroutine(CountdownCoroutine());
    }
}