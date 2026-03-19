using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    void Update()
    {
        if (GameSession.Instance != null)
            scoreText.text = "Score: " + GameSession.Instance.GetScore();
    }
}