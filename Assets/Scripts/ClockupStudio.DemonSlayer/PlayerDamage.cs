using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    public class PlayerDamage : MonoBehaviour
    {
        private Rigidbody2D _rb2d;
        private PlayerDirection _playerDirection;
        private PlayerMovementReadOnlyState _playerMovementReadOnlyState;
        private PlayerHealth _playerHealth;

        public int BouncingForce = 500;

        private void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _playerDirection = GetComponent<PlayerDirection>();
            _playerMovementReadOnlyState = GetComponent<PlayerMovementReadOnlyState>();
            _playerHealth = GetComponent<PlayerHealth>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemies"))
            {
                return;
            }

            if (_playerMovementReadOnlyState.MovingState == PlayerMovingState.Moving)
            {
                return;
            }

            _playerHealth.Decrease();

            if (_playerDirection.CurrentDirection == Direction.Left)
            {
                _rb2d.AddForce(Vector2.right*BouncingForce);
            }
            else
            {
                _rb2d.AddForce(Vector2.left*BouncingForce);
            }
        }
    }
}
