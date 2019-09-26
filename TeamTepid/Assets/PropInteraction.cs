using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropInteraction : MonoBehaviour
{
    [Tooltip("The direction the prop is thrown.")]
    [SerializeField] Vector2 throwDirection;
    [Tooltip("The speed of the throw")]
    [SerializeField] public float throwSpeed = 1;
    [Tooltip("The delay between rotaion steps - used to make rotaion seem janky")]
    [SerializeField] public float rotationDelay = 0.5f;
    [Tooltip("The amount of rotation per step")]
    [SerializeField] public float rotationStep = 5;

    private Transform playerTransform;
    private bool pickedUp = false;
    private bool startThrow = false;
    private bool destroyed = false;
   
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown("e"))
        {
            if (!pickedUp && playerTransform != null)
            {
                transform.parent = playerTransform;
                pickedUp = true;
            }
            else if(pickedUp)
            {
                pickedUp = false;
                transform.parent = null;
                startThrow = true;
            }
        }

        if(startThrow)
        {
            ThrowProp();
        }

    }

    private void ThrowProp()
    {
        GetComponent<Rigidbody2D>().velocity = throwDirection.normalized * throwSpeed;
        transform.Rotate(new Vector3(0, 0, rotationStep));
        StartCoroutine(SpinProp());
        startThrow = false;
    }

    IEnumerator SpinProp()
    {
        while (!destroyed)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotationStep);
            yield return new WaitForSeconds(rotationDelay);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = null;
        }
    }
}
