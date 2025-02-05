using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishLine : MonoBehaviour
{
    public GameObject gameOverPanel;
    private bool isGameOver = false;

    void Start()
    {
        if (isGameOver)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isGameOver)
        {
            Health.Instance.TakeDamage(1);
            SoundManager.PlaySound(SoundType.PLAYERHURT);

            if (Health.Instance.currentHealth <= 0)
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
