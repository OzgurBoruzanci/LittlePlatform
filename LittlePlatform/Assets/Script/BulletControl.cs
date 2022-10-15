using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    MaceControl maceControl;
    Rigidbody2D physics;
    void Start()
    {
        maceControl = GameObject.FindGameObjectWithTag("Mace").GetComponent<MaceControl>();
        physics = GetComponent<Rigidbody2D>();
        physics.AddForce(maceControl.BulletDirection()*1000);
    }

}
