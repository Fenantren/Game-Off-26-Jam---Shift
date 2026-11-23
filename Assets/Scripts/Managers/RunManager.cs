using System;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    public static RunManager Instance { get; private set; }

    [Header("Run Progression")]
    [SerializeField] WorkDay currentDay = WorkDay.Monday;

    [Header("Currency")]
    [SerializeField] int money = 0;

    [Header("Burnout")]
    [SerializeField] float burnout = 0f;

    private bool runFinished;

    public WorkDay CurrentDay => currentDay;
    public int Money => money;
    public float Burnout => burnout;
    public bool RunFinished => runFinished;
    //Events
    public event Action<int> OnMoneyChanged;
    public event Action<float> OnBurnoutChanged;
    public event Action<WorkDay> OnDayChanged;
    public event Action OnRunFinished;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //CURRENCY
    public void AddMoney(int amount)
    {
        if (amount <= 0) return;
        money += amount;
        OnMoneyChanged?.Invoke(money);
    }
    public bool SpendMoney(int amount)
    {
        if (amount <= 0 || money < amount) return false;

        money -= amount;
        OnMoneyChanged?.Invoke(money);  
        return true;
    }

    //BURNOUT
    public void AddBurnout(int amount)
    {
        if (runFinished || amount <= 0) return;

        burnout = Mathf.Clamp(burnout + amount, 0f, 100f);
        OnBurnoutChanged?.Invoke(burnout);

        if (burnout >= 100f)
        {
            FinishRun();
        }
    }
    public void ReduceBurnout(int amount)
    {
        if (runFinished || amount <= 0) return;
        burnout = Mathf.Clamp(burnout - amount, 0f, 100f);
        OnBurnoutChanged?.Invoke(burnout);
    }

    //DAY PROGRESSION
    public void AdvanceDay()
    {
        if(runFinished) return;

        if(currentDay == WorkDay.Friday)
        {
            FinishRun();
            return;
        }

        currentDay++;
        OnDayChanged?.Invoke(currentDay);
    }

    //RUN COMPLETION 
    private void FinishRun()
    {
        if (RunFinished) return;

        runFinished = true;
        OnRunFinished?.Invoke();
        
    }
}
