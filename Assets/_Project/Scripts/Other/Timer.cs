using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeLabel;

    private float _timerDuration;
    
    private void Update()
    {
        if (_timerDuration > 0)
        {
            _timerDuration -= Time.deltaTime;
            UpdateTimerDisplay(_timerDuration);
        }
        else
        {
            _timerDuration = 0;
            timeLabel.text = "00:00";
        }
    }

    public void Init(float duration)
    {
        _timerDuration = duration;
    }

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = $"{minutes:00}{seconds:00}";
        timeLabel.text = currentTime;
    }
}