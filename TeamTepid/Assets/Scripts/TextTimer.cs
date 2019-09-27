using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTimer : MonoBehaviour
{
    [SerializeField] private List<string> textToRender = new List<string>();
    [SerializeField] private float timeBetweenText = 1.0f;

    private Text textUI;
    private AudioSource audioSource;

    private int currentTextIndex = 0;
    private float currentTextTimer = 0.0f;
    private float currentIntervalTimer = 0.0f;
    private bool shouldTrigger = false;

    /* Grab appropriate objects */
    private void Start()
    {
        textUI = gameObject.transform.GetChild(0).GetComponent<Text>();
        audioSource = gameObject.transform.GetChild(0).GetComponent<AudioSource>();
    }

    /* If playing, handle logic */
    void Update()
    {
        if (shouldTrigger && currentTextTimer <= textToRender.Count * timeBetweenText)
        {
            textUI.text = textToRender[currentTextIndex];

            if (currentIntervalTimer >= timeBetweenText)
            {
                currentIntervalTimer = 0.0f;
                currentTextIndex++;
                if (currentTextIndex >= textToRender.Count)
                {
                    shouldTrigger = false;
                }
                gameObject.transform.GetChild(0).GetComponent<AudioSource>().Play();
            }

            currentTextTimer += Time.deltaTime;
            currentIntervalTimer += Time.deltaTime;
        }
        else
        {
            shouldTrigger = false;
            textUI.text = "";
        }
    }

    /* Play the text */
    public void Play()
    {
        if (IsPlaying())
        {
            Debug.LogWarning("Tried to play already playing text!");
            return;
        }

        currentTextIndex = 0;
        currentTextTimer = 0.0f;
        currentIntervalTimer = 0.0f;
        shouldTrigger = true;

        gameObject.transform.GetChild(0).GetComponent<AudioSource>().Play();
    }

    /* Get if the text animation is already playing */
    public bool IsPlaying()
    {
        return shouldTrigger;
    }
}
