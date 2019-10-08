using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    private float timer = 0;
    private float lengthKeepVisible = 0.2f;

    private bool wasSeenOnce;
    private bool wasSeenForever;

    public bool activateSeenForever = true;
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
        if(activateSeenForever)
        {
            if (wasSeenForever)
            {
                this.gameObject.layer = 2;
            }
            else
            {
                if (timer >= 0)
                {
                    if (Camera.current.tag == "VisibilityChecker")
                    {
                        if (wasSeenOnce)
                        {
                            wasSeenForever = true;
                        }

                        if (this.gameObject.tag == "Ai")
                        {
                            //Debug.Log(Camera.current.name);
                        }

                        this.gameObject.layer = 2;
                        wasSeenOnce = true;
                        timer = 0;
                    }
                    else
                    {
                        this.gameObject.layer = 8;
                        wasSeenOnce = false;
                    }
                }
            }
        }
        else
        {
            if (timer >= lengthKeepVisible)
            {
                if (Camera.current.tag == "VisibilityChecker")
                {
                    if (this.gameObject.tag == "Ai")
                    {
                        //Debug.Log(Camera.current.name);
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
}
