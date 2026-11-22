using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    [SerializeField] int valueAmount = 5;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.AdjustMoneyCounter(valueAmount);
            Destroy(gameObject);
        }
    }
}
