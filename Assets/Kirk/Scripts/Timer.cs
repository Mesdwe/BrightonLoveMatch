// KHOGDEN
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        if(!pauseTime)
        {
            if(time > 0f)
                time -= Time.deltaTime;
            else if(time < 0f)
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
}