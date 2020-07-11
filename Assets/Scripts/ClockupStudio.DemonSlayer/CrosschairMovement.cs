using System.Collections;
using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    internal enum ClockwiseState
    {
        Clockwise = 1,
        CounterClockwise = -1
    }

    struct AngleDirection
    {
        public float StartAngle, EndAngle;
        public ClockwiseState ClockwiseState;
    }

    public class CrosschairMovement : MonoBehaviour
    {
        public Transform PlayerPosition;
        public float Radius = 2f;
        public float Speed = 2f;
        public PlayerDirection PlayerDirection;

        private float _angle;
        private AngleDirection _angleDirection;
        private ClockwiseState _clockwiseState;
        private Direction _previousPlayerDirection;

        private void Start()
        {
            DetectAngleFromPlayerDirection();
            _angle = _angleDirection.StartAngle;
            _clockwiseState = _angleDirection.ClockwiseState;
            _previousPlayerDirection = PlayerDirection.CurrentDirection;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_previousPlayerDirection != PlayerDirection.CurrentDirection)
            {
                DetectAngleFromPlayerDirection();
                _angle = _angleDirection.StartAngle;
                _clockwiseState = _angleDirection.ClockwiseState;
                _previousPlayerDirection = PlayerDirection.CurrentDirection;
            }


            transform.position = PositionFromAngle(_angle) + PlayerPosition.position;
            _angle += (Time.unscaledDeltaTime * Speed) * (int) _clockwiseState;
            _clockwiseState = NextClockwise(_clockwiseState, _angle, _angleDirection);
        }

        private Vector3 PositionFromAngle(float angle)
        {
            var x = Mathf.Cos(Mathf.PI * 2 * angle / 360) * Radius;
            var y = Mathf.Sin(Mathf.PI * 2 * angle / 360) * Radius;
            return new Vector3(x, y);
        }

        private ClockwiseState NextClockwise(ClockwiseState clockwiseState, float angle, AngleDirection angleDirection)
        {
            if (clockwiseState == ClockwiseState.Clockwise && angle >= angleDirection.EndAngle)
            {
                return ClockwiseState.CounterClockwise;
            }
            else if (clockwiseState == ClockwiseState.CounterClockwise && angle <= angleDirection.StartAngle)
            {
                return ClockwiseState.Clockwise;
            }

            return clockwiseState;
        }

        private void DetectAngleFromPlayerDirection()
        {
            switch (PlayerDirection.CurrentDirection)
            {
                case Direction.Right:
                    _angleDirection = new AngleDirection
                    {
                        StartAngle = 0f,
                        EndAngle = 90f,
                        ClockwiseState = ClockwiseState.Clockwise
                    };
                    break;
                case Direction.Left:
                    _angleDirection = new AngleDirection
                    {
                        StartAngle = 90f,
                        EndAngle = 180f,
                        ClockwiseState = ClockwiseState.CounterClockwise
                    };
                    break;
            }
        }
    }
}