using System;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField] float dayDuration;
    [SerializeField] float currentTime;
    
    [SerializeField] bool isOvertime ;
    [SerializeField] bool isDayCompleted;
    
    [SerializeField] int enemiesRemaining;
    [SerializeField] bool isSpawningFinished;

    public event Action OnOvertimeStarted;
    public event Action OnDayCompleted;
    public event Action<float> OnTimerUpdated;

    public bool IsOvertime => isOvertime && !isDayCompleted;
    public bool IsDayCompleted => isDayCompleted;
    public void InitializeDay()
    {
        currentTime = dayDuration;
        isOvertime = false;
        isDayCompleted = false;
        enemiesRemaining = 0;
        isSpawningFinished = false;
    }

    private void Start()
    {
        InitializeDay();
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
       if(!isOvertime && !isDayCompleted)
       {
            currentTime = Mathf.Max(0, currentTime - Time.deltaTime);
            OnTimerUpdated?.Invoke(currentTime);

            if (currentTime <= 0 && enemiesRemaining > 0)
            {
                currentTime = 0f;
                
                isOvertime = true;
                OnOvertimeStarted?.Invoke();
            }
       }
       else if (isOvertime && !isDayCompleted)
       {
            currentTime += Time.deltaTime;
            OnTimerUpdated?.Invoke(currentTime);    
       }

        
    }

    public void SetTotalEnemies(int amount)
    {
        enemiesRemaining = Mathf.Max(0,amount);
    }

    public void RegisterEnemyDeath()
    {
        if (isDayCompleted) return;
        enemiesRemaining = Mathf.Max(0, enemiesRemaining - 1);
       

        TryCompleteDay();
    }

    public void SetSpawningFinished()
    {
        isSpawningFinished = true;
        TryCompleteDay();
    }

    public void TryCompleteDay()
    {
        if(isSpawningFinished && enemiesRemaining == 0 && !isDayCompleted)
        {
            isDayCompleted = true;
            OnDayCompleted?.Invoke();
        }
    }
}
