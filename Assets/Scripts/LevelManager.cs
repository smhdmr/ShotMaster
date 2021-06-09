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
    private Hoop hoopLscript, hoopRscript;

    
    void Start()
    {
        //GET HOOPS
        hoopL = hoops.transform.GetChild(0).gameObject;
        hoopR = hoops.transform.GetChild(1).gameObject;

        //GET HOOP SCRIPTS
        hoopLscript = hoopL.GetComponent<Hoop>();
        hoopRscript = hoopR.GetComponent<Hoop>();
    }
    

    public void GetScore()
    {
        AudioController.PlaySFX(Global.Sfx.Score);       
        Global.score++;
        TimeController.Instance.AddTime();
        Ball.Instance.ResetVelocityX();

        ChangeHoops();
    }

    public void ChangeHoops()
    {
        if (Global.gameDir == Global.GameDirection.toLeft)
        {
            //CHANGE GAME DIRECTION
            Global.gameDir = Global.GameDirection.toRight;

            //CHANGE THE VISIBLE HOOP
            hoopL.SetActive(false);

            hoopRscript.ChangePosition();            
            hoopR.SetActive(true);
        }

        else if (Global.gameDir == Global.GameDirection.toRight)
        {
            //CHANGE GAME DIRECTION
            Global.gameDir = Global.GameDirection.toLeft;

            //CHANGE THE VISIBLE HOOP
            hoopR.SetActive(false);

            hoopLscript.ChangePosition();
            hoopL.SetActive(true);
        }
    }

    public static void ResetStreak()
    {
        Global.streakCount = 0;
    }
}
