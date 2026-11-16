using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;
    PlayerController player;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        player = FindFirstObjectByType<PlayerController>(); 
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player HIT");
        }
    }

    private void FixedUpdate()
    {
        LookForPlayer();
    }

    void LookForPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        rb.linearVelocity = direction.normalized * moveSpeed ;
    }
}
