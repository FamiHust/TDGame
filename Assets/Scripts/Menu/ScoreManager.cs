using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set;}
    public TextMeshProUGUI scoreText;
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
        UpdateScoreDisplay(); 
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();

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
            scoreText.text = score.ToString(); 
        }
    }
}