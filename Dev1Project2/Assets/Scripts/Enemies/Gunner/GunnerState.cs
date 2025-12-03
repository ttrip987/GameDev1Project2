using UnityEngine;

public class GunnerState : State
{
    protected Gunner gunner;
    void Awake()
    {
        gunner = transform.root.GetComponent<Gunner>();
    }

}
