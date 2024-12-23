using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuPanel;

    public void OpenPauseMenu()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePauseMenu()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

}
