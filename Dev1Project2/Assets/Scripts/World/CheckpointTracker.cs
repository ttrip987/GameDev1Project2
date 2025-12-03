using UnityEngine;

public class CheckpointTracker : MonoBehaviour
{
    public Checkpoint[] checkpoints;

    public Vector3 GetCheckpoint() //This will find the most recent checkpoint and return the Vector3 of the spawnpoint of it
    {
        for(int i = 0; i <= checkpoints.Length; i++)
        {
            if (checkpoints[i].activated) //Will go along each checkpoint in order to find the most recently activated one
            {
                continue;
            }
            else
            {
                return checkpoints[i - 1].checkpointSpawn;
            }
        }
        return Vector3.zero; //Never hits, but complains if this line doesn't exist
    }
}
