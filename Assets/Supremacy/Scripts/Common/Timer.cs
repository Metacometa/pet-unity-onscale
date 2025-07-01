using UnityEngine;
using System.Collections;

public class Timer
{
    private float time;
    public bool isCompleted;

    public void StartTimer()
    {
        isCompleted = false;
        //StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        yield return null;

        isCompleted = true;
    }
}
