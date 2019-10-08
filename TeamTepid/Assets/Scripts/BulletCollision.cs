using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [HideInInspector] public string ignoreCollisionTag;

    /* When a bullet hits something, do stuff */
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Kill if we hit a player or AI
        if (collider.CompareTag(ignoreCollisionTag) || collider.CompareTag("Bullet") || collider.CompareTag("Prop"))
        {
            return;
        }

        if (ignoreCollisionTag != "Player" && collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player");
            collider.gameObject.GetComponent<ThePlayer>().isDead = true;
        }
        else if (ignoreCollisionTag != "Ai" && collider.gameObject.CompareTag("Ai"))
        {
            collider.gameObject.GetComponent<EnemyAI>().isDead = true;
        }

        //Destroy bullet
        Destroy(this.gameObject);
    }
}
