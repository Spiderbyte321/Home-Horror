using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject howToPlayPanel;

    private void Start()
    {
        ShowMainMenu();
    }

    // ========== MAIN MENU ==========
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
    }

    // ========== BUTTON FUNCTIONS ==========
    public void PlayGame()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    // ========== BACK BUTTON ==========
    public void BackToMainMenu()
    {
        ShowMainMenu();
    }
}