
using UnityEngine;
using UnityEngine.InputSystem;

public class GunAim : MonoBehaviour
{
    [Header("Offsets")]
    [SerializeField] Vector2 gunLeftOffset;
    [SerializeField] Vector2 gunRightOffset;
    [SerializeField] Vector2 shoulderOffset = new Vector2(0.15f, 0.05f);
    

    [Header("Sprites")]
    [SerializeField] SpriteRenderer gunSprite;
    [SerializeField] SpriteRenderer bodySprite;
    [SerializeField] SpriteRenderer armSprite;
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
        Vector2 offset = shoulderOffset;

        if(aimingLeft) 
        {
            offset.x = -shoulderOffset.x;
            gunSprite.flipY = true;
            armSprite.flipY = true;
            bodySprite.flipX = true;

            gunSprite.transform.localPosition = gunLeftOffset;
            
        }
        else
        {
            gunSprite.flipY = false;
            armSprite.flipY = false;
            bodySprite.flipX = false;

            gunSprite.transform.localPosition = gunRightOffset;
        }
        
        transform.localPosition = offset;
    }

    
}
