using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public GameObject QuitPanel;

    public void Start()
    {
        Time.timeScale = 1;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        QuitPanel.SetActive(true);

    }

    public void Response(string response)
    {
        if (response == "quit")
        {
            Application.Quit();
        }
        else
        {
            QuitPanel.SetActive(false);
        }
    }
}
