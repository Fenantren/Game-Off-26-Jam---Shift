using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float bulletSpeed = 25f;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * bulletSpeed;
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
       
            Destroy(gameObject);
        }

        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        Collider2D colliderHit = collision.GetComponent<Collider2D>();

        enemyHealth?.TakeDamage(damage);
        Destroy(gameObject);
        //add SFX and VFX for a hit
        
    }
}
