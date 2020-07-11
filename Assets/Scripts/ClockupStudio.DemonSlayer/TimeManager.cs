using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    public class TimeManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void DoSlowdown()
        {
            // 20x slower.
            Time.timeScale = 0.05f;
        }

        public void CancelSlowdown()
        {
            Time.timeScale = 1;
        }
    }
}