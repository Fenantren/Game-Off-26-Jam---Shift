using UnityEngine;



public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Health Variables")]
    [SerializeField] int healthPoints = 3;
    [SerializeField] int currentHP;
    
    [SerializeField] Transform heartBar;
    [SerializeField] GameObject heartUI;
    [SerializeField] bool isDead = false;

    [SerializeField] GameObject explosionVFX;
    [SerializeField] GameObject moneyPrefab;
    GameManager gameManager;

    private void Awake()
    {
        currentHP = healthPoints;
        
        for (int i = 0;  i < healthPoints; i++)
        {
            Instantiate(heartUI, heartBar.position, heartBar.rotation, heartBar);
        }

    }

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.AdjustEnemiesLeft(1);
    }



    public void TakeDamage (int damage)
    {
        if (isDead) return;
        
            currentHP -= damage;
            Destroy(heartBar.GetChild(0).gameObject);

            if(currentHP <=0)
            {
                Instantiate(explosionVFX,transform.position,Quaternion.identity);
                Instantiate(moneyPrefab,transform.position, Quaternion.identity);
                isDead = true;
                Debug.Log("is Dead");
                gameManager.AdjustEnemiesLeft(-1);
                //VFX + SFX added here for death animations
            
                Destroy(gameObject);

            }

        
    }


}
