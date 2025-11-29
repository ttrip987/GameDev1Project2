using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Component References
    public Camera cam;
    public Player player;

    //Dead Zone Boundaries, measured in screen pixels
    private float left_edge = Screen.width * 0.45f;
    private float right_edge = Screen.width * 0.55f;
    private float top_edge = Screen.height * 0.55f;
    private float bottom_edge = Screen.height * 0.45f;

    //Position Tracking
    private Vector3 player_pos;
    private Vector3 camera_pos;

    //Moving Camera
    private float camera_hspeed; //Camera's horizontal speed
    private float camera_vspeed; //Camera's vertical speed
    private float horizontal_offset; //Measured in world units
    private float vertical_offset; //Measured in world units

    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        player_pos = cam.WorldToScreenPoint(player.transform.position);
        //Sets the horizontal speed of the camera in conjuncion with the movement of the player
        camera_hspeed = player.sprint ? player.sprint_speed : player.standard_speed;
        camera_hspeed *= Time.fixedDeltaTime;
        //Sets the vertical speed of the camera in conjunction with the movement of the player
        camera_vspeed = Mathf.Abs(player.vertical_velocity);
        camera_vspeed *= Time.fixedDeltaTime;
        //Determines the vertical and horizontal offsets of the player if they are past the designated dead zone
        if(player_pos.x < left_edge)
        {
            horizontal_offset = (player_pos.x - left_edge) / PixelsPerUnit();
        }
        else if(player_pos.x > right_edge)
        {
            horizontal_offset = (player_pos.x - right_edge) / PixelsPerUnit();
        }
        else { horizontal_offset = 0f; }
        if (player_pos.y > top_edge)
        {
            vertical_offset = (player_pos.y - top_edge) / PixelsPerUnit();
        }
        else if (player_pos.y < bottom_edge)
        {
            vertical_offset = (player_pos.y - bottom_edge) / PixelsPerUnit();
        }
        else { vertical_offset = 0f; }

       // Debug.Log(horizontal_offset);
       // Debug.Log(vertical_offset);
        cam.transform.position = new Vector3(cam.transform.position.x + (horizontal_offset * camera_hspeed), cam.transform.position.y + (vertical_offset * camera_vspeed), cam.transform.position.z);
    }

    private float PixelsPerUnit() //Calculates the ratio for pixels:world_units using the player as a guide (since they are always on screen)
    {
        Vector3 p1 = cam.WorldToScreenPoint(player_pos);
        Vector3 p2 = cam.WorldToScreenPoint(player_pos + Vector3.right);

        return Mathf.Abs(p2.x - p1.x);

    }
}
