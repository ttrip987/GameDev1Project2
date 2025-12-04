using UnityEngine;

public class CharacterFlipLikeHitbox : MonoBehaviour
{
    [Header("References")]
    public Transform model;          // The child model to rotate
    public Player playerScript;      // Reference to your Player script
    public float forwardRotationY = 0f; // Normally 0 degrees
    public float backwardRotationY = 180f; // Flipped rotation

    void Update()
    {
        if (playerScript == null || model == null)
            return;

        if (playerScript.move_dir < 0)
        {
            model.localRotation = Quaternion.Euler(0, backwardRotationY, 0);
        }
        else if (playerScript.move_dir > 0)
        {
            model.localRotation = Quaternion.Euler(0, forwardRotationY, 0);
        }
    }
}
