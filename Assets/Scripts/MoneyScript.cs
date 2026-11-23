using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    [SerializeField] int valueAmount = 5;
    [SerializeField] float moveSpeed = 5f;
    GameManager gameManager;
    PlayerController player;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        player = FindFirstObjectByType<PlayerController>();
        
    }
    private void Update()
    {
        MoveTowardsPlayer();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.AdjustMoneyCounter(valueAmount);
            Destroy(gameObject);
        }
    }

    void MoveTowardsPlayer()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    }
}
