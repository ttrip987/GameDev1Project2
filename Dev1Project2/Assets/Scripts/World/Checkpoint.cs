using JetBrains.Annotations;
using UnityEditor.Rendering;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool activated = false;
    public Vector3 checkpointSpawn;

    private void Awake()
    {
        checkpointSpawn = transform.position + new Vector3(0f, 2f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            activated = true;
        }
    }
}
