using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoSingleton<LevelLoader>
{
    [SerializeField] private List<GameObject> LevelPrefabs;
    [SerializeField] private List<string> LevelNames;
    [SerializeField] private List<float> LevelTimesToAimFor = new List<float>();

    private GameObject CurrentLevel;
    private int CurrentLevelIndex = 0;

    /* On start of game, load level 0 */
    private void Start()
    {
        Screen.SetResolution(160, 192, true, 60);
        ScoreManager.Instance.ShowIntroScreen();
        ScoreManager.Instance.LevelTargetTime = LevelTimesToAimFor;
    }
    
    /* Handle intro/gameover input */
    private void Update()
    {
        if (ScoreManager.Instance.canStartGame && (Input.GetButtonDown("NES BUTTON B")||Input.GetKeyDown(KeyCode.Space)))
        {
            ScoreManager.Instance.canStartGame = false;
            ScoreManager.Instance.ClearAllScore();
            ScoreManager.Instance.HideIntroScreen();
            LoadNewLevel(0);
        }
    }

    /* Get the number of levels */
    public int GetLevelCount()
    {
        return LevelPrefabs.Count;
    }

    /* Load the next level in the sequence */
    public void LoadNextLevel()
    {
        //Game is over
        if (CurrentLevelIndex == LevelPrefabs.Count - 1)
        {
            ScoreManager.Instance.ShowVictoryScreen();
            Destroy(CurrentLevel);
            CurrentLevel = null;
            return;
        }

        //Proceed to next level
        LoadNewLevel(CurrentLevelIndex + 1);
    }

    /* Load a new level by index */
    public void LoadNewLevel(int index)
    {
        Destroy(CurrentLevel);
        SceneManager.LoadScene(index+1);
        CurrentLevel = Instantiate(LevelPrefabs[index], new Vector3(0,0,0), Quaternion.identity) as GameObject;
        CurrentLevelIndex = index;

        ScoreManager.Instance.ClearTimeScore();
        TextTimer.Instance.SetTextAndPlay(LevelNames[index], 1);
    }

    /* Return the current level as a GameObject */
    public GameObject GetCurrentLevel()
    {
        return CurrentLevel;
    }

    /* Get the current level index */
    public int GetCurrentLevelIndex()
    {
        return CurrentLevelIndex;
    }
}
