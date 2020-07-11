using System.Collections;
using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    enum Clockwises
    {
        Clockwise = 1,
        CounterClockwise = -1
    }

    struct AngleDirection
    {
        public float Start, End;
    }

    public class CrosschairMovement : MonoBehaviour
    {
        public Transform PlayerPosition;

        public float Radius = 2f;

        public float Speed = 2f;

        public PlayerDirection PlayerDirection;

        private float _angle;
        private AngleDirection _angleDirection;

        private Clockwises _clockwise = Clockwises.Clockwise;

        private void Start()
        {
            DetectAngleFromPlayerDirection();
            Debug.Log($"Start Angle Direction: {_angleDirection.Start}");
            _angle = _angleDirection.Start;
        }

        // Update is called once per frame
        private void Update()
        {
            var position = PlayerPosition.position;
            var x = Mathf.Cos(Mathf.PI * 2 * _angle / 360) * Radius;
            var y = Mathf.Sin(Mathf.PI * 2 * _angle / 360) * Radius;

            transform.position = new Vector3(x, y) + position;
            _angle += (Time.deltaTime * Speed) * (int) _clockwise;
            _clockwise = NextClockwise(_clockwise, _angle, _angleDirection);
        }

        private Clockwises NextClockwise(Clockwises clockwise, float angle, AngleDirection angleDirection)
        {
            if (clockwise == Clockwises.Clockwise && angle >= angleDirection.End)
            {
                return Clockwises.CounterClockwise;
            }
            else if (clockwise == Clockwises.CounterClockwise && angle <= angleDirection.Start)
            {
                return Clockwises.Clockwise;
            }
            return clockwise;
        }

        private void DetectAngleFromPlayerDirection()
        {
            switch (PlayerDirection.CurrentDirection)
            {
                case Direction.Right:
                    _angleDirection = new AngleDirection
                    {
                        Start = 0f,
                        End = 90f
                    };
                    break;
                case Direction.Left:
                    _angleDirection = new AngleDirection
                    {
                        Start = 360f,
                        End = 270f
                    };
                    break;
            }
        }
    }
}