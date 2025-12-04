using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTriggerSwitch : MonoBehaviour
{
    [Header("End")]
    public string sceneName = "NextScene"; // Change this in Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object stepping on trigger is tagged "Player"
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(End);
        }
    }
}
