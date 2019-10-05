using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoSingleton<LevelLoader>
{
    [SerializeField] private List<GameObject> LevelPrefabs;
    private GameObject CurrentLevel;

    /* On start of game, load level 0 */
    private void Start()
    {
        LoadNewLevel(0);
    }

    /* Load a new level by index */
    public void LoadNewLevel(int index)
    {
        Destroy(CurrentLevel);
        CurrentLevel = Instantiate(LevelPrefabs[index], new Vector3(0,0,0), Quaternion.identity) as GameObject;
    }

    /* Return the current level as a GameObject */
    public GameObject GetCurrentLevel()
    {
        return CurrentLevel;
    }
}
