using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    public class CrosschairMovement : MonoBehaviour
    {
        public Transform PlayerPosition;

        public float Radius = 2f;

        public float Speed = 2f;

        private float _angle = 0f;

        // Update is called once per frame
        private void Update()
        {
            var position = PlayerPosition.position;
            var x = Mathf.Cos(Mathf.PI * 2 * _angle / 360) * Radius;
            var y = Mathf.Sin(Mathf.PI * 2 * _angle / 360) * Radius;

            Debug.Log($"New Position: {x}, {y}");
            transform.position = new Vector3(x, y) + position;
            _angle += Time.deltaTime * Speed;
            Debug.Log($"Current angle: {_angle}");
            if (_angle >= 90f)
            {
                _angle = 0f;
            }
        }
    }
}