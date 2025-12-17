using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Panels")]
    public GameObject mainMenu;
    public GameObject gameUI;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text timeSurvivedText;



    [Header("Gameplay References")]
    public Transform carSpawnPoint;

    [HideInInspector]
    public int score;
    
    private float timeSurvived = 0f;


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

    // Track survival time
    timeSurvived += Time.deltaTime;

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
        timeSurvived = 0f;

        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);

        // Reset player and level when starting
        EndlessLevelHandler endless = FindObjectOfType<EndlessLevelHandler>();
        if (endless != null)
            endless.ResetLevel();

        CarHandler car = FindObjectOfType<CarHandler>();
        if (car != null)
            car.ResetCar(carSpawnPoint.position, carSpawnPoint.rotation);
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

    if (finalScoreText != null)
        finalScoreText.text = "Final Score: " + score;

    if (timeSurvivedText != null)
        timeSurvivedText.text = "Time Survived: " + Mathf.FloorToInt(timeSurvived) + "s";
}





    public void RestartGame()
    {
        score = 0;
        isGameStarted = true;
        Time.timeScale = 1f;
        timeSurvived = 0f;

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

        // Reset car and level so player isn't stuck
        EndlessLevelHandler endless = FindObjectOfType<EndlessLevelHandler>();
        if (endless != null)
            endless.ResetLevel();

        CarHandler car = FindObjectOfType<CarHandler>();
        if (car != null)
            car.ResetCar(carSpawnPoint.position, carSpawnPoint.rotation);
    }
}
