using UnityEngine;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    [SerializeField] int healthPoints = 5;
    int currentHP;
    
    //Invurnability 
    [SerializeField] float invFrame = 1f;
    float invTimer;

    [SerializeField] RectTransform healthBar;
    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] GameObject gameOverText;

    [SerializeField] SpriteRenderer playerBody;

    private void Awake()
    {
        currentHP = healthPoints;
        invTimer = invFrame;
        for( int i = 0; i < healthPoints; i++)
        {
            Instantiate(healthBarPrefab, healthBar.position, healthBar.rotation, healthBar);
        }
    }

    private void Update()
    {
        invTimer += Time.deltaTime;

        InvulnurabilityFrames();
    }

    public void TakeDamage(int damage)
    {   
        if(invTimer >= invFrame)
        {
            currentHP -= damage;
            Destroy(healthBar.GetChild(0).gameObject);
            invTimer = 0f;
            
            /*(if (currentHP <= 0)
            {
                gameOverText.SetActive(true);
                Destroy(gameObject);
            }*/
        }
        
        
    }

    void InvulnurabilityFrames()
    {
        if( invTimer < invFrame)
        {
            Color tempColor = playerBody.color;

            tempColor.a = Mathf.PingPong(invTimer * 8f, 1f);
            playerBody.color = tempColor;
        }
        else
        {
            Color visible = playerBody.color;
            visible.a = 1;
            playerBody.color = visible;

        }

    }
}
