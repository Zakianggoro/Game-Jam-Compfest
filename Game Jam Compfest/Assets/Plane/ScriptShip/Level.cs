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
                    UpdateThresholdText(); 
                }
                else
                {
                    NextScene(); 
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
        UpdateThresholdText(); 
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
        }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER!!!");
    }
}
