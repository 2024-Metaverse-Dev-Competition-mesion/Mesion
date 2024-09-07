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
    

    void Start()
    {
        // 카운트다운 시작
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        int countdownValue = 5;
        
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
}