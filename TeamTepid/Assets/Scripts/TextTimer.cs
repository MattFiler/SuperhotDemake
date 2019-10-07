using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTimer : MonoSingleton<TextTimer>
{
    [SerializeField] private string textString = "";
    [SerializeField] private float timeBetweenText = 1.0f;

    private GameObject childObject;
    private Text textUI;
    private AudioSource audioSource;

    private List<string> textToRender = new List<string>();
    private int currentTextIndex = 0;
    private float currentTextTimer = 0.0f;
    private float currentIntervalTimer = 0.0f;
    private bool shouldTrigger = false;

    /* Grab appropriate objects and parse text from editor */
    private void Start()
    {
        childObject = gameObject.transform.GetChild(0).gameObject;
        textUI = gameObject.transform.GetChild(0).GetComponent<Text>();
        audioSource = gameObject.transform.GetChild(0).GetComponent<AudioSource>();

        if (textString != "")
        {
            textToRender = new List<string>(textString.Trim().ToUpper().Split(' '));
        }
    }

    /* If playing, handle logic */
    void Update()
    {
        if (shouldTrigger && currentTextTimer <= textToRender.Count * timeBetweenText)
        {
            textUI.text = textToRender[currentTextIndex];
            textUI.color = new Color(0.2784314f, 0.003921569f, 0.4784314f, 1.0f);
            childObject.transform.localScale += new Vector3(0.002f, 0.002f, 0.002f);

            if (currentIntervalTimer >= timeBetweenText)
            {
                currentIntervalTimer = 0.0f;
                currentTextIndex++;
                if (currentTextIndex >= textToRender.Count)
                {
                    shouldTrigger = false;
                }
                gameObject.transform.GetChild(0).GetComponent<AudioSource>().Play();
                childObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }
            else if (currentIntervalTimer + Time.unscaledDeltaTime >= timeBetweenText)
            {
                textUI.color = Color.white;
            }

            currentTextTimer += Time.unscaledDeltaTime;
            currentIntervalTimer += Time.unscaledDeltaTime;
        }
        else
        {
            shouldTrigger = false;
            textUI.text = "";
        }
    }

    /* Set a new text string to animate in code */
    public void SetText(string text, float interval = 1.0f)
    {
        if (IsPlaying())
        {
            Debug.LogWarning("Tried to set new text on already playing text!");
            return;
        }

        textToRender = new List<string>(text.Trim().ToUpper().Split(' '));
        timeBetweenText = interval;
    }

    /* Set a new text string and play immediately */
    public void SetTextAndPlay(string text, float interval = 1.0f)
    {
        SetText(text, interval);
        Play();
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
