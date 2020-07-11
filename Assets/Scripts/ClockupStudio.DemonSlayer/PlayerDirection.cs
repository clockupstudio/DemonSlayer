using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    public enum Direction
    {
        Left,
        Right
    }

    public class PlayerDirection : MonoBehaviour
    {
        public Direction CurrentDirection = Direction.Right;

        private void Update()
        {
            var scale = transform.localScale;
            switch (CurrentDirection)
            {
                case Direction.Left:
                    scale.x = Mathf.Abs(scale.x) * -1;
                    break;
                case Direction.Right:
                    scale.x = Mathf.Abs(scale.x);
                    break;
            }

            transform.localScale = scale;
        }
    }
}