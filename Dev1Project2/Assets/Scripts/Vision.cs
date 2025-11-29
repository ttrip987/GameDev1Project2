using System.IO;
using System.Xml;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public bool sees_player = false;
    private bool player_in_field = false;
    private Transform entity;
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            entity = transform.parent;
            player = other.gameObject;

            player_in_field = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player_in_field = false;
        }
    }

    private void FixedUpdate()
    {
        if (player_in_field)
        {
            Vector3 direction = new Vector3(player.transform.position.x - entity.transform.position.x, player.transform.position.y - entity.transform.position.y, 0);
            direction = direction.normalized;
            Debug.DrawRay(entity.transform.position, direction * 15f, Color.yellow);
            if (Physics.Raycast(entity.transform.position, direction, out RaycastHit hitInfo, 100f, LayerMask.GetMask("Player", "Default")))
            {
                if (hitInfo.collider.tag == "Player")
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
