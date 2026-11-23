using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Timer variables")]
    float currentTime;
    [SerializeField] float startSeconds;
    [SerializeField] TMP_Text currentTimeText;

    [SerializeField] bool overtimeState ;
    [SerializeField] bool roomCleared  ;
    public event Action OnOvertimeStarted;
    public event Action OnRoomCleared;

    [SerializeField] TMP_Text moneyText;
    [SerializeField] int money = 0;

    [Header("Enemies text")]
    [SerializeField] int enemiesLeft;
    [SerializeField] TMP_Text enemiesLeftText;

    public bool IsOvertime => overtimeState && !roomCleared;

    private void Start()
    {
        currentTime = startSeconds ;
        overtimeState = false ;
        roomCleared = false ;  
        moneyText.text = "Money : " + money.ToString();
    }
    private void Update()
    {
        Timer();
        
    }

    const string ENEMIES_LEFT_STRING = "Enemies Left : ";
    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();

        if (enemiesLeft <= 0 && !roomCleared)
        {
            roomCleared = true;
            currentTimeText.color = Color.green;
            Debug.Log("Room Cleared");
            OnRoomCleared?.Invoke();
        }


    }

    public void AdjustMoneyCounter(int amount)
    {
        money += amount;
        moneyText.text = "Money : " + money.ToString();  
    }

    private void Timer()
    {
        if (!overtimeState && !roomCleared)
        {
            currentTime -= Time.deltaTime;
            
            if(currentTime <= 0)
            {
                currentTime = 0f;
                overtimeState = true;
                OnOvertimeStarted?.Invoke();
            }
        }
        else if(! roomCleared)
        {
            currentTimeText.color = Color.red;
            currentTime += Time.deltaTime;
            
        }

        
        int totalSeconds = Mathf.CeilToInt(currentTime);
        if (overtimeState)
        {
            currentTimeText.text = "O.T. : " + (totalSeconds / 60) + ":" + (totalSeconds % 60).ToString("00");
        }
        else
        {
            currentTimeText.text = (totalSeconds / 60) + ":" + (totalSeconds % 60).ToString("00");
        }
           
    }

    
}
