using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public void OnStartClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnOptionsClicked()
    {
        // TODO
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}