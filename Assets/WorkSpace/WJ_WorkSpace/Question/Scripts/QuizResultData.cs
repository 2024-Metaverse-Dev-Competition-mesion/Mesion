using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuizResult", menuName = "Quiz/QuizResultData")]
public class QuizResultData : ScriptableObject
{
    public List<QuizResult> quizResults; // 퀴즈 결과 리스트
}

[Serializable]
public class QuizResult
{
    public string quizTitle;               // 퀴즈 제목 또는 ID
    public float totalTime;                // 퀴즈에 소요된 시간
    public int totalScore;                 // 최종 점수
    public List<QuestionResult> results;   // 각 질문의 결과 리스트
}

[Serializable]
public class QuestionResult
{
    public string questionText;            // 질문 텍스트
    public string selectedAnswerText;      // 사용자가 선택한 답변의 텍스트
    public int selectedAnswerIndex;        // 사용자가 선택한 답변의 인덱스
    public bool isCorrect;                 // 정답 여부
    public string correctAnswerText;       // 정답 텍스트 (오답일 경우 보여줌)
    public int part;                       // 질문이 속한 파트
}