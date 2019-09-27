using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTest : MonoBehaviour
{
    [SerializeField] private TextTimer textTimer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !textTimer.IsPlaying())
        {
            textTimer.Play();
        }
    }
}
