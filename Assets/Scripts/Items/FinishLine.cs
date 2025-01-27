using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    public GameObject gameOverPanel;
    private Health playerHealth;
    private bool isGameOver = false;

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = FindObjectOfType<Health>();

        if (isGameOver)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isGameOver)
        {
            playerHealth.TakeDamage(1);
            SoundManager.PlaySound(SoundType.PLAYERHURT);

            // Kiểm tra nếu máu về 0
            if (playerHealth.currentHealth <= 0)
            {
                StartCoroutine(DelayGameOver());
            }
        }
    }


    public void ReplayGame(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    public void OpenMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ReplayMenuTutor()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(1f);
        isGameOver = true;
        SoundManager.PlaySound(SoundType.GAMEOVER);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
