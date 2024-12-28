using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public void OpenLevel()
    {
        SceneManager.LoadScene(2);
    }
}
