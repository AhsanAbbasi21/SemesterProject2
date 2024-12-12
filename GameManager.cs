using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject enemyPrefab;
    public float minInstantiateValue;
    public float maxInstantiateValue;
    public float enemyDestroyTime = 10f;
    public int score = 0;
    public int playerLives = 3;
    public TextMeshProUGUI livesText;


    [Header("Particle Effect")]
    public GameObject explosion;
    public GameObject muzzleFlash;

    [Header("Panels")]
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;  // Add this for game over UI

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        startMenu.SetActive(true);
        pauseMenu.SetActive(true);
        gameOverMenu.SetActive(false);  // Ensure it's hidden at start
        Time.timeScale = 0f;
        InvokeRepeating("InstantiateEnemy", 1f, 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame(true);
        }
    }

    void InstantiateEnemy()
    {
        Vector3 enemyPos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
        GameObject enemy = Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
        Destroy(enemy, enemyDestroyTime); // Destroy enemy after a set time
    }

    public void StartGameButton()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void pauseGame(bool isPaused)
    {
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AddScore(int value)
    {
        score += value; // Increase score
        Debug.Log("Score: " + score); // Optional: Log the score to the console
    }

    // Game Over method that shows the Game Over screen
    public void GameOver()
    {
        // Show the game over screen and pause the game
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;  // Pause the game
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Log the name of the object the player collided with
        Debug.Log("Player collided with: " + collision.gameObject.name);

        // Check if the player collided with an enemy or an enemy bullet
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Handle player collision with enemy
            Debug.Log("Collision with enemy detected");
            GameObject gm = Instantiate(GameManager.instance.explosion, transform.position, transform.rotation);
            Destroy(gm, 2f); // Destroy explosion effect after 2 seconds
            Destroy(this.gameObject); // Destroy the player
            GameManager.instance.GameOver();
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            // Handle player collision with enemy bullet
            Debug.Log("Collision with enemy bullet detected");
            GameObject gm = Instantiate(GameManager.instance.explosion, transform.position, transform.rotation);
            Destroy(gm, 2f); // Destroy explosion effect after 2 seconds
            Destroy(this.gameObject); // Destroy the player
            GameManager.instance.GameOver();
        }
    }
    public void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + playerLives;
        }
        else
        {
            Debug.LogError("LivesText is not assigned in the Inspector!");
        }
    }
    public void ReducePlayerLives()
    {
        playerLives--; // Decrease lives by 1
        UpdateLivesUI();

        if (playerLives <= 0)
        {
            GameOver(); // Call GameOver if no lives are left
        }
    }


}
