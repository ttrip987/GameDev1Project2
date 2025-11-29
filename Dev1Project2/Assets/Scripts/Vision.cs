using System.IO;
using System.Xml;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public bool sees_player = false;
    private GameObject entity;
    private GameObject player;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            entity = gameObject;
            player = other.gameObject;

            Vector3 direction = new Vector3(player.transform.position.x - entity.transform.position.x, player.transform.position.y - entity.transform.position.y, 0);
            direction = direction.normalized;
            Debug.DrawRay(entity.transform.position, direction * 15f, Color.yellow);
            if (Physics.Raycast(entity.transform.position, direction, out RaycastHit hitInfo, 100f, LayerMask.GetMask("Player", "Default")))
            {
                if(hitInfo.collider.tag == "Player")
                {
                    sees_player = true;
                }
                else
                {
                    sees_player = false;
                }
            }
        }
        else
        {
            sees_player = false;
        }
        
    }

}
