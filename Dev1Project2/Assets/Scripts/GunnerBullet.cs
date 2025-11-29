using Unity.VisualScripting;
using UnityEngine;

public class GunnerBullet : MonoBehaviour
{
    //Component References
    private Rigidbody rb;

    //Bullet Movement
    private float bullet_speed = 8f;
    private Vector3 direction = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private float bullet_life = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveBullet();
        bullet_life -= Time.fixedDeltaTime;
        if(bullet_life < 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetBullet(Vector3 start_position, Vector3 shoot_direction)
    {
        transform.position = start_position;

        direction = shoot_direction; //Sets the direction of the bullet as the shoot direction passed in

        transform.up = direction; //Automatically sets the "upwards" direction as the direction of the bullet
    }

    public void MoveBullet()
    {
        velocity = direction * bullet_speed * Time.fixedDeltaTime;
        Vector3 newPos = transform.position + velocity;
        rb.MovePosition(newPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
        
    }
}
