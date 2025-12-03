using UnityEngine;

public class Healthpack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) //If the player picks up a health pack, heal them. Healthpacks will disappear only if it heals
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(other.gameObject.GetComponent<Player>().Heal()) //Always calls the heal function but return value determines if the healthpack is actually used
            {
                Destroy(gameObject);
            }
        }
    }
}
