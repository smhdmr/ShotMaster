using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float xPower, yPower, speedLimitX, speedLimitY;
    private Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    void Update()
    {
        //CHECKS THE INPUT
        if (Input.GetMouseButtonDown(0))
        {
            if (!Global.isGameStarted)
                Global.isGameStarted = true;
                        
            Vector2 forceVector = new Vector2(xPower, yPower);

            if(Global.gameDir == Global.GameDirection.toLeft)
            {
                forceVector.x *= -1f;
            }

            rb.AddForce(forceVector, ForceMode2D.Impulse);           

        }


        //LIMITS THE MOVEMENT VELOCITY 
        if (rb.velocity.x >= speedLimitX)
            rb.velocity = new Vector2(speedLimitX, rb.velocity.y);

        if (rb.velocity.y >= speedLimitY)
            rb.velocity = new Vector2(rb.velocity.x, speedLimitY);

    }
}
