using UnityEngine;
using System.Collections.Generic;

public class ClearRoomTrigger : MonoBehaviour
{
    public int enemy_count;
    private HashSet<GameObject> enemies = new HashSet<GameObject>();
    public bool room_cleared = false;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void Update()
    {
        enemies.RemoveWhere(enemy => enemy == null);

        enemy_count = enemies.Count;

        if(enemy_count == 0 )
        {
            room_cleared = true;
        }
        else
        {
            room_cleared = false;
        }
    }
}
