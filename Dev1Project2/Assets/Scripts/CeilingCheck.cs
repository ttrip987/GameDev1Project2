using UnityEngine;

public class CeilingCheck : MonoBehaviour
{
    public bool hit_ceiling;
    private void OnTriggerEnter(Collider other)
    {
        hit_ceiling = true;
    }
    private void OnTriggerExit(Collider other)
    {
        hit_ceiling = false;
    }
}
