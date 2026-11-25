using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    [SerializeField] int valueAmount = 5;
    [SerializeField] float moveSpeed = 5f;
    RunManager runManager;
    PlayerController player;


    private void Awake()
    {
        runManager = RunManager.Instance;
    }
    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    public void Initialize(int amount)
    {
        valueAmount = amount;
    }
    private void Update()
    {
        MoveTowardsPlayer();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (runManager != null)
            {
                runManager.AddMoney(valueAmount);
                Destroy(gameObject);

            }
        }
    }

    void MoveTowardsPlayer()
    {
        float step = moveSpeed * Time.deltaTime;
        if (player == null) return;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    }
}
