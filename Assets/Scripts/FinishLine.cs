using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public GameObject gameOverPanel;
    private bool isGameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isGameOver)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);
            SoundManager.PlaySound(SoundType.GAMEOVER);
            Time.timeScale = 0;
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

}
