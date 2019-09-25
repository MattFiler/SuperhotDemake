using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePlayer : MonoBehaviour
{
    [SerializeField] private float MaxVelocity = 50.0f;
    [SerializeField] private float VelocityChangeRate = 5.0f;
    private Rigidbody2D thisRigidbody;

    /* Get rigidbody on start (2d) */
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
    }
    
    /* Handle player input */
    //void FixedUpdate()
    void Update()
    {
        //FORWARD
        if (Input.GetKey(KeyCode.W))
        {
            if (thisRigidbody.velocity.y <= MaxVelocity)
            {
                thisRigidbody.velocity += new Vector2(0.0f, VelocityChangeRate);
            }
        }
        //RIGHT
        if (Input.GetKey(KeyCode.D))
        {
            if (thisRigidbody.velocity.x <= MaxVelocity)
            {
                thisRigidbody.velocity += new Vector2(VelocityChangeRate, 0.0f);
            }
        }
        //LEFT
        if (Input.GetKey(KeyCode.A))
        {
            if (thisRigidbody.velocity.x >= -MaxVelocity)
            {
                thisRigidbody.velocity -= new Vector2(VelocityChangeRate, 0.0f);
            }
        }
        //BACK
        if (Input.GetKey(KeyCode.S))
        {
            if (thisRigidbody.velocity.y >= -MaxVelocity)
            {
                thisRigidbody.velocity -= new Vector2(0.0f, VelocityChangeRate);
            }
        }

        //Change game speed based on velocity
        float velocityX = (thisRigidbody.velocity.x > 0) ? thisRigidbody.velocity.x : thisRigidbody.velocity.x * -1;
        float velocityY = (thisRigidbody.velocity.y > 0) ? thisRigidbody.velocity.y : thisRigidbody.velocity.y * -1;
        float normalisedVelocityX = velocityX / MaxVelocity;
        if (normalisedVelocityX > 1.0f) { normalisedVelocityX = 1.0f; }
        float normalisedVelocityY = velocityY / MaxVelocity;
        if (normalisedVelocityY > 1.0f) { normalisedVelocityY = 1.0f; }
        float normalisedVelocityBothDirs = normalisedVelocityX + normalisedVelocityY;
        if (normalisedVelocityBothDirs >= 1.0f) { normalisedVelocityBothDirs = 1.0f; }
        //Time.timeScale = normalisedVelocityBothDirs;
        //Time.fixedDeltaTime = Time.timeScale;
    }
}
