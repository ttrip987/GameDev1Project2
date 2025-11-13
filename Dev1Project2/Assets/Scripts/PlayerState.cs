using UnityEngine;

public class PlayerState : State
{
    protected Player player;
    void Awake()
    {
        player = transform.root.GetComponent<Player>();
    }
}
