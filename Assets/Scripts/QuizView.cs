using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizView : MonoBehaviour
{
    [SerializeField]
    private Image questionImage;
    [SerializeField]
    private Button[] answerButtons;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text timerText;
    [SerializeField]
    private TMP_Text questionText;
    private string[] currentAnswers;

    public void DisplayQuestion(Question question, string[] answers)
    {
        if (answers.Length != answerButtons.Length)
        {
            Debug.LogError($"Expected {answerButtons.Length} answers but got {answers.Length}.");
            return;
        }
        questionImage.sprite = question.imageReference;
        currentAnswers = answers;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TMP_Text buttonText = answerButtons[i].GetComponentInChildren<TMP_Text>();
            buttonText.text = answers[i];

        }
    }
    public void DisplayResult(int score)
    {

    }
    private void Awake()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].onClick.AddListener(() => OnAnswerClicked(index));

        }
    }
    private void OnAnswerClicked(int index)
    {

    }
    public void UpdateTimer(int secondsLeft)
    {
        timerText.text = secondsLeft.ToString();
    }
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
