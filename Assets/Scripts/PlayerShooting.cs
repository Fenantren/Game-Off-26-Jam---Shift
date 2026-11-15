using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform muzzleTransform;

    [SerializeField] float shotCooldown = 2f;
    [SerializeField] float shotTimer;

    bool mShooting = false;

    private void Start()
    {
        shotTimer = shotCooldown;
    }
    private void Update()
    {
        if (mShooting && shotTimer >= shotCooldown)
        {
            Fire();
            shotTimer = 0;
            
        }
        shotTimer += Time.deltaTime;
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        mShooting = context.ReadValueAsButton();
        Debug.Log(context.phase);



    }

    void Fire()
    {
        Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
    }
}
