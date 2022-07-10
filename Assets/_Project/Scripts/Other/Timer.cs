using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeLabel;

    private float _timer;
    
    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            UpdateTimerDisplay(_timer);
        }
        else
        {
            ResetTimer();
        }
    }

    public void Init(float duration)
    {
        _timer = duration;
    }

    public void ResetTimer()
    {
        _timer = 0;
        timeLabel.text = "0";
    }

    private void UpdateTimerDisplay(float time)
    {
        //float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = $"{seconds:00}";
        timeLabel.text = currentTime;
    }
}