using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTest : MonoBehaviour
{
    void FixedUpdate()
    {
        gameObject.transform.position += new Vector3(0.0f, 5.0f, 0.0f);
        if (gameObject.transform.position.y >= 600.0f)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.0f, 0.0f);
        }
    }
}
