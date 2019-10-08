using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePlayer : MonoBehaviour
{
    [HideInInspector] public bool isDead = false;

    public enum Direction {UP, DOWN, LEFT, RIGHT }
    Direction currentDirection = Direction.RIGHT;

    /* Handle player input & move appropriately */
    void FixedUpdate()
    {
        if (isDead) return;
        if (ScoreManager.Instance.canGoToNextLevel) return;
        if (!isDead) gameObject.SetActive(true);

        //Turn the player to the correct direction
        bool shouldMove = false;
        if (Input.GetAxis("Vertical") > 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
            currentDirection = Direction.UP;
            shouldMove = true;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            currentDirection = Direction.RIGHT;
            shouldMove = true;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            currentDirection = Direction.LEFT;
            shouldMove = true;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
            currentDirection = Direction.DOWN;
            shouldMove = true;
        }

        //We always move forward
        if (shouldMove)
        {
            gameObject.transform.position += gameObject.transform.right / 6.5f;
        }
    }

    public void Kill()
    {
        StartCoroutine(DeathAnim());
    }

    private IEnumerator DeathAnim()
    {
        AiDeathSfx.Instance.PlaySFX();
        isDead = true;
        for (int i = 1; i < 6; i++)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("player_death_" + i);
        }
        gameObject.SetActive(false);
        ScoreManager.Instance.ShowDefeatScreen();
    }

    public Vector2 getCurrentDirection()
    {
        switch (currentDirection)
        {
            case Direction.UP:
                return Vector2.up;
            case Direction.DOWN:
                return Vector2.down;
            case Direction.LEFT:
                return Vector2.left;
            case Direction.RIGHT:
                return Vector2.right;
        }

        return Vector2.zero;
    }


}
