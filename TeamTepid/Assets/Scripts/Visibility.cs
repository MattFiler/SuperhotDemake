using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    private float timer = 0;
    private float lengthKeepVisible = 0.2f;

    private bool wasSeenOnce = false;
    private bool wasSeenForever = false;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.layer = 8;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
    }

    private void OnWillRenderObject()
    {
        if (timer >= lengthKeepVisible)
        {
            if (Camera.current.tag == "VisibilityChecker")
            {
                if (this.gameObject.tag == "Ai")
                {
                    Debug.Log(Camera.current.name);
                }

                this.gameObject.layer = 2;
                timer = 0;
            }
            else
            {
                this.gameObject.layer = 8;
            }
        }

    }
}
