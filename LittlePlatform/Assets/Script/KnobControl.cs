using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobControl : MonoBehaviour
{
    int number = 0;
    //float time = 0;
    void Start()
    {

    }

    
    void Update()
    {
        Swing();
    }
    void Swing()
    {
        if (number == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -45), 0.05f);
            //System.Threading.Thread.Sleep(1000);
            if (transform.rotation==Quaternion.Euler(0,0,-45))
            {
                number = 1;
            }
        }
        else if (number == 1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 45), 0.05f);
            //System.Threading.Thread.Sleep(1000);
            if (transform.rotation == Quaternion.Euler(0, 0, 45))
            {
                number = 0;
            }

        }
    }
    
}
