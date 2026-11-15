using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField] int healthPoints = 3;
    [SerializeField] float currentHP;
    
    [SerializeField] Transform heartBar;
    [SerializeField] GameObject heartUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        currentHP = healthPoints;
        for (int i = 0;  i < healthPoints; i++)
        {
            Instantiate(heartUI, heartBar.position, heartBar.rotation, heartBar);
        }
        

    }
   

    public void TakeDamage (int damage)
    {
        currentHP -= damage;
        Destroy(heartBar.GetChild(0).gameObject);

        if(currentHP <=0)
        {
            //VFX + SFX added here for death animations
            //Reduce the enemy count here 
            Destroy(gameObject);

        }
    }
}
