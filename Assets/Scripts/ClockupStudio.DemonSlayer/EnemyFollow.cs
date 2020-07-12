using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform Target;
    private SpriteRenderer _sprite;

    private bool _isAttacked = false;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_isAttacked)
        {
            return;
        }

        _sprite.flipX = transform.position.x > Target.position.x;
        transform.position = Vector2.MoveTowards(transform.position, Target.position, Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
        _isAttacked = true;
    }

}
