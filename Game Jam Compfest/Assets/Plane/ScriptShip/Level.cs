using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    public static Level instance;

    uint numDestructables = 0;
    bool startNextLevel = false;
    float nextLevelTimer = 1;

    string[] levels = { "SHMUP", "SHMUP2", "SHMUP3" };
    int currentLevel = 1;

    int score = 0;
    TMP_Text scoreText;
    TMP_Text thresholdText;

    public int[] scoreThresholds = { 1500, 4500, 9000 };
    private int currentThresholdIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
            thresholdText = GameObject.Find("ScoreThreshold").GetComponent<TMP_Text>();
            UpdateThresholdText();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (startNextLevel)
        {
            if (nextLevelTimer <= 0)
            {
                currentLevel++;
                currentThresholdIndex++;
                if (currentLevel <= levels.Length)
                {
                    string sceneName = levels[currentLevel - 1];
                    SceneManager.LoadSceneAsync(sceneName);
                    UpdateThresholdText(); // Update threshold text when switching levels
                }
                else
                {
                    NextScene(); // Go to the next scene after the last level
                }
                nextLevelTimer = 3;
                startNextLevel = false;
            }
            else
            {
                nextLevelTimer -= Time.deltaTime;
            }
        }
    }

    public void ResetLevel()
    {
        foreach (Bullet b in GameObject.FindObjectsOfType<Bullet>())
        {
            Destroy(b.gameObject);
        }
        numDestructables = 0;
        score = 0;
        AddScore(score);
        string sceneName = levels[currentLevel - 1];
        SceneManager.LoadScene(sceneName);
        UpdateThresholdText(); // Update threshold text when resetting the level
    }

    public void AddScore(int amountToAdd)
    {
        score += amountToAdd;
        scoreText.text = score.ToString();

        // Add score threshold check
        if (score >= scoreThresholds[currentThresholdIndex])
        {
            ClearDestructables();
            startNextLevel = true;
        }
    }

    public void AddDestructable()
    {
        numDestructables++;
    }

    public void RemoveDestructable()
    {
        numDestructables--;

        if (numDestructables == 0)
        {
            startNextLevel = true;
        }
    }

    private void ClearDestructables()
    {
        foreach (Destructable d in GameObject.FindObjectsOfType<Destructable>())
        {
            Destroy(d.gameObject);
        }
        numDestructables = 0;
    }

    private void UpdateThresholdText()
    {
        if (currentThresholdIndex < scoreThresholds.Length)
        {
            thresholdText.text = scoreThresholds[currentThresholdIndex].ToString();
        }
    }

    private void NextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels. Game Over.");
            // Optionally, add code to handle the end of the game, like returning to the main menu
        }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER!!!");
        // Add end game logic here
        // For example, display the end game screen, return to the main menu, etc.
    }
}
