using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadIn : MonoBehaviour
{
    public bool startLoadIn = true;
    public Material start_mat;
    public GameObject start_obj;

    public int counter = 1;
    private float timer = 0;
    public float timeBetweenColorChanges = 1.0f;
    public int howManyColorChanges = 3;

    // Start is called before the first frame update
    void Start()
    {
        //start_mat.color = new Color(0.0f, 0.0f, 0.0f, 255.0f);
        start_obj.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.0f, 0.0f, 0.0f, 1.0f));

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.unscaledDeltaTime;

        if(startLoadIn)
        {
            if (timeBetweenColorChanges < timer)
            {
                if (counter >= howManyColorChanges)
                {
                    //start_mat.SetColor("_Color", new Color(0.0f, 0.0f, 0.0f, 0.0f));
                    start_obj.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.0f, 0.0f, 0.0f, 0.0f));
                    counter = 1;
                    startLoadIn = false;
                }
                else
                {
                    //start_mat.color = new Color(0.0f, 0.0f, 0.0f, ((255.0f / howManyColorChanges) * howManyColorChanges - counter));
                    //start_obj.GetComponent<MeshRenderer>().material = start_mat;
                    //start_obj.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.0f, 0.0f, 0.0f, (255.0f / (float)howManyColorChanges)));

                    start_obj.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.0f, 0.0f, 0.0f, ((float)(1.0f / (float)howManyColorChanges) * (float)((float)howManyColorChanges - (float)counter))));

                }

                counter++;
                timer = 0;
            }
        }
    }
}
