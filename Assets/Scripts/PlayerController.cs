using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    
    [SerializeField] bool isFacingRight = true;
    
    
    Rigidbody2D rb;
    Vector2 input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        MovePlayer();
        

        if (!isFacingRight && input.x > 0)
        {
            FlipSprite();
        }
        else if (isFacingRight && input.x < 0)
        {
            FlipSprite();
        }
    }

    
    void MovePlayer()
    {
        rb.linearVelocity = input * moveSpeed;

        
    }
    
    
    
    void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }


    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }


}
