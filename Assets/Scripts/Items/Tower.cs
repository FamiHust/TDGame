using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Tower : MonoBehaviour
{
    public GameObject gameOverPanel;
    // private Health playerHealth;
    private bool isGameOver = false;

    void Start()
    {
        // playerHealth = FindObjectOfType<Health>();

        if (isGameOver)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isGameOver)
        {
            Health.Instance.TakeDamageTower(1);
            SoundManager.PlaySound(SoundType.PLAYERHURT);

            // Kiểm tra nếu máu về 0
            if (Health.Instance.currentHealth <= 0)
            {
                StartCoroutine(DelayGameOver());
            }
        }
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
