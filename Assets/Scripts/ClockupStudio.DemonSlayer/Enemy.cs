using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace ClockupStudio.DemonSlayer
{
    public class Enemy : MonoBehaviour
    {
        public uint Health = 1;

        private void Update()
        {
            if (Health == 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var moveState = other.GetComponent<PlayerMovementReadOnlyState>();
            if (moveState == null)
            {
                Debug.Log("Player doesn't have movement read-only state. Health doesn't decrease.");
                return;
            }
            if (moveState.MovingState != PlayerMovingState.Moving)
            {
                Debug.Log("Player doesn't attack. Health doesn't decrease.");
                return;
            }

            Health--;
        }
    }
}