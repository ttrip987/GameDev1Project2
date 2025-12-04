using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            door.SetActive(false);
        }
    }
}
