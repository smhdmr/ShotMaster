using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball Instance;
    public float xPower, yPower, speedLimitX, speedLimitY;
    private Rigidbody2D rb;
    private Vector2 forceVector;
    

    void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        forceVector = new Vector2(xPower, yPower);
    }
    

    // Update is called once per frame
    void Update()
    {
        //CHECKS THE INPUT
        if (Input.GetMouseButtonDown(0))
        {
            if (!Global.isGameStarted)
                Global.isGameStarted = true;                                   

            if(Global.gameDir == Global.GameDirection.toLeft)            
                forceVector.x = -xPower;            

            else
                forceVector.x = xPower;

            rb.AddForce(forceVector, ForceMode2D.Impulse);           

        }

        LimitVelocity();

    }

    //LIMITS THE BALL VELOCITY
    private void LimitVelocity()
    {
        if(Global.gameDir == Global.GameDirection.toRight)
        {
            //LIMITS THE MOVEMENT VELOCITY 
            if (rb.velocity.x >= speedLimitX)
                rb.velocity = new Vector2(speedLimitX, rb.velocity.y);

            if (rb.velocity.y >= speedLimitY)
                rb.velocity = new Vector2(rb.velocity.x, speedLimitY);
        }

        else if(Global.gameDir == Global.GameDirection.toLeft)
        {            
            if (rb.velocity.x <= -speedLimitX)
                rb.velocity = new Vector2(-speedLimitX, rb.velocity.y);

            if (rb.velocity.y >= speedLimitY)
                rb.velocity = new Vector2(rb.velocity.x, speedLimitY);
        }

    }


    public void ResetVelocityX()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }
}
