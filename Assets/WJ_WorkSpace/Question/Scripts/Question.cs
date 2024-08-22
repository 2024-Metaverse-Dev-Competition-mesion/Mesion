using System;

[Serializable]
public class Question
{
    public string questionText;        // 질문 텍스트
    public string[] options;           // 선택지 배열 (위에서부터 선택지 1 ~ 4번)
    public int correctAnswerIndex;     // 정답 인덱스 (1번 ~ 4번 = 0 ~ 3)
    public int part;                   // 질문이 속한 파트 (0: 항공 법규, 1: 항공 기상, 2: 비행이론 및 응용)
}