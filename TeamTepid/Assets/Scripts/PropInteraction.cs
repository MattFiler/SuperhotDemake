using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropInteraction : MonoBehaviour
{
    public enum PropType { SIMPLE, MELEE, RANGED }
    [Tooltip("The type of prop: simple - no use, melee - melee attack, ranged - shoot attack")]
    public PropType propType = PropType.SIMPLE;
    [Tooltip("The delay between rotaion steps - used to make rotaion seem janky")]
    public float rotationDelay = 0.5f;
    [Tooltip("The amount of rotation per step")]
    public float rotationStep = 5;
    public Vector2 defaultThrowDirection = Vector2.right;
    public bool propThrown = false;


    private AttackWithProp attack;

    private void Start()
    {
        if(GetComponent<AttackWithProp>() != null)
        {
            attack = GetComponent<AttackWithProp>();
            propType = PropType.MELEE;
        }
    }

    public void PickUpProp(Transform pickUpTransform)
    {
        transform.parent = pickUpTransform;
    }

    public void ThrowProp(Vector2 throwDirection, float throwSpeed)
    {
        GetComponent<Rigidbody2D>().velocity = throwDirection.normalized * throwSpeed;
        propThrown = true;
        StartCoroutine(SpinProp());
    }

    IEnumerator SpinProp()
    {
        while (propThrown)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotationStep);
            yield return new WaitForSeconds(rotationDelay);
        }

        yield return null;
    }

    public void UseProp()
    {
        if(attack != null)
        {
            if (attack.canAttack)
            {
                attack.startAttack();
                StartCoroutine(AttackCooldown());
                attack.canAttack = false;
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attack.attackCooldown);
        attack.canAttack = true;
        Debug.Log("Can Attack");
    }
}
