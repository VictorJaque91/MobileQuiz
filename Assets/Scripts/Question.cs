using UnityEngine;

[System.Serializable]
public class Question
{
    public Sprite imageReference;
    public string correctAnswer;
    public string[] wrongAnswers;

    public bool IsValid()
    {
        if (imageReference == null)
        {
            Debug.LogWarning("Ingen bild referens hittades för frågan.");
            return false;
        }
        if (string.IsNullOrEmpty(correctAnswer))
        {
            Debug.LogWarning("Ingen korrekt svar hittades för frågan.");
            return false;
        }

        if (wrongAnswers == null || wrongAnswers.Length != 3)
        {
            Debug.LogWarning("Fel svar antal är inte korrekt.");
            return false;
        }
        foreach (var wrongAnswer in wrongAnswers)
        {
            if (string.IsNullOrEmpty(wrongAnswer))
            {
                Debug.LogWarning("Ingen fel svar hittades för frågan.");
                return false;
            }
            if (wrongAnswer == correctAnswer)
            {
                Debug.LogWarning("Fel svar är samma som korrekt svar.");
                return false;
            }
        }
        return true;
    }
}
