using Birds;
using Enemies;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private EnemySpawner _enemySpawner;
    
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;

    private void OnEnable()
    {
        _bird.GameOver += OnGameOver;
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _gameOverScreen.RestartButtonClicked += OnRestartButtonClick;
    }

    private void OnDisable()
    {
        _bird.GameOver -= OnGameOver;
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _gameOverScreen.RestartButtonClicked -= OnRestartButtonClick;
    }

    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.Open();
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        _gameOverScreen.Open();
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void OnRestartButtonClick()
    {
        _gameOverScreen.Close();
        ReloadScene();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _bird.Reset();
    }

    private void ReloadScene() => 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}