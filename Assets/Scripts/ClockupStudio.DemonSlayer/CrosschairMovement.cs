using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    enum Clockwises
    {
        Clockwise = 1,
        CounterClockwise = -1
    }
    public class CrosschairMovement : MonoBehaviour
    {
        public Transform PlayerPosition;

        public float Radius = 2f;

        public float Speed = 2f;

        private float _angle = 0f;

        private Clockwises _clockWise = Clockwises.Clockwise;

        // Update is called once per frame
        private void Update()
        {
            var position = PlayerPosition.position;
            var x = Mathf.Cos(Mathf.PI * 2 * _angle / 360) * Radius;
            var y = Mathf.Sin(Mathf.PI * 2 * _angle / 360) * Radius;
            
            transform.position = new Vector3(x, y) + position;
            _angle += (Time.deltaTime * Speed) * (int)_clockWise;
            if (_clockWise == Clockwises.Clockwise && _angle >= 90f)
            {
                _clockWise = Clockwises.CounterClockwise;
            }
            else if (_clockWise == Clockwises.CounterClockwise && _angle <= 0)
            {
                _clockWise = Clockwises.Clockwise;
            }
        }
    }
}