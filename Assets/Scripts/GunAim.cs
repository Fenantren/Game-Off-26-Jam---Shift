using UnityEngine;
using UnityEngine.InputSystem;

public class GunAim : MonoBehaviour
{
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
        cam.ScreenToWorldPoint(mouseScreenPos);
        
    }
}
