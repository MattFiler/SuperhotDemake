using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    /* When a bullet hits something, do stuff */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Kill if we hit a player or AI
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player");
            collision.gameObject.GetComponent<ThePlayer>().isDead = true;
        }
        else if (collision.gameObject.CompareTag("Ai"))
        {
            collision.gameObject.GetComponent<EnemyAI>().isDead = true;
        }

        //Destroy bullet
        Destroy(this.gameObject);
    }
}
