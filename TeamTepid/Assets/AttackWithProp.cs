using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWithProp : MonoBehaviour
{
    [Tooltip("The nuber of successfull hits before prop breaks, -1 = inifinite")]
    public int numberOfHits = 0;
    [Tooltip("The time delay between attacks")]
    public float attackCooldown = 0;

    public bool attacking = false;
    public bool canAttack = true;
    private bool weaponColliding = false;

    private void Update()
    {
        if(attacking && weaponColliding)
        {
            Debug.Log("Sucessfull Attack");

            --numberOfHits;

            if (numberOfHits == 0)
            {
                GameObject.Destroy(gameObject);
            }

            attacking = false;
        }
    }

    public void startAttack()
    {
        Debug.Log("Starting Attack");
        attacking = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        weaponColliding = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        weaponColliding = false;
    }
}
