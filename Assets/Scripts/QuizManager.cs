using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField]
    private List<Question> questions;
    private int currentIndex;
    private int score;
    const float TimeLimitPerQuestion = 10f;
    private QuestionTimer questionTimer;
    private bool isQuestionActive;

    private void LoadQuestions()
    {
        int amountBefore = questions.Count;
        questions.RemoveAll(q => q == null || !q.IsValid());
        if (amountBefore != questions.Count)
        {
            Debug.LogWarning($"Removed {amountBefore - questions.Count} invalid questions.");
        }
    }
    public void StartQuiz()
    {
        LoadQuestions();
        if (questions.Count == 0)
        {
            Debug.LogError("No valid questions available to start the quiz.");
            return;
        }
        ShuffleUtility.Shuffle(questions);
        currentIndex = 0;
        score = 0;
        StartCurrentQuestion();
    }
    public void CheckAnswer(string answer)
    {
        if (!isQuestionActive)
        {
            return;
        }
        isQuestionActive = false;
        questionTimer.StopTimer();
        bool isCorrect = questions[currentIndex].correctAnswer == answer;
        score += ScoreCalculator.CalculateScore(isCorrect, questionTimer.GetElapsedTime(), TimeLimitPerQuestion);
        NextQuestion();
    }
    private void HandleTimeout()
    {
        isQuestionActive = false;
        questionTimer.StopTimer();
        NextQuestion();
    }

    private void NextQuestion()
    {
        currentIndex++;
        if (currentIndex >= questions.Count)
        {
            EndQuiz();
            return;
        }
        StartCurrentQuestion();
    }
    private void EndQuiz()
    {
        isQuestionActive = false;
        Debug.Log($"Quiz ended. Final score: {score}");
    }
    private void StartCurrentQuestion()
    {
        questionTimer.StartTimer();
        isQuestionActive = true;
        // TODO: Skicka till QuizView
        GetCurrentQuestionAnswers();
    }
    private string[] GetCurrentQuestionAnswers()
    {
        Question current = questions[currentIndex];
        string[] answers = new string[current.wrongAnswers.Length + 1];
        answers[0] = current.correctAnswer;
        for (int i = 0; i < current.wrongAnswers.Length; i++)
        {
            answers[i + 1] = current.wrongAnswers[i];
        }
        ShuffleUtility.Shuffle(answers);
        return answers;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questionTimer = new QuestionTimer(TimeLimitPerQuestion);
        StartQuiz();
    }

    // Update is called once per frame
    void Update()
    {
        if (isQuestionActive && questionTimer.IsTimeUp())
        {
            HandleTimeout();
        }
    }
}
