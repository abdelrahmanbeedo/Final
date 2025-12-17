using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Panels")]
    public GameObject mainMenu;
    public GameObject gameUI;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    [Header("Gameplay References")]
    public Transform carSpawnPoint;

    [HideInInspector]
    public int score;

    private bool isGameStarted = false;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ShowMainMenu();
    }

    void Update()
    {
        if (!isGameStarted || Time.timeScale == 0f) return;

        // Increment score over time
        score += Mathf.RoundToInt(Time.deltaTime * 10f);

        // Pause toggle
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // ===== GAME FLOW =====

    public void StartGame()
    {
        score = 0;
        isGameStarted = true;
        Time.timeScale = 1f;

        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);

        // Reset player position or game state if needed
    }

    public void PauseGame()
    {
        if (!isGameStarted) return;

        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        gameUI.SetActive(false); // hide gameplay UI while paused
    }

    public void ResumeGame()
    {
        if (!isGameStarted) return;

        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
    }

    public void GameOver()
    {
        isGameStarted = false;
        Time.timeScale = 0f;

        gameOverMenu.SetActive(true);
        gameUI.SetActive(false);

        // Optionally, display final score
        // gameOverMenu.GetComponentInChildren<TMP_Text>().text = "Score: " + score;
    }

    public void RestartGame()
    {
        score = 0;
        isGameStarted = true;
        Time.timeScale = 1f;

        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);

        EndlessLevelHandler endless = FindObjectOfType<EndlessLevelHandler>();
        if (endless != null)
            endless.ResetLevel();

        CarHandler car = FindObjectOfType<CarHandler>();
        if (car != null)
            car.ResetCar(carSpawnPoint.position, carSpawnPoint.rotation);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in editor
        #else
            Application.Quit(); // Quit standalone build
        #endif
    }

    public void ShowMainMenu()
    {
        isGameStarted = false;
        Time.timeScale = 0f;

        mainMenu.SetActive(true);
        gameUI.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }
}
