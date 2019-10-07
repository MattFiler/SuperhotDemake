using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    [SerializeField] private GameObject inGameScoreUI;
    [SerializeField] private GameObject endGameScoreUI;

    [SerializeField] private AudioSource victorySFX;
    [SerializeField] private AudioSource defeatSFX;
    private float totalScore = 0;

    [HideInInspector] public List<float> LevelTargetTime = new List<float>();
    private float timeInLevel = 0.0f;

    /* Show the in-game UI to start with */
    private void Start()
    {
        inGameScoreUI.SetActive(true);
        endGameScoreUI.SetActive(false);
    }

    /* Monitor the time spent in a level */
    public void Update()
    {
        timeInLevel += Time.deltaTime;
        inGameScoreUI.transform.Find("Text").GetComponent<Text>().text = "LEVEL SCORE:\n" + CalculateLevelScore();
    }

    /* Calculate the current score realtime */
    public int CalculateLevelScore()
    {
        float levelScore = LevelTargetTime[LevelLoader.Instance.GetCurrentLevelIndex()] - timeInLevel;
        if (levelScore <= 0) return 0;
        levelScore *= 10;

        return (int)levelScore;
    }

    /* Add to the score based on time in level */
    public void ClearTimeScore()
    {
        timeInLevel = 0.0f;
        totalScore += CalculateLevelScore();
    }

    /* Get the total score */
    public float GetTotalScore()
    {
        return totalScore;
    }

    /* Show the time score UI */
    public void ShowVictoryScreen()
    {
        inGameScoreUI.SetActive(false);
        endGameScoreUI.SetActive(true);

        endGameScoreUI.transform.Find("Text").GetComponent<Text>().text = "YOU WON\n\nSCORE: " + (int)totalScore;
        victorySFX.Play();
    }

    /* Show the defeat screen */
    public void ShowDefeatScreen()
    {
        inGameScoreUI.SetActive(false);
        endGameScoreUI.SetActive(true);

        endGameScoreUI.transform.Find("Text").GetComponent<Text>().text = "DEFEAT\n\nSCORE: " + (int)totalScore;
        defeatSFX.Play();
    }
}
