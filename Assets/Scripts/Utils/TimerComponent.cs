using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerComponent : MonoBehaviour
{
    public IEnumerator Timer(float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator StartTimer(float duration)
    {
        yield return StartCoroutine(Timer(duration));
    }
}
