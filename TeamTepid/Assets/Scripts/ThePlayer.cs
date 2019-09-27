using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePlayer : MonoBehaviour
{
    private float MaxVelocity = 20.0f;
    private float VelocityChangeRate = 3000.0f;
    private Rigidbody2D thisRigidbody;

    /* Get rigidbody on start (2d) */
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
    }
    
    /* Handle player input & adjust game speed */
    void Update()
    {
        Debug.Log(thisRigidbody.velocity);
        //Player input: forwards
        if (Input.GetKey(KeyCode.W))
        {
            if (thisRigidbody.velocity.y <= MaxVelocity)
            {
                thisRigidbody.AddForce(new Vector2(0.0f, VelocityChangeRate * Time.fixedDeltaTime));
            }
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && thisRigidbody.velocity.x != 0)
            {
                thisRigidbody.velocity -= new Vector2(Time.fixedDeltaTime * thisRigidbody.velocity.x, 0.0f);
            }
        }
        //Player input: right
        if (Input.GetKey(KeyCode.D))
        {
            if (thisRigidbody.velocity.x <= MaxVelocity)
            {
                thisRigidbody.AddForce(new Vector2(VelocityChangeRate * Time.fixedDeltaTime, 0.0f));
            }
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && thisRigidbody.velocity.y != 0)
            {
                thisRigidbody.velocity -= new Vector2(0.0f, Time.fixedDeltaTime * thisRigidbody.velocity.y);
            }
        }
        //Player input: left
        if (Input.GetKey(KeyCode.A))
        {
            if (thisRigidbody.velocity.x >= -MaxVelocity)
            {
                thisRigidbody.AddForce(-new Vector2(VelocityChangeRate * Time.fixedDeltaTime, 0.0f));
            }
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && thisRigidbody.velocity.y != 0)
            {
                thisRigidbody.velocity -= new Vector2(0.0f, Time.fixedDeltaTime * thisRigidbody.velocity.y);
            }
        }
        //Player input: back
        if (Input.GetKey(KeyCode.S))
        {
            if (thisRigidbody.velocity.y >= -MaxVelocity)
            {
                thisRigidbody.AddForce(-new Vector2(0.0f, VelocityChangeRate * Time.fixedDeltaTime));
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
        //Debug.Log("GAME TIME IS: " + normalisedVelocityBothDirs);
        //Time.timeScale = normalisedVelocityBothDirs;
        //Time.fixedDeltaTime = Time.timeScale;
    }
}
