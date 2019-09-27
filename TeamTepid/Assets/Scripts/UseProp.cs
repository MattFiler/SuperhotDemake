using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseProp : MonoBehaviour
{
    public float throwSpeed = 0;

    private bool pickedUpProp;
    [SerializeField]  private PropInteraction currentProp;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentProp != null)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                if(!pickedUpProp)
                {
                    currentProp.PickUpProp(transform);
                    pickedUpProp = true;
                }
                else
                {
                    pickedUpProp = false;
                    Vector2 currentDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                    //If player is not moving then throw is set to a default direction
                    currentProp.ThrowProp(currentDirection == Vector2.zero ? currentProp.defaultThrowDirection : currentDirection, throwSpeed);
                }
            }
            else if(Input.GetKeyDown(KeyCode.Space))
            {
                currentProp.UseProp();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Prop"))
        {
            currentProp = collision.GetComponent<PropInteraction>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Prop"))
        {
            currentProp = null;
        }
    }
}
