using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText; // Reference to the score text
    private int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreDisplay(); // Update score at start
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();

        // Increase energy based on score
        if (EnergyManager.Instance != null)
        {
            EnergyManager.Instance.AddEnergy(amount);
        }
    }

    public int GetScore()
    {
        return score;
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Update score text
        }
    }
}