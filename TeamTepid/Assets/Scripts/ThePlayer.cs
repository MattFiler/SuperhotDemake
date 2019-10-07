using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePlayer : MonoBehaviour
{
    [HideInInspector] public bool isDead = false;

    /* Handle player input & move appropriately */
    void FixedUpdate()
    {
        //Destroy on death
        if (isDead)
        {
            Debug.Log("DEAD");
            gameObject.SetActive(false);
            return;
        }

        //Turn the player to the correct direction
        if (Input.GetAxis("Vertical") > 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
        }

        //We always move forward
        gameObject.transform.position += gameObject.transform.right / 6.5f;
    }
}
