using UnityEngine;

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
        score = 0;
        Time.timeScale = 1f;

        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        gameOverMenu.SetActive(true);
        gameUI.SetActive(false);
    }

    void ShowMainMenu()
    {
        Time.timeScale = 0f;

        mainMenu.SetActive(true);
        gameUI.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }
}
