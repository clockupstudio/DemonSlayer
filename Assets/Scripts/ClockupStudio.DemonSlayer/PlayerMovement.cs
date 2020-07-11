using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public float Force;

        private Rigidbody2D _rb2d;

        // Start is called before the first frame update
        private void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void Move(Vector2 crosscharPosition)
        {
            // TODO(wingyplus): ignore gravity.
            _rb2d.velocity = crosscharPosition.normalized * new Vector2(Force, Force);
        }
    }
}