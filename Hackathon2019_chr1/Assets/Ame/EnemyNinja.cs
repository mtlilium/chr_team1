using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNinja : MonoBehaviour
{
    public float torque;

    Rigidbody2D r2d;

    private void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        r2d.AddTorque(torque);
    }
}
