using System;

[Serializable]
public class Question
{
    public string questionText;        // 질문 텍스트
    public string[] options;           // 선택지 배열
    public int correctAnswerIndex;     // 정답 인덱스
    public int part;                   // 질문이 속한 파트 (0: Part 1, 1: Part 2, 2: Part 3)
}