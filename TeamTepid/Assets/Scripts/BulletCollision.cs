using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ai"))
        {
            GameObject.Destroy(collision.gameObject);
        }

        GameObject.Destroy(this.gameObject);
    }
}
