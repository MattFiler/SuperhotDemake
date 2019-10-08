using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathSfx : MonoSingleton<AiDeathSfx>
{
    public void PlaySFX()
    {
        GetComponent<AudioSource>().Play();
    }
}
