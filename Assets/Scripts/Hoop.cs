using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{    
    public float upLimit;
    public float downLimit;

    public void ChangePosition()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(downLimit, upLimit), transform.position.z);
    }

}
