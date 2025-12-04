using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;
    public string nextSceneName = "Level";
    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        yesButton.onClick.AddListener(Retry);
        noButton.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void Retry()
    {
        player.Reset();
    }
}
