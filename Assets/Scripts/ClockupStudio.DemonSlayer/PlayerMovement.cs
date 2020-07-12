using System;
using UnityEngine;
using UnityEngine.Events;

namespace ClockupStudio.DemonSlayer
{
    [RequireComponent(
        typeof(Rigidbody2D),
        typeof(PlayerMovementReadOnlyState))]
    public class PlayerMovement : MonoBehaviour
    {
        public float Force;
        public float Distance = 2f;
        public UnityEvent OnMoveEnd;

        private Rigidbody2D _rb2d;
        private PlayerMovementReadOnlyState _movementState;

        private Vector3 _origin;
        private State _state;

        // An internal state for this component.
        private struct State
        {
            public PlayerMovingState MovingState;
        }

        private enum Action
        {
            EndMove,
            StartMoving,
            DisableMoving,
            HitGround
        }

        // Start is called before the first frame update
        private void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _movementState = GetComponent<PlayerMovementReadOnlyState>();
            _state = new State
            {
                MovingState = PlayerMovingState.Falling
            };
            _origin = transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            _movementState.MovingState = _state.MovingState;
            if (_rb2d.velocity == Vector2.zero)
            {
                return;
            }

            if (Vector2.Distance(_origin, transform.position) >= Distance)
            {
                if (_state.MovingState == PlayerMovingState.Moving)
                {
                    MoveEnd();
                }
            }
        }

        private void MoveEnd()
        {
            ResetVelocity();
            if (_state.MovingState == PlayerMovingState.Moving)
                OnMoveEnd.Invoke();
            _state = UpdateState(Action.EndMove, _state);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                Debug.Log("Player hit the ground");
                _state = UpdateState(Action.HitGround, _state);
            }

            if (other.gameObject.CompareTag("Wall"))
            {
                Debug.Log("Player hit the wall");
                MoveEnd();
            }
        }

        private static State UpdateState(Action action, State state)
        {
            Debug.Log($"Receive action: {action}");
            switch (action)
            {
                case Action.StartMoving:
                    return new State
                    {
                        MovingState = PlayerMovingState.Moving
                    };
                case Action.EndMove:
                    if (state.MovingState != PlayerMovingState.Moving)
                        return state;

                    return new State
                    {
                        MovingState = PlayerMovingState.BeforeMove
                    };
                case Action.DisableMoving:
                    if (state.MovingState == PlayerMovingState.OnGround
                        || state.MovingState == PlayerMovingState.Moving)
                        return state;

                    return new State
                    {
                        MovingState = PlayerMovingState.Falling
                    };
                case Action.HitGround:
                    return new State
                    {
                        MovingState = PlayerMovingState.OnGround
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        public void Move(Vector2 crosschairPosition)
        {
            // Don't move during falling.
            if (_state.MovingState == PlayerMovingState.Falling)
            {
                return;
            }

            _state = UpdateState(Action.StartMoving, _state);
            _origin = transform.position;
            _rb2d.gravityScale = 0;
            _rb2d.velocity = (crosschairPosition - (Vector2) _origin).normalized * new Vector2(Force, Force);
        }

        private void ResetVelocity()
        {
            _rb2d.velocity = Vector2.zero;
            _rb2d.gravityScale = 1;
        }

        public void DisableMoving()
        {
            Debug.Log("Disable Moving..");
            _state = UpdateState(Action.DisableMoving, _state);
        }
    }

    public enum PlayerMovingState
    {
        OnGround,
        Moving,
        Falling,

        // Occur after player end moving.
        BeforeMove
    }
}