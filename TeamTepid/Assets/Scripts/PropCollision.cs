using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropCollision : MonoBehaviour
{
    PropInteraction propInteraction;

    private void Start()
    {
        propInteraction = GetComponent<PropInteraction>();
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(propInteraction.propThrown)
        {
            GameObject.Destroy(gameObject);
        }
    }

}
