// KHOGDEN
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    [SerializeField] float time = 180f;
    [SerializeField] bool pauseTime = true;
    private float defaultTime;

    // KH - Called before 'void Start()'.
    void Awake()
    {
        instance = this;
        defaultTime = time;
    }

    // KH - Called upon every frame.
    void Update()
    {
        // KH - Decrease timer so long as it's not paused.
        if (!pauseTime)
        {
            if (time > 0f)
                time -= Time.deltaTime;
            else if (time < 0f)
                time = 0f;
        }
    }

    // KH - Reset the timer to be what it starts off by.
    public void ResetTime()
    {
        time = defaultTime;
    }

    public void PauseTime(bool pause)
    {
        pauseTime = pause;
    }

    public float GetTime()
    {
        return time;
    }

    // KH - Method to convert the time float into a readable time display.
    public string TimeDisplay()
    {
        int min = Mathf.FloorToInt(GetTime() / 60f);
        int sec = Mathf.FloorToInt(GetTime() % 60f);
        int msec = Mathf.FloorToInt(GetTime() * 1000f);
        msec = msec % 1000;
        return string.Format("{0:00}:{1:00}", min, sec);
    }
}