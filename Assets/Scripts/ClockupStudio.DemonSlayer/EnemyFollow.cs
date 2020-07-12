using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform Target;

    private bool _isAttacked = false;
    private void Update()
    {
        if (_isAttacked)
        {
            return;
        }
        
        transform.position = Vector2.MoveTowards(transform.position, Target.position, Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        _isAttacked = true;
    }

}
