using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Component References
    public Rigidbody rb;
    public MeshRenderer rend;
    public Vision vision;

    //Movement
    public float move_speed; //Set different for each enemy
    public ColliderCheck left_check;
    public ColliderCheck right_check;

    //Health
    public int maxHits;  //Set different for each enemy
    private int currentHits = 0;
    public bool isDead = false;

    //Invulnerability
    public bool invulnerability = false;
    public float invulnerability_timer = 0.5f;
    protected Color last_color;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<MeshRenderer>();
    }


    void FixedUpdate()
    {
        if (invulnerability)
        {
            rend.material.color = new Color(135, 206, 235); //Set invulnerability color
            invulnerability_timer -= Time.fixedDeltaTime; //Start invulnerable timer
            if (invulnerability_timer <= 0f)
            {
                invulnerability = false; //Reset invulnerability and timer, and reset the color of the model
                invulnerability_timer = 1f;
                rend.material.color = last_color;
            }
        }
    }

    public void TakeDamage()
    {
        if (!invulnerability)
        {
            currentHits++;
            invulnerability = true;
            last_color = rend.material.color;

            if (currentHits >= maxHits)
            {
                Die();
            }
        }
    }

    public virtual void Die() //Kills an enemy
    {
        isDead = true;
        GetComponent<Collider>().enabled = false;

        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player")) //Damage the player on touch
        {
            if (!isDead) //No damage on collision if the enemy is dead
            {
                collision.collider.gameObject.GetComponent<Player>().TakeDamage();
            }
        }
    }
}
