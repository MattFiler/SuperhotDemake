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
        //Destroy on death
        if (isDead)
        {
            Debug.Log("DEAD");
            gameObject.SetActive(false);
            ScoreManager.Instance.ShowDefeatScreen();
            return;
        }

        //Turn the player to the correct direction
        if (Input.GetAxis("Vertical") > 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
            currentDirection = Direction.UP;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            currentDirection = Direction.RIGHT;

        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            currentDirection = Direction.LEFT;

        }
        if (Input.GetAxis("Vertical") < 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
            currentDirection = Direction.DOWN;

        }

        //We always move forward
        gameObject.transform.position += gameObject.transform.right / 6.5f;
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
