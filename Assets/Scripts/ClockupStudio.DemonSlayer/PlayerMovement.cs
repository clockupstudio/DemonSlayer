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

        // Start is called before the first frame update
        private void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
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
                    OnMoveEnd.Invoke();
                    _done = true;
                }
            }
        }

        public void Move(Vector2 crosschairPosition)
        {
            _origin = transform.position;
            _rb2d.gravityScale = 0;
            _rb2d.velocity = (crosschairPosition - (Vector2) _origin).normalized * new Vector2(Force, Force);
        }

        public void ResetVelocity()
        {
            _rb2d.velocity = Vector2.zero;
            _rb2d.gravityScale = 1;
        }
    }
}