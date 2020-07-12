using System;
using UnityEngine;
using UnityEngine.Events;

namespace ClockupStudio.DemonSlayer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public float Force;
        public float Distance = 2f;
        public UnityEvent OnMoveEnd;

        private Rigidbody2D _rb2d;
        private Vector3 _origin;
        private bool _done;

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
            _state = new State
            {
                MovingState = PlayerMovingState.Falling
            };
            _origin = transform.position;
            _done = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_rb2d.velocity == Vector2.zero)
            {
                return;
            }

            if (Vector2.Distance(_origin, transform.position) >= Distance)
            {
                if (!_done)
                {
                    DisableMoving();
                    ResetVelocity();
                    _state = UpdateState(Action.EndMove);
                    _done = true;
                    OnMoveEnd.Invoke();
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                Debug.Log("Player hit the ground");
                _state = UpdateState(Action.HitGround);
            }
        }

        private static State UpdateState(Action action)
        {
            switch (action)
            {
                case Action.StartMoving:
                    return new State
                    {
                        MovingState = PlayerMovingState.Moving
                    };
                case Action.EndMove:
                    return new State
                    {
                        MovingState = PlayerMovingState.BeforeMove
                    };
                case Action.DisableMoving:
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

            _done = false;
            _state = UpdateState(Action.StartMoving);
            _origin = transform.position;
            _rb2d.gravityScale = 0;
            _rb2d.velocity = (crosschairPosition - (Vector2) _origin).normalized * new Vector2(Force, Force);
        }

        private void ResetVelocity()
        {
            _rb2d.velocity = Vector2.zero;
            _rb2d.gravityScale = 1;
        }

        private void DisableMoving()
        {
            _state = UpdateState(Action.DisableMoving);
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