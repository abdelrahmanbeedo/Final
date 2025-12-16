using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene reload

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject mainMenu;
    public GameObject gameUI;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    public int score;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ShowMainMenu();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        score += Mathf.RoundToInt(Time.deltaTime * 10f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        score = 0;  // Reset score
        Time.timeScale = 1f;

        mainMenu.SetActive(false);   // Hide main menu
        gameUI.SetActive(true);      // Show game UI
        pauseMenu.SetActive(false);  // Hide pause menu
        gameOverMenu.SetActive(false);  // Hide game over menu

        // Optionally, reset player position and other game state here
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;  // Freeze game
        pauseMenu.SetActive(true);  // Show pause menu
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;  // Resume game
        pauseMenu.SetActive(false);  // Hide pause menu
    }

    public void GameOver()
    {
        Time.timeScale = 0f;  // Freeze game time

        gameOverMenu.SetActive(true);  // Show game over panel
        gameUI.SetActive(false);  // Hide game UI

        // Optionally, display score on the Game Over menu
        // gameOverMenu.GetComponentInChildren<Text>().text = "Score: " + score;
    }

    public void RestartGame()
    {
        // Reset the game by reloading the current scene
        Time.timeScale = 1f;  // Ensure time is running
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }

    void ShowMainMenu()
    {
        Time.timeScale = 0f;  // Pause game at start

        mainMenu.SetActive(true);  // Show main menu
        gameUI.SetActive(false);  // Hide game UI
        pauseMenu.SetActive(false);  // Hide pause menu
        gameOverMenu.SetActive(false);  // Hide game over menu
    }

}
