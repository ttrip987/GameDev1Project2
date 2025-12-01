using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (other.gameObject.name != "Vision")
            { 
                other.gameObject.GetComponent<Enemy>().TakeDamage();
            }
        }
    }
}
