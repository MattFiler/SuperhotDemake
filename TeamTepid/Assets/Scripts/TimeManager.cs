using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    private float pauseGauge = 1.0f;
    private float realDeltaTimeCumulative = 0.0f;
    
    //These values work backwards, so 1.0f is stopped, 0.0f is full speed
    private float minPauseGauge = 0.95f; 
    private float maxPauseGauge = 0.0f;

    /* Keep track of player input and handle time speedup/down */
    void Update()
    {
        //Reulate update to be the same as FixedUpdate (can't use FixedUpdate due to time alterations)
        realDeltaTimeCumulative += Time.unscaledDeltaTime;
        if (realDeltaTimeCumulative >= Time.fixedDeltaTime)
        {
            realDeltaTimeCumulative = 0.0f;
        }
        else
        {
            return;
        }

        //Decrease the pause gauge when a movement direction is pressed
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            if (horizontal <= 0) horizontal *= -1;
            if (vertical <= 0) vertical *= -1;
            pauseGauge -= horizontal + vertical;
        }
        else
        {
            pauseGauge += 0.1f;
        }
        if (pauseGauge <= maxPauseGauge)
        {
            pauseGauge = maxPauseGauge;
        }
        else if (pauseGauge >= minPauseGauge)
        {
            pauseGauge = minPauseGauge;
        }

#if UNITY_EDITOR
        //If in editor, allow keyboard controls (UNITY BUG!!)
        if (horizontal == 0 && vertical == 0)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                pauseGauge = maxPauseGauge;
            }
            else
            {
                pauseGauge = minPauseGauge;
            }
        }
#endif

        //Change game time to match pause gauge
        Time.timeScale = (pauseGauge - 1) * -1;
    }

    /* Force jump to max game speed */
    public void SetMaxSpeed()
    {
        pauseGauge = 1.0f;
    }
}
