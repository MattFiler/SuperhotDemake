using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    [SerializeField] private GameObject inGameScoreUI;
    [SerializeField] private GameObject endGameScoreUI;
    [SerializeField] private GameObject nextLevelPrompt;

    [SerializeField] private AudioSource mainMusicSFX;
    [SerializeField] private AudioSource introSFX;
    [SerializeField] private AudioSource victorySFX;
    [SerializeField] private AudioSource victoryStingSFX;
    [SerializeField] private AudioSource defeatSFX;
    [SerializeField] private AudioSource defeatStingSFX;
    private float totalScore = 0;
    private int levelScoreExtra = 0;
    private bool scoreFrozen = false;
    private int frozenScore = 0;

    [HideInInspector] public List<float> LevelTargetTime = new List<float>();
    private float timeInLevel = 0.0f;

    public bool canStartGame = false;
    public bool canGoToNextLevel = false;

    /* Monitor the time spent in a level */
    public void Update()
    {
        timeInLevel += Time.deltaTime;
        inGameScoreUI.transform.Find("Text").GetComponent<Text>().text = "LEVEL SCORE:\n" + CalculateLevelScore();
    }

    /* Calculate the current score realtime */
    public int CalculateLevelScore()
    {
        if (scoreFrozen) return frozenScore;

        float levelScore = (LevelTargetTime[LevelLoader.Instance.GetCurrentLevelIndex()] + levelScoreExtra) - timeInLevel;
        if (levelScore <= 0) return 0;
        levelScore *= 10;

        return (int)levelScore;
    }

    /* Add to the score */
    public void AddToLevelScore(int value = 10)
    {
        levelScoreExtra += value;
    }

    /* Freeze the score decrease */
    public void FreezeScore()
    {
        frozenScore = CalculateLevelScore();
        scoreFrozen = true;
    }

    /* Update total score */
    public void UpdateScore()
    {
        totalScore += CalculateLevelScore();
        ClearTimeScore();
    }

    /* Add to the score based on time in level */
    public void ClearTimeScore()
    {
        timeInLevel = 0.0f;
        levelScoreExtra = 0;
        scoreFrozen = false;
    }

    /* Clear the total score */
    public void ClearAllScore()
    {
        totalScore = 0;
    }

    /* Get the total score */
    public float GetTotalScore()
    {
        return totalScore;
    }

    /* Show the intro UI */
    public void ShowIntroScreen()
    {
        inGameScoreUI.SetActive(false);
        endGameScoreUI.SetActive(true);
        nextLevelPrompt.SetActive(false);
        DontDestroyOnLoad(nextLevelPrompt);
        canStartGame = true;

        endGameScoreUI.transform.Find("Text").GetComponent<Text>().text = "SUPER HOT\n\nPRESS B";
        introSFX.Play();
        mainMusicSFX.Stop();
    }
    
    /* Hide the intro UI */
    public void HideIntroScreen()
    {
        inGameScoreUI.SetActive(true);
        endGameScoreUI.SetActive(false);
        nextLevelPrompt.SetActive(false);
        canStartGame = false;
        ClearTimeScore();

        victorySFX.Stop();
        defeatSFX.Stop();
        introSFX.Stop();
        mainMusicSFX.Play();
    }

    /* Show "next level" ui prompt */
    public void ShowNextLevelPrompt()
    {
        if (!canGoToNextLevel) UpdateScore();
        ClearTimeScore();

        inGameScoreUI.SetActive(false);
        nextLevelPrompt.SetActive(true);
        canGoToNextLevel = true;

        mainMusicSFX.Stop();
    }

    /* Hide "next level" ui prompt */
    public void HideNextLevelPrompt()
    {
        inGameScoreUI.SetActive(true);
        nextLevelPrompt.SetActive(false);
        canGoToNextLevel = false;

        mainMusicSFX.Play();
    }

    /* Show the time score UI */
    public void ShowVictoryScreen()
    {
        inGameScoreUI.SetActive(false);
        endGameScoreUI.SetActive(true);
        nextLevelPrompt.SetActive(false);
        canStartGame = true;

        bool wasHighscore = LogNewScore();
        string endgameText = "YOU WON!\n\n";
        string endgameTextEnd = "YOUR SCORE: " + (int)totalScore + "\n\nPRESS B";
        if (wasHighscore) endgameText += "NEW HIGHSCORE!\n" + endgameTextEnd;
        if (!wasHighscore) endgameText += "HIGHSCORE: " + HighestScore().ToString() + "\n" + endgameTextEnd;
        endGameScoreUI.transform.Find("Text").GetComponent<Text>().text = endgameText;
        
        victorySFX.Play();
        victoryStingSFX.Play();
        mainMusicSFX.Stop();
    }

    /* Show the defeat screen */
    public void ShowDefeatScreen()
    {
        inGameScoreUI.SetActive(false);
        endGameScoreUI.SetActive(true);
        nextLevelPrompt.SetActive(false);
        canStartGame = true;

        bool wasHighscore = LogNewScore();
        string endgameText = "DEFEAT\n\n";
        string endgameTextEnd = "YOUR SCORE: " + (int)totalScore + "\n\nPRESS B";
        if (wasHighscore) endgameText += "NEW HIGHSCORE!\n" + endgameTextEnd;
        if (!wasHighscore) endgameText += "HIGHSCORE: " + HighestScore().ToString() + "\n" + endgameTextEnd;
        endGameScoreUI.transform.Find("Text").GetComponent<Text>().text = endgameText;

        defeatSFX.Play();
        defeatStingSFX.Play();
        mainMusicSFX.Stop();
    }

    /* Log new score */
    public bool LogNewScore()
    {
        PlayerPrefs.SetString("highscores", PlayerPrefs.GetString("highscores") + "," + ((int)totalScore).ToString());
        return (HighestScore() == (int)totalScore);
    }

    /* Get the highest score logged */
    public int HighestScore()
    {
        List<string> allScores = new List<string>(PlayerPrefs.GetString("highscores").Split(','));
        int highestScore = 0;
        foreach (string score in allScores)
        {
            if (score == "") continue;
            int thisScore = Convert.ToInt32(score);
            if (thisScore > highestScore)
            {
                highestScore = thisScore;
            }
        }
        return highestScore;
    }
}
