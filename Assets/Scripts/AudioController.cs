using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySFX(Global.Sfx sfx)
    {
        switch (sfx)
        {
            case Global.Sfx.Score:
                audioSource.clip = SFX.instance.throwball;
                audioSource.Play();
                break;

            case Global.Sfx.Basketboard:
                audioSource.clip = SFX.instance.boardHit;
                audioSource.Play();
                break;

            case Global.Sfx.Ground:
                audioSource.clip = SFX.instance.groundHit;
                audioSource.Play();
                break;
        }
    }
}
