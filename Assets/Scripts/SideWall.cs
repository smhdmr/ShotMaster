using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{    
    void OnCollisionEnter2D(Collision2D collision)
    {
        LevelManager.Instance.ChangeHoops();
    }

}
