using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText; // Tham chiếu đến TextMeshProUGUI

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
        UpdateScoreDisplay(); // Cập nhật điểm số khi bắt đầu
    }

    private void Update()
    {
        // Cập nhật điểm số liên tục (nếu cần)
        UpdateScoreDisplay();
    }

    public void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + ScoreManager.Instance.GetScore(); // Cập nhật văn bản
    }

    public int GetScore()
    {
        return score;
    }

       public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score); // In ra điểm số hiện tại
        UpdateScoreDisplay(); // Cập nhật điểm số trên UI
    }
}