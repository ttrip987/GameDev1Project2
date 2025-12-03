using UnityEngine;

public class BruteState : State
{
    protected Brute brute;

    private void Awake()
    {
        brute = transform.root.GetComponent<Brute>();
    }
}
