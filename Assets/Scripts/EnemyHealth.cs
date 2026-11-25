using UnityEngine;



public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Health Variables")]
    [SerializeField] int healthPoints = 3;
    [SerializeField] int currentHP;
    
    [SerializeField] Transform heartBar;
    [SerializeField] GameObject heartUI;
    [SerializeField] bool isDead = false;

    [Header("VFX and Money Prefab")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] GameObject moneyPrefab;

    [Header("Money values")]
    [SerializeField] private int normalMoneyValue;
    [SerializeField] private float overtimeMultiplier;

    private DayManager dayManager;

    private void Awake()
    {
        currentHP = healthPoints;
        
        for (int i = 0;  i < healthPoints; i++)
        {
            Instantiate(heartUI, heartBar.position, heartBar.rotation, heartBar);
        }

    }

    public void Initialize(DayManager dayManagerScript)
    {
            

        dayManager = dayManagerScript;
        
        Debug.Log(dayManager == null
         ? "Enemy received NULL DayManager"
         : "Enemy successfully received DayManager");
    }
    

    public void TakeDamage (int damage)
    {
        if (isDead) 
            return;
        if(damage <=0) return;  

        currentHP -= damage;
        Destroy(heartBar.GetChild(0).gameObject);

        if(currentHP <=0)
        {
            isDead = true;
            Instantiate(explosionVFX,transform.position,Quaternion.identity);
            // SFX added here for death animations
            int moneyAmount;

            if (dayManager.IsOvertime)
            {
                moneyAmount = Mathf.RoundToInt(normalMoneyValue * overtimeMultiplier);
            }
            else moneyAmount = normalMoneyValue;

            Debug.Log("Overtime: " + dayManager.IsOvertime);
            Debug.Log("Money reward: " + moneyAmount);

            GameObject money = Instantiate(moneyPrefab, transform.position, Quaternion.identity);
            MoneyScript moneyScript = money.GetComponent<MoneyScript>();
            if (moneyScript != null)
            {
                moneyScript.Initialize(moneyAmount);
            }
            else
            {
                Debug.LogError("Money prefab is missing MoneyScript!");
            }

            dayManager.RegisterEnemyDeath();
            
            Destroy(gameObject);

        }

        
    }


}
