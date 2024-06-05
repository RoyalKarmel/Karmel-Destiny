using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        int hours = (int)(currentTime / 3600);
        int minutes = (int)((currentTime % 3600) / 60);
        int seconds = (int)(currentTime % 60);

        string gameTime = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        SetTimerText(gameTime);
    }

    void SetTimerText(string gameTime)
    {
        timerText.text = gameTime;
    }
}
