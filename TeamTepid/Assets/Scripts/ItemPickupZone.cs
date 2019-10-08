using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.parent == null) return;
        Debug.Log("Enter");
        transform.SendMessageUpwards("OnChildTriggerEnter2D", collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (transform.parent == null) return;
        transform.SendMessageUpwards("OnChildTriggerExit2D", collision);
    }
}
