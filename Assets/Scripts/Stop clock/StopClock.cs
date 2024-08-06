using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopClock : MonoBehaviour
{
    private float elapsedTime = 0f;

    private bool clockRunning = false;

    public Action clockStopedEvent;

    private void Update()
    {
        if (clockRunning == true)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    private void ToggleClock(bool onOff)
    {
        clockRunning = onOff;
    }

    private void ResetClock()
    {
        ToggleClock(false);
        elapsedTime = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ClockStartTag>() != null)
        {
            ToggleClock(true);
        }

        else if(other.GetComponent<ClockStopTag>() != null)
        {
            Debug.Log("Stopping clock.");
            Debug.Log("Time: " + elapsedTime);
            clockStopedEvent?.Invoke();
            ResetClock();
        }
    }


}
