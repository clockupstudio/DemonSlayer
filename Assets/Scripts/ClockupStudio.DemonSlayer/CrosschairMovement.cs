using System;
using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    internal enum Rotation
    {
        Clockwise = 1,
        CounterClockwise = -1
    }

    internal struct CrosschairMovementState
    {
        // Track angle in each update loop.
        public float CurrentAngle;
        public float StartAngle, EndAngle;
        public Rotation Rotation;
        public Direction PreviousPlayerDirection;
    }

    public class CrosschairMovement : MonoBehaviour
    {
        public Transform PlayerPosition;
        public float Radius = 2f;
        public float Speed = 2f;
        public PlayerDirection PlayerDirection;

        private CrosschairMovementState _state;

        private void Start()
        {
            _state = InitializeState(PlayerDirection.CurrentDirection);
        }

        // Update is called once per frame
        private void Update()
        {
            if (_state.PreviousPlayerDirection != PlayerDirection.CurrentDirection)
            {
                _state = InitializeState(PlayerDirection.CurrentDirection);
            }

            transform.position = PositionFromAngle(_state.CurrentAngle) + PlayerPosition.position;
            _state.CurrentAngle += Time.unscaledDeltaTime * Speed * (int) _state.Rotation;
            _state.Rotation =
                NextClockwise(_state.Rotation, _state.CurrentAngle, _state.StartAngle, _state.EndAngle);
        }

        private Vector3 PositionFromAngle(float angle)
        {
            var x = Mathf.Cos(Mathf.PI * 2 * angle / 360) * Radius;
            var y = Mathf.Sin(Mathf.PI * 2 * angle / 360) * Radius;
            return new Vector3(x, y);
        }

        #region State

        private static CrosschairMovementState InitializeState(Direction dir)
        {
            var state = new CrosschairMovementState
            {
                PreviousPlayerDirection = dir
            };

            switch (dir)
            {
                case Direction.Right:
                    state.StartAngle = 0f;
                    state.EndAngle = 90f;
                    state.Rotation = Rotation.Clockwise;
                    break;
                case Direction.Left:
                    state.StartAngle = 90f;
                    state.EndAngle = 180f;
                    state.Rotation = Rotation.CounterClockwise;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }

            state.CurrentAngle = state.StartAngle;
            return state;
        }

        private static Rotation NextClockwise(Rotation rotation, float angle, float startAngle,
            float endAngle)
        {
            switch (rotation)
            {
                case Rotation.Clockwise when angle >= endAngle:
                    return Rotation.CounterClockwise;
                case Rotation.CounterClockwise when angle <= startAngle:
                    return Rotation.Clockwise;
                default:
                    return rotation;
            }
        }

        #endregion
    }
}