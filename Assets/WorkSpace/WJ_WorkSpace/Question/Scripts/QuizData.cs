using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuiz", menuName = "Quiz/QuizData")]
public class QuizData : ScriptableObject
{
    public List<Question> questions;   // 질문 리스트
}