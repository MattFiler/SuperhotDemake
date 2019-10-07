using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoSingleton<LevelLoader>
{
    [SerializeField] private List<GameObject> LevelPrefabs;
    private GameObject CurrentLevel;
    private int CurrentLevelIndex = 0;

    /* On start of game, load level 0 */
    private void Start()
    {
        LoadNewLevel(0);
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
            TextTimer.Instance.SetTextAndPlay("GAME OVER", 3);
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
        CurrentLevel = Instantiate(LevelPrefabs[index], new Vector3(0,0,0), Quaternion.identity) as GameObject;
        CurrentLevelIndex = index;
        TextTimer.Instance.SetTextAndPlay("LEVEL " + (index + 1), 2);
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
