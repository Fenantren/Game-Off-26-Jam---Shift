using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int damage = 1;

    [SerializeField] float castRadius = 0.3f;
    [SerializeField] float lookAhead = 1.5f;
    [SerializeField] LayerMask obstacleMask;

    float detourSide = 0f;
    Rigidbody2D rb;
    PlayerController player;
    PlayerHealth playerHealth;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        player = FindFirstObjectByType<PlayerController>(); 
        playerHealth = player.GetComponent<PlayerHealth>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Player HIT");
        }
    }

    private void FixedUpdate()
    {
        LookForPlayer();
    }

    void LookForPlayer()
    {
        if (player == null) { rb.linearVelocity = Vector2.zero; return; };

        Vector2 toPlayer = player.transform.position - rb.transform.position; 
        Vector2 direction = toPlayer .normalized;
        float castDistance = Mathf.Min(toPlayer.magnitude, lookAhead);

        RaycastHit2D hit = Physics2D.CircleCast(rb.position, castRadius, direction, castDistance, obstacleMask);

        if (hit.collider != null)
        {
            Vector2 along = Vector2.Perpendicular(hit.normal);

            if ( detourSide == 0f)
            {
                detourSide = Vector2.Dot(along, direction) >= 0f ? 1f : -1f; 
                
            }

            direction = along * detourSide;
        }
        else
        {
            detourSide = 0f; // path is clear
        }

        rb.linearVelocity = direction * moveSpeed;
    }
}
