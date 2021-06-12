using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class UITimerScreen : UIScreen
{
    [Space(10)] public bool ApplyTimer;
    public float TimeToWait;
    public UnityEvent OnTimerEnd;
    
    public override void StartScreen()
    {
        base.StartScreen();
        
        if (ApplyTimer)
            StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSecondsRealtime(TimeToWait);
        OnTimerEnd?.Invoke();
    }
}
