using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); 
    }
    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("QuitGame called");
        Application.Quit();
    }
}