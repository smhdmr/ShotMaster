using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Global.isGameStarted)
        {
            Global.isGameStarted = false;
            Global.isPlayable = false;
            UIManager.Instance.GameOverPanelSet(true);
            Ball.Instance.ResetVelocityX();
        }

    }   

}
