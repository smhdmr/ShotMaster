using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketboard : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        AudioController.PlaySFX(Global.Sfx.Basketboard);
    }

}
