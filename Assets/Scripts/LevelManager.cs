using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Instance
    public static LevelManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    public GameObject hoops;    
    private GameObject hoopL, hoopR;

    
    void Start()
    {
        //GET HOOPS
        hoopL = hoops.transform.GetChild(0).gameObject;
        hoopR = hoops.transform.GetChild(1).gameObject;
    }
    

    public void GetScore()
    {
        Global.score++;
        Ball.Instance.ResetVelocityX();

        if(Global.gameDir == Global.GameDirection.toLeft)
        {
            //CHANGE GAME DIRECTION
            Global.gameDir = Global.GameDirection.toRight;

            //CHANGE THE VISIBLE HOOP
            hoopL.SetActive(false);
            hoopR.SetActive(true);
        }

        else if (Global.gameDir == Global.GameDirection.toRight)
        {
            //CHANGE GAME DIRECTION
            Global.gameDir = Global.GameDirection.toLeft;

            //CHANGE THE VISIBLE HOOP
            hoopR.SetActive(false);
            hoopL.SetActive(true);
        }
    }
}
