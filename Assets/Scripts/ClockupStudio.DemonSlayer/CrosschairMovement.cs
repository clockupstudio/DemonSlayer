using System;
using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    internal enum Rotation
    {
        Clockwise = -1,
        CounterClockwise = 1
    }


    public class CrosschairMovement : MonoBehaviour
    {
        public Transform PlayerPosition;
        public float Radius = 2f;
        public float Speed = 2f;
        public PlayerDirection PlayerDirection;

        private State _state;

        // Represents internal state for this component.
        private struct State
        {
            // Track angle in each update loop.
            public float CurrentAngle;
            public float StartAngle, EndAngle;
            public Rotation Rotation;
            public Direction PreviousPlayerDirection;
        }

        private void Start()
        {
            _state = InitializeState(PlayerDirection.CurrentDirection);
        }

        // Update is called once per frame
        private void Update()
        {
            if (_state.PreviousPlayerDirection != PlayerDirection.CurrentDirection)
            {
                Debug.Log("Player change direction, Re-initialize state.");
                _state = InitializeState(PlayerDirection.CurrentDirection);
            }

            transform.position = PositionFromAngle(_state.CurrentAngle) + PlayerPosition.position;
            _state = UpdateState(_state, Time.unscaledDeltaTime, Speed);

            Debug.Log($"Current angle: {_state.CurrentAngle}, Rotation: {_state.Rotation}");
        }

        private Vector3 PositionFromAngle(float angle)
        {
            var x = Mathf.Cos(Mathf.PI * 2 * angle / 360) * Radius;
            var y = Mathf.Sin(Mathf.PI * 2 * angle / 360) * Radius;
            return new Vector3(x, y);
        }

        #region State

        private static State InitializeState(Direction dir)
        {
            var state = new State
            {
                PreviousPlayerDirection = dir
            };

            switch (dir)
            {
                case Direction.Right:
                    state.StartAngle = 0f;
                    state.EndAngle = 90f;
                    state.Rotation = Rotation.CounterClockwise;
                    break;
                case Direction.Left:
                    state.StartAngle = 180f;
                    state.EndAngle = 90f;
                    state.Rotation = Rotation.Clockwise;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }

            state.CurrentAngle = state.StartAngle;
            return state;
        }

        private static State UpdateState(State previousState, float unscaledDeltaTime, float speed)
        {
            return new State
            {
                StartAngle = previousState.StartAngle,
                EndAngle = previousState.EndAngle,
                CurrentAngle = previousState.CurrentAngle + (unscaledDeltaTime * speed * (int) previousState.Rotation),
                PreviousPlayerDirection = previousState.PreviousPlayerDirection,
                Rotation = NextRotation(previousState.Rotation, previousState.CurrentAngle, previousState.StartAngle,
                    previousState.EndAngle)
            };
        }

        #endregion

        #region Rotation

        private static Rotation NextRotation(Rotation rotation, float angle, float startAngle,
            float endAngle)
        {
            switch (rotation)
            {
                // For rotation of right direction.
                case Rotation.CounterClockwise when angle >= endAngle && startAngle < endAngle:
                    return Rotation.Clockwise;
                case Rotation.Clockwise when angle <= startAngle && startAngle < endAngle:
                    return Rotation.CounterClockwise;

                // For rotation of left direction.
                case Rotation.CounterClockwise when angle >= startAngle && startAngle > endAngle:
                    return Rotation.Clockwise;
                case Rotation.Clockwise when angle <= endAngle && startAngle > endAngle:
                    return Rotation.CounterClockwise;
                default:
                    return rotation;
            }
        }

        #endregion
    }
}