
using UnityEngine;
using UnityEngine.InputSystem;

public class GunAim : MonoBehaviour
{
    [SerializeField] SpriteRenderer gunSprite;
    [SerializeField] SpriteRenderer bodySprite;
    [SerializeField] bool aimingLeft = false;
    Camera cam;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);
        Vector2 direction = mouseWorldPos - transform.position;
        float angle =  Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0, 0, angle);

        aimingLeft = direction.x < 0;

        if(aimingLeft)
        {
            gunSprite.flipY = true;
            bodySprite.flipX = true;
        }
        else
        {
            gunSprite.flipY = false;
            bodySprite.flipX = false;
        }
    }

    
}
