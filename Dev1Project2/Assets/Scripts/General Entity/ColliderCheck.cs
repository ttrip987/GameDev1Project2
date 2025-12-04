using UnityEngine;

public class ColliderCheck : MonoBehaviour
{
    public bool hit_collider;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("Enemy") && other.gameObject.tag != "Elevator" && other.gameObject.tag != "Checkpoint")
        {
            hit_collider = true;
        }
        else
        {
            hit_collider = false;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        hit_collider = false;
    }
}
