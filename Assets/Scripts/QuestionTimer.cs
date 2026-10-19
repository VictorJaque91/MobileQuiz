using UnityEngine;

public class QuestionTimer
{
    private float startTime;
    private float frozenTime;
    private float timeLimit;
    private bool isRunning;
    public QuestionTimer(float timeLimit)
    {
        this.timeLimit = timeLimit;
        frozenTime = 0f;
        isRunning = false;
    }
    public void StartTimer()
    {
        startTime = Time.time;
        frozenTime = 0f;
        isRunning = true;
    }
    public void StopTimer()
    {
        if (!isRunning)
        {
            Debug.LogWarning("The timer is not running. Cannot stop the timer.");
            return;
        }
        frozenTime = Time.time - startTime;
        isRunning = false;
    }
    public float GetElapsedTime()
    {
        if (isRunning)
        {
            return Time.time - startTime;
        }
        return frozenTime;
    }
    public void ResetTimer()
    {
        frozenTime = 0f;
        startTime = 0f;
        isRunning = false;
    }
    public bool IsTimeUp()
    {
        return GetElapsedTime() >= timeLimit;
    }
}

