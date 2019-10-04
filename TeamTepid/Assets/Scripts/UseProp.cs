using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseProp : MonoBehaviour
{
    public float throwSpeed = 0;

    private bool pickedUpProp;
    [SerializeField]  private PropInteraction adjacentProp;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (adjacentProp != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (!pickedUpProp)
                {
                    adjacentProp.PickUpProp(transform);
                    pickedUpProp = true;
                }
                else
                {
                    Debug.Log("Throw Prop");
                    pickedUpProp = false;
                    Vector2 currentDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                    //If player is not moving then throw is set to a default direction
                    adjacentProp.ThrowProp(currentDirection == Vector2.zero ? adjacentProp.defaultUseDirection : currentDirection, throwSpeed);
                    GetComponent<Animator>().SetInteger("Weapon Index", 0);

                }
            }
            else if (Input.GetKeyDown(KeyCode.Space) && HasProp())
            {
                Vector2 currentDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                adjacentProp.UseProp(currentDirection == Vector2.zero ? adjacentProp.defaultUseDirection : currentDirection);
            }
            else if (Input.GetKeyUp(KeyCode.Space) && HasProp())
            {
                adjacentProp.StopPropUse();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pickedUpProp && collision.gameObject.CompareTag("Prop"))
        {
            adjacentProp = collision.GetComponent<PropInteraction>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!pickedUpProp && collision.gameObject.CompareTag("Prop"))
        {
            adjacentProp = null;
        }
    }

    private bool HasProp()
    {
        foreach(Transform child in transform)
        {
            if(child.CompareTag("Prop"))
            {
                return true;
            }
        }

        return false;
    }
}
