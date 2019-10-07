using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePlayer : MonoBehaviour
{
    private float MaxVelocity = 20.0f;
    private float VelocityChangeRate = 3000.0f;
    private Rigidbody2D thisRigidbody;
    private GameObject thisSprite;

    /* Get rigidbody on start (2d) */
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        thisSprite = gameObject.transform.Find("Sprite").gameObject;
    }
    
    /* Handle player input & adjust game speed */
    void Update()
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

        //Player input: forwards
        if (Input.GetAxis("Vertical") > 0)
        {
            thisSprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
            if (thisRigidbody.velocity.y <= MaxVelocity)
            {
                thisRigidbody.AddForce(new Vector2(0.0f, Input.GetAxis("Vertical") * VelocityChangeRate * Time.fixedDeltaTime));
            }
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && thisRigidbody.velocity.x != 0)
            {
                thisRigidbody.velocity -= new Vector2(Time.fixedDeltaTime * thisRigidbody.velocity.x, 0.0f);
            }
        }
        //Player input: right
        if (Input.GetAxis("Horizontal") > 0)
        {
            thisSprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            if (thisRigidbody.velocity.x <= MaxVelocity)
            {
                thisRigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal") * VelocityChangeRate * Time.fixedDeltaTime, 0.0f));
            }
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && thisRigidbody.velocity.y != 0)
            {
                thisRigidbody.velocity -= new Vector2(0.0f, Time.fixedDeltaTime * thisRigidbody.velocity.y);
            }
        }
        //Player input: left
        if (Input.GetAxis("Horizontal") < 0)
        {
            thisSprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            if (thisRigidbody.velocity.x >= -MaxVelocity)
            {
                thisRigidbody.AddForce(-new Vector2((Input.GetAxis("Horizontal") * -1) * VelocityChangeRate * Time.fixedDeltaTime, 0.0f));
            }
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && thisRigidbody.velocity.y != 0)
            {
                thisRigidbody.velocity -= new Vector2(0.0f, Time.fixedDeltaTime * thisRigidbody.velocity.y);
            }
        }
        //Player input: back
        if (Input.GetAxis("Vertical") < 0)
        {
            thisSprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
            if (thisRigidbody.velocity.y >= -MaxVelocity)
            {
                thisRigidbody.AddForce(-new Vector2(0.0f, (Input.GetAxis("Vertical") * -1) * VelocityChangeRate * Time.fixedDeltaTime));
            }
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && thisRigidbody.velocity.x != 0)
            {
                thisRigidbody.velocity -= new Vector2(Time.fixedDeltaTime * thisRigidbody.velocity.x, 0.0f);
            }
        }
        
        //Change game speed based on velocity
        float velocityX = (thisRigidbody.velocity.x > 0) ? thisRigidbody.velocity.x : thisRigidbody.velocity.x * -1;
        float velocityY = (thisRigidbody.velocity.y > 0) ? thisRigidbody.velocity.y : thisRigidbody.velocity.y * -1;
        float normalisedVelocityX = velocityX / (MaxVelocity - (MaxVelocity / 5));
        if (normalisedVelocityX > 1.0f) { normalisedVelocityX = 1.0f; }
        float normalisedVelocityY = velocityY / (MaxVelocity - (MaxVelocity / 5));
        if (normalisedVelocityY > 1.0f) { normalisedVelocityY = 1.0f; }
        float normalisedVelocityBothDirs = normalisedVelocityX + normalisedVelocityY;
        if (normalisedVelocityBothDirs >= 1.0f) { normalisedVelocityBothDirs = 1.0f; }
        if (normalisedVelocityBothDirs <= 0.1f) { normalisedVelocityBothDirs = 0.05f; }
        //Debug.Log("GAME TIME IS: " + normalisedVelocityBothDirs);
        Time.timeScale = normalisedVelocityBothDirs;
        //Time.fixedDeltaTime = Time.timeScale;
    }
}
