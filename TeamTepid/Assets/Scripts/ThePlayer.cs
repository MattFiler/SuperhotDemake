using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePlayer : MonoBehaviour
{
    [SerializeField] private float MaxVelocity = 50000.0f;
    [SerializeField] private float VelocityChangeRate = 5000.0f;
    private Rigidbody2D thisRigidbody;
    private RectTransform spriteTransform;

    /* Get rigidbody on start (2d) */
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        spriteTransform = gameObject.transform.GetChild(0).GetComponent<RectTransform>();
    }
    
    /* Handle player input */
    void FixedUpdate()
    {
        //FORWARD
        if (Input.GetKey(KeyCode.W))
        {
            if (thisRigidbody.velocity.y <= MaxVelocity * 10000)
            {
                thisRigidbody.AddForce(new Vector2(0.0f, VelocityChangeRate * Time.fixedDeltaTime * 10000));
            }
            if (thisRigidbody.velocity.x != 0)
            {
                thisRigidbody.velocity -= new Vector2(Time.fixedDeltaTime * thisRigidbody.velocity.x * 2, 0.0f);
            }
            spriteTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        //RIGHT
        if (Input.GetKey(KeyCode.D))
        {
            if (thisRigidbody.velocity.x <= MaxVelocity * 10000)
            {
                thisRigidbody.AddForce(new Vector2(VelocityChangeRate * Time.fixedDeltaTime * 10000, 0.0f));
            }
            if (thisRigidbody.velocity.y != 0)
            {
                thisRigidbody.velocity -= new Vector2(0.0f, Time.fixedDeltaTime * thisRigidbody.velocity.y * 2);
            }
            spriteTransform.rotation = Quaternion.Euler(new Vector3(0,0,270));
        }
        //LEFT
        if (Input.GetKey(KeyCode.A))
        {
            if (thisRigidbody.velocity.x >= -(MaxVelocity * 10000))
            {
                thisRigidbody.AddForce(-new Vector2(VelocityChangeRate * Time.fixedDeltaTime * 10000, 0.0f));
            }
            if (thisRigidbody.velocity.y != 0)
            {
                thisRigidbody.velocity -= new Vector2(0.0f, Time.fixedDeltaTime * thisRigidbody.velocity.y * 2);
            }
            spriteTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        //BACK
        if (Input.GetKey(KeyCode.S))
        {
            if (thisRigidbody.velocity.y >= -(MaxVelocity * 10000))
            {
                thisRigidbody.AddForce(-new Vector2(0.0f, VelocityChangeRate * Time.fixedDeltaTime * 10000));
            }
            if (thisRigidbody.velocity.x != 0)
            {
                thisRigidbody.velocity -= new Vector2(Time.fixedDeltaTime * thisRigidbody.velocity.x * 2, 0.0f);
            }
            spriteTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }

        /*
        //Change game speed based on velocity
        float velocityX = (thisRigidbody.velocity.x > 0) ? thisRigidbody.velocity.x : thisRigidbody.velocity.x * -1;
        float velocityY = (thisRigidbody.velocity.y > 0) ? thisRigidbody.velocity.y : thisRigidbody.velocity.y * -1;
        float normalisedVelocityX = velocityX / MaxVelocity;
        if (normalisedVelocityX > 1.0f) { normalisedVelocityX = 1.0f; }
        float normalisedVelocityY = velocityY / MaxVelocity;
        if (normalisedVelocityY > 1.0f) { normalisedVelocityY = 1.0f; }
        float normalisedVelocityBothDirs = normalisedVelocityX + normalisedVelocityY;
        if (normalisedVelocityBothDirs >= 1.0f) { normalisedVelocityBothDirs = 1.0f; }
        //Debug.Log(normalisedVelocityBothDirs);
        //Time.timeScale = normalisedVelocityBothDirs;
        //Time.fixedDeltaTime = Time.timeScale;
        */
    }
}
