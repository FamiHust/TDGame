using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float remainingTime;
    public float speedManage = 1.0f;
    private bool isPaused = false;
    private bool isGameWin = false;
    public GameObject winPanel;

    private bool isBlinking = false; // Biến kiểm tra trạng thái nhấp nháy
    private float blinkInterval = 0.5f; // Thời gian giữa các lần nhấp nháy
    private float blinkTimer = 0f; // Bộ đếm thời gian cho nhấp nháy

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            timerText.color = Color.red;
            GameWin();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (remainingTime <= 11f)
        {
            isBlinking = true;
        }

        if (isBlinking)
        {
            blinkTimer += Time.deltaTime;
            if (blinkTimer >= blinkInterval)
            {
                timerText.color = (timerText.color == Color.red) ? Color.white : Color.red;
                blinkTimer = 0f; // Đặt lại bộ đếm thời gian
            }
        }
    }

    public void GameWin()
    {
        isGameWin = true;
        StopAllSpawners();
        UnlockNewLevel();
        StartCoroutine(GameWinCoroutine());
    }

    private IEnumerator GameWinCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
        SoundManager.PlaySound(SoundType.VICTORY);
        winPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    private void StopAllSpawners()
    {
        EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();

        foreach (EnemySpawner spawner in spawners)
        {
            spawner.CancelInvoke("SpawnEnemy");
        }
    }
}
