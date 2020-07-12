using System;
using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    public class Enemy : MonoBehaviour
    {
        public int Health = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var moveState = other.GetComponent<PlayerMovementReadOnlyState>();
            if (moveState != null && moveState.MovingState != PlayerMovingState.Moving)
            {
                Debug.Log("Player doesn't attack. Health doesn't decrease.");
                return;
            }

            Health--;
            if (Health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}