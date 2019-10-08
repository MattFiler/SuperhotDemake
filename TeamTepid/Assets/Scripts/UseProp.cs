using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseProp : MonoBehaviour
{
    public float throwSpeed = 0;

    private bool pickedUpProp;
    [SerializeField]  private PropInteraction adjacentProp;
    bool usingProp = false;
    // Start is called before the first frame update
    void Start()
    {
        //FOR REFERENCE
        if (Input.GetButton("NES BUTTON B"))
        {
            Debug.Log("NES BUTTON B");
        }
        if (Input.GetButton("NES BUTTON A"))
        {
            Debug.Log("NES BUTTON A");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (adjacentProp != null)
        {
            adjacentProp.useDirection = GetComponent<ThePlayer>().getCurrentDirection();


            if(!pickedUpProp)
            {
                if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("NES BUTTON A"))
                {
                    adjacentProp.PickUpProp(transform);
                    pickedUpProp = true;
                }
            }
            else
            {
                if (!usingProp && adjacentProp.canUse && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("NES BUTTON A")))
                {
                    Debug.Log("down");
                    adjacentProp.UseProp();
                    usingProp = true;
                }
                else if(usingProp && (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("NES BUTTON A")))
                {
                    Debug.Log("up");
                    adjacentProp.StopPropUse();
                    usingProp = false;
                }
            }







            //adjacentProp.useDirection = direction;
            ////Debug.Log(adjacentProp.useDirection);

            //if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("NES BUTTON B"))
            //{
            //    if (!pickedUpProp)
            //    {
            //        adjacentProp.PickUpProp(transform);
            //        pickedUpProp = true;
            //    }
            //    else
            //    {
            //        Debug.Log("Throw Prop");
            //        pickedUpProp = false;
            //        //If player is not moving then throw is set to a default direction
            //        adjacentProp.ThrowProp( throwSpeed);

            //    }
            //}
            //else if (HasProp())
            //{
            //    if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("NES BUTTON A"))
            //    {
            //        adjacentProp.UseProp();
            //    }
            //    else if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("NES BUTTON A"))
            //    {
            //        adjacentProp.StopPropUse();
            //    }
        
        }
    }

    private void OnChildTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter2");
        if (!pickedUpProp && collision.gameObject.CompareTag("Prop"))
        {
            adjacentProp = collision.GetComponent<PropInteraction>();
        }
    }

    private void OnChildTriggerExit2D(Collider2D collision)
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
