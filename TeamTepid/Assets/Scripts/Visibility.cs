using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    private float timer = 0;
    public float lengthKeepVisible = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.layer = 8;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnWillRenderObject()
    {
        if (timer >= lengthKeepVisible)
        {
            if (Camera.current.tag == "VisibilityChecker")
            {
                if (this.gameObject.tag == "DebugTag")
                {
                    Debug.Log(Camera.current.name);
                }

                this.gameObject.layer = 0;
                timer = 0;
            }
            else
            {

                this.gameObject.layer = 8;

            }
        }

    }
}
