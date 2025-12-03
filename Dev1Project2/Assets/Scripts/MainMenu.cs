using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject SettingsPanel;
    public GameObject ContinuePanel;

    public Button startButton;
    public Button settingsButton;
    public Button quitButton;
    public Button backButton;
    public Button continueButton;

    public string nextSceneName = "Level";

    void Start()
    {
        StartPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        ContinuePanel.SetActive(false);

        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(Quit);
        backButton.onClick.AddListener(Back);
        continueButton.onClick.AddListener(ContinueGame);
    }

    // --- BUTTON FUNCTIONS ---

    public void StartGame()
    {
        ContinueGame();
    }

    public void OpenSettings()
    {
        StartPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        ContinuePanel.SetActive(false);
    }

    public void OpenContinuePanel()
    {
        StartPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        ContinuePanel.SetActive(true);
    }

    public void Back()
    {
        StartPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        ContinuePanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}