using UnityEngine;

public static class ScoreCalculator
{
    public static int CalculateScore(bool isCorrect, float elapsedSeconds, float timeLimit)
    {
        if (timeLimit <= 0)
        {
            return 0;
        }
        if (!isCorrect)
        {
            return 0;
        }
        if (elapsedSeconds > timeLimit)
        {
            return 0;
        }
        float points = 10 + 90 * Mathf.Max(0, timeLimit - elapsedSeconds) / timeLimit;
        return Mathf.RoundToInt(points);
    }
}
