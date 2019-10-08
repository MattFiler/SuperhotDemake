using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.SendMessageUpwards("OnChildTriggerEnter2D", collision, SendMessageOptions.DontRequireReceiver);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.SendMessageUpwards("OnChildTriggerExit2D", collision,SendMessageOptions.DontRequireReceiver);
    }
}
