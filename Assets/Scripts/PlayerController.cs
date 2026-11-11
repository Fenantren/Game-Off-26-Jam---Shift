using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    
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
    }

    
    void MovePlayer()
    {
        rb.linearVelocity = new Vector2(input.x * moveSpeed, input.y * moveSpeed);
    }
    
    
    public void Move(InputAction.CallbackContext context)
    {
        input = new Vector2(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
    }


}
