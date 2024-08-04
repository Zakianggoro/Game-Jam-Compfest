using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSwitcher : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(0);
    }
}
