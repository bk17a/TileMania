using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}