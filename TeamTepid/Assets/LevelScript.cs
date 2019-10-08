using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    private List<EnemyAI> levelEnemies = new List<EnemyAI>();

    /* On init of level, work out how many enemies we have, so we can monitor them for isDead state */
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        GameObject enemyParent = gameObject.transform.GetChild(0).gameObject.transform.Find("Enemies").gameObject;
        for (int i = 0; i < enemyParent.transform.childCount; i++)
        {
            levelEnemies.Add(enemyParent.transform.GetChild(i).gameObject.GetComponent<EnemyAI>());
        }
    }

    /* Monitor isDead state to see if we've won or not */
    void Update()
    {
        foreach (EnemyAI anEnemy in levelEnemies)
        {
            if (anEnemy != null)
            {
                return;
            }
        }
        LevelLoader.Instance.LoadNextLevel();
    }
}
