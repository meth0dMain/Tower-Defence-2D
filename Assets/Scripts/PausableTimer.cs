using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PausableTimer : MonoBehaviour
{
    public float Timer { get; private set; }
    protected void Start()
    {
        ResetTimer();
    }
    
    private void Update()
    {
        Timer += Time.deltaTime;
    }

    protected void ResetTimer()
    {
        Timer = 0.0f;
    }

    protected IEnumerator WaitForTime(float timeToWait)
    {
        var timer = 0.0f;
        while (timer <= timeToWait)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
