using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobControl : MonoBehaviour
{
    Vector3 vec;
    void Start()
    {
        //vec = new Vector3(0, 0, 0);
    }

    
    void FixedUpdate()
    {
        transform.Rotate(vec, 180);
    }
}
