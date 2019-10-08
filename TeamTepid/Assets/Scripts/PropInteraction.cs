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
    [HideInInspector] public Vector2 useDirection = Vector2.zero;
    [HideInInspector] public bool propThrown = false;
    [HideInInspector] public bool canUse = false;

    private AttackWithProp attack;
    private ShootWithProp shoot;
    [HideInInspector] public string pickedupObjTag;

    private void Start()
    {
        if (GetComponent<AttackWithProp>() != null)
        {
            attack = GetComponent<AttackWithProp>();
            propType = PropType.MELEE;
        }
        else if (GetComponent<ShootWithProp>() != null)
        {
            shoot = GetComponent<ShootWithProp>();
            propType = PropType.RANGED;
        }
    }

    private void Update()
    {
        if(attack != null)
        {
            canUse = attack.canAttack;
        }
        else if(shoot != null)
        {
            shoot.setShootDirection(useDirection);

            canUse = shoot.canShoot;
        }
    }


    public void PickUpProp(Transform pickUpTransform)
    {
        if (!propThrown)
        {
            transform.parent = pickUpTransform;
            transform.position = pickUpTransform.Find("PropPos").position;
            pickedupObjTag = pickUpTransform.gameObject.tag;

            int index = 0;

            if (attack != null)
            {
                index = (int)attack.weaponType;
            }
            else if (shoot != null)
            {
                index = (int)shoot.gunType;
            }

            transform.rotation = pickUpTransform.rotation;

            pickUpTransform.GetComponent<SwapSprites>().swapSprite((SwapSprites.SpriteType)index);

            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void ThrowProp(float throwSpeed)
    {
        if (!propThrown)
        {
            pickedupObjTag = null;
            GetComponent<SpriteRenderer>().enabled = true;
            gameObject.AddComponent<Rigidbody2D>();
            GetComponent<Rigidbody2D>().velocity = useDirection.normalized * throwSpeed;
            propThrown = true;
            StartCoroutine(SpinProp());
        }
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
        if (attack != null)
        {
            if (attack.canAttack)
            {
                attack.startAttack();
                attack.canAttack = false;
                StartCoroutine(Cooldown());
            }
        }
        else if (shoot != null)
        {
            shoot.startShoot();
            shoot.canShoot = false;
            StartCoroutine(Cooldown());
        }
    }

    public void StopPropUse()
    {
        if(shoot != null && shoot.gunType == ShootWithProp.GunType.ASSAULT_RIFLE)
        {
            shoot.setStopShooting(true);
        }
    }


    IEnumerator Cooldown()
    {
        if(propType == PropType.MELEE)
        {
            yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(attack.attackCooldown);
            attack.canAttack = true;
            //Debug.Log("Can Attack");
        }
        else if(propType == PropType.RANGED)
        {
            yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(shoot.shootCooldown);
            shoot.canShoot = true;
            //Debug.Log("Can Attack");
        }

        
    }
}
