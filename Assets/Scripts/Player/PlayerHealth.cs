using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int healthPoints = 5;
    int currentHP;

    [SerializeField] RectTransform healthBar;
    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] GameObject gameOverText;

    private void Awake()
    {
        currentHP = healthPoints;
        for( int i = 0; i < healthPoints; i++)
        {
            Instantiate(healthBarPrefab, healthBar.position, healthBar.rotation, healthBar);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Destroy(healthBar.GetChild(0).gameObject);

        if ( currentHP <= 0)
        {
            gameOverText.SetActive(true);
            Destroy(gameObject);
        }
    }
}
