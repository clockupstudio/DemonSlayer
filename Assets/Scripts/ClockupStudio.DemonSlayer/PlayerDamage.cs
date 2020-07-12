using System;
using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    public class PlayerDamage : MonoBehaviour
    {
        private Rigidbody2D _rb2d;
        private PlayerDirection _playerDirection;

        public int BouncingForce = 500;

        private void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _playerDirection = GetComponent<PlayerDirection>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemies"))
            {
                return;
            }

            if (_playerDirection.CurrentDirection == Direction.Left)
            {
                _rb2d.AddForce(Vector2.right*500);
            }
            else
            {
                _rb2d.AddForce(Vector2.left*500);
            }
        }
    }
}
