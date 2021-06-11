using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;
    public float timeToAdd;
    public float timeToAddStreak;
    public float timeToEnd;
    private float remainingTime;
    public GameObject timeBar;
    private Image timeBarFill;

    void Awake()
    {
        Instance = this;
        timeBarFill = timeBar.transform.GetChild(1).GetComponent<Image>();
        remainingTime = timeToEnd;
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.isGameStarted)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime >= timeToEnd)
                remainingTime = timeToEnd;

            timeBarFill.fillAmount = remainingTime / timeToEnd;

            if(remainingTime <= 0f)
            {
                remainingTime = 0f;
                Global.isGameStarted = false;
                Global.isPlayable = false;
                UIManager.Instance.GameOverPanelSet(true);
                Ball.Instance.ResetVelocityX();
            }
        }        
    }

    public void AddTime()
    {     
        if (Global.streakCount > 1)
            remainingTime += timeToAddStreak;

        else
            remainingTime += timeToAdd;

        if (remainingTime >= timeToEnd)
            remainingTime = timeToEnd;

        timeBarFill.fillAmount = remainingTime / timeToEnd;
    }

    public void ResetTime()
    {
        remainingTime = timeToEnd;
        timeBarFill.fillAmount = remainingTime / timeToEnd;
    }

}
