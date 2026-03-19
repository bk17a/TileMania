using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameSession : MonoBehaviour
{
    // ─── Singleton ───────────────────────────────────────────────────────
    public static GameSession Instance { get; private set; }

    // ─── State ───────────────────────────────────────────────────────────
    [SerializeField] int lives = 3;
    int score = 0;
    HashSet<string> collectedCoins = new HashSet<string>();

    void Awake()
    {
        // Singleton pattern — one GameSession persists across scenes
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ─── Score ───────────────────────────────────────────────────────────
    public void AddScore(int points)
    {
        score += points;
        // TODO: Update score UI
    }

    public int GetScore() => score;

    // ─── Lives ───────────────────────────────────────────────────────────
    public void ProcessPlayerDeath()
    {
        lives--;
        if (lives > 0)
            ReloadCurrentScene();
        else
            LoadGameOver();
    }

    // ─── Scene Loading ───────────────────────────────────────────────────
    void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadGameOver()
    {
        Destroy(gameObject);  // Reset session
        SceneManager.LoadScene("GameOver");
    }

    public void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int lastLevelIndex = 3;

        if (currentIndex >= lastLevelIndex)
            SceneManager.LoadScene("WinScreen");
        else
            SceneManager.LoadScene(currentIndex + 1);
    }

    public void CollectCoin(string coinID, int pointValue)
    {
        collectedCoins.Add(coinID);
        AddScore(pointValue);
    }

    public bool IsCoinCollected(string coinID)
    {
        return collectedCoins.Contains(coinID);
    }
}