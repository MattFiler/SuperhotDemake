using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour
{
    public bool startShowing;
    public Text[] textObjects;
    private int[] startFontSize;

    private int counter = 0;
    private float timer = 0f;
    public float timeBetweenText = 1.5f;
    public int increaseFontSizeBy = 10;
    public bool scaleAllTextAtOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        startFontSize = new int[textObjects.Length];
        for(int i = 0; i < textObjects.Length; i++)
        {
            startFontSize[i] = textObjects[i].fontSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.unscaledDeltaTime;
        if(startShowing)
        {
            if (timeBetweenText <= timer)
            {
                if (scaleAllTextAtOnce && (counter == textObjects.Length && counter < textObjects.Length + 1))
                {
                    for (int i = 0; i < textObjects.Length; i++)
                    {
                        textObjects[i].fontSize = startFontSize[i] + increaseFontSizeBy;
                    }
                }
                else if(!scaleAllTextAtOnce && (counter >= textObjects.Length && counter < textObjects.Length * 2))
                {
                    textObjects[counter - textObjects.Length].fontSize = startFontSize[counter - textObjects.Length] + increaseFontSizeBy;
                }
                else if ((!scaleAllTextAtOnce && (counter >= textObjects.Length * 2)) || (scaleAllTextAtOnce && (counter >= textObjects.Length + 1)))
                {
                    for (int i = 0; i < textObjects.Length; i++)
                    {
                        textObjects[i].fontSize = startFontSize[i];
                        textObjects[i].enabled = false;
                    }

                    counter = -1;
                    startShowing = false;
                }
                else
                {
                    if (startFontSize.Length < textObjects.Length)
                    {
                        System.Array.Resize(ref startFontSize, textObjects.Length);
                    }

                    textObjects[counter].enabled = true;
                    startFontSize[counter] = textObjects[counter].fontSize;
                }

                counter++;
                timer = 0;
            }
        }

    }

}
