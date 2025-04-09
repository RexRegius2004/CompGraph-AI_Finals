using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float gameDuration = 60f;
    private float timer;

    public TextMeshProUGUI timerText;
    public GameObject deathScreen;
    public GameObject winScreen;
    public TextMeshProUGUI winLoseText;

    private bool isGameOver = false;

    public AudioManager audioManager;

    void Start()
    {
        audioManager.PlayGameMusic();
        timer = gameDuration;
        deathScreen.SetActive(false);
    }


    void Update()
    {
        if (!isGameOver)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Max(timer, 0f);

            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);

            timerText.text = $"{minutes}:{seconds:00}";

            if (timer <= 0)
            {
                GameWin();
            }
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        isGameOver = true;
        winLoseText.text = "YOU DIED!";
        deathScreen.SetActive(true);
        audioManager.PlayLoseMusic();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GameWin()
    {
        Time.timeScale = 0f;
        isGameOver = true;
        winLoseText.text = "YOU SURVIVED!";
        winScreen.SetActive(true);
        audioManager.PlayMainMenuMusic();
    }

}
