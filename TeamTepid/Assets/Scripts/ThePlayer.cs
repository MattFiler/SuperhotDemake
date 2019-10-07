using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePlayer : MonoBehaviour
{
    /* Handle player input & move appropriately */
    void FixedUpdate()
    {
        //FOR REFERENCE
        if (Input.GetButton("NES BUTTON B"))
        {
            Debug.Log("NES BUTTON B");
        }
        if (Input.GetButton("NES BUTTON A"))
        {
            Debug.Log("NES BUTTON A");
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
        gameObject.transform.position += gameObject.transform.right / 8;
    }
}
