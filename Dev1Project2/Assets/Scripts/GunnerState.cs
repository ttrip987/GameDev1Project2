using UnityEngine;

public class GunnerState : MonoBehaviour
{
    protected Gunner gunner;
    void Awake()
    {
        gunner = transform.root.GetComponent<Gunner>();
    }

}
