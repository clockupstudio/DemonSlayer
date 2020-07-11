using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public float Force;
        public float Distance = 2f;

        private Rigidbody2D _rb2d;
        private Vector3 _origin;

        // Start is called before the first frame update
        private void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _origin = transform.position;
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
                _rb2d.velocity = Vector2.zero;
                _rb2d.gravityScale = 1;
            }
        }

        public void Move(Vector2 crosschairPosition)
        {
            _origin = transform.position;
            _rb2d.gravityScale = 0;
            _rb2d.velocity = (crosschairPosition - (Vector2) _origin).normalized * new Vector2(Force, Force);
        }
    }
}