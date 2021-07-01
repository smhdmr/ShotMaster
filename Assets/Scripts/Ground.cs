using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Global.isGameStarted)
            LevelManager.Instance.OnPlayerFail();      

    }   

}
