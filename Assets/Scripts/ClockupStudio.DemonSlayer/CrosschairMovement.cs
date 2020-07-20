using System;
using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    [Serializable]
    public struct RotationAngle
    {
        public float From;
        public float To;
    }

    public class CrosschairMovement : MonoBehaviour
    {
        public Transform PlayerPosition;
        public float Radius = 2f;
        public float Speed = 2f;
        public PlayerDirection PlayerDirection;

        public RotationAngle LeftRotation;
        public RotationAngle RightRotation;

        // available value from 0f to 1f.
        private float _anglePercentage;
        private RotationAngle _currentRotationAngle;
        private Direction _previousPlayerDirection;

        private void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_previousPlayerDirection != PlayerDirection.CurrentDirection)
            {
                Debug.Log("Player change direction, Re-initialize state.");
                Initialize();
            }

            // set Crosshair transform base on angle.
            var angle = Mathf.Lerp(_currentRotationAngle.From, _currentRotationAngle.To, _anglePercentage);
            Debug.Log($"Current angle {angle}");
            transform.position = PositionFromAngle(angle) + PlayerPosition.position;

            _anglePercentage += Speed * Time.unscaledDeltaTime;
            Debug.Log($"angle percentage {_anglePercentage}");

            if (_anglePercentage < 1f)
                return;

            _currentRotationAngle = FlipRotationAngle(_currentRotationAngle);
            _anglePercentage = 0f;
        }

        private void Initialize()
        {
            _currentRotationAngle = RotationAngleFromDirection(PlayerDirection.CurrentDirection);
            _previousPlayerDirection = PlayerDirection.CurrentDirection;
            _anglePercentage = 0f;
        }

        private Vector3 PositionFromAngle(float angle)
        {
            var x = Mathf.Cos(Mathf.PI * 2 * angle / 360) * Radius;
            var y = Mathf.Sin(Mathf.PI * 2 * angle / 360) * Radius;
            return new Vector3(x, y);
        }

        #region Rotation

        private RotationAngle RotationAngleFromDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.Left:
                    return LeftRotation;
                case Direction.Right:
                    return RightRotation;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }

        private static RotationAngle FlipRotationAngle(RotationAngle rotationAngle)
        {
            return new RotationAngle
            {
                From = rotationAngle.To,
                To = rotationAngle.From
            };
        }

        #endregion
    }
}