using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropInteraction : MonoBehaviour
{
    [Tooltip("The delay between rotaion steps - used to make rotaion seem janky")]
    [SerializeField] public float rotationDelay = 0.5f;
    [Tooltip("The amount of rotation per step")]
    [SerializeField] public float rotationStep = 5;
    public Vector2 defaultThrowDirection = Vector2.right;
    public bool propThrown = false;

    private bool startSpin = false;
    private bool destroyed = false;

    //private Transform playerTransform;
    //private bool startThrow = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startSpin)
        {
            StartCoroutine(SpinProp());
            startSpin = false;
        }

    }

    //public void ThrowProp(Vector2 throwDirection)
    //{
    //    
    //    transform.Rotate(new Vector3(0, 0, rotationStep));
    //    StartCoroutine(SpinProp());
    //    startThrow = false;
    //}

    IEnumerator SpinProp()
    {
        while (!destroyed)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotationStep);
            yield return new WaitForSeconds(rotationDelay);
        }
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        playerTransform = collision.transform;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        playerTransform = null;
    //    }
    //}

    public void PickUpProp(Transform pickUpTransform)
    {
        transform.parent = pickUpTransform;
    }

    public void ThrowProp(Vector2 throwDirection, float throwSpeed)
    {
        GetComponent<Rigidbody2D>().velocity = throwDirection.normalized * throwSpeed;
        propThrown = true;
        startSpin = true;
    }
}
