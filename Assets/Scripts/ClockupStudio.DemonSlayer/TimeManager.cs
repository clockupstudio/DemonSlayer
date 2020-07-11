using UnityEngine;
using UnityEngine.Events;

namespace ClockupStudio.DemonSlayer
{
    public class TimeManager : MonoBehaviour
    {
        public float Length = 0.5f;
        public UnityEvent OnTimeScaleNormal;

        private bool _triggered = true;

        // Update is called once per frame
        private void Update()
        {
            Time.timeScale += (1 / Length) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1);

            if (!_triggered && Time.timeScale >= 1f)
            {
                OnTimeScaleNormal.Invoke();
                _triggered = true;
            }
        }

        public void DoSlowdown()
        {
            // 20x slower.
            Time.timeScale = 0.05f;
            _triggered = false;
            Debug.Log("Slowdown...");
        }

        public void CancelSlowdown()
        {
            Time.timeScale = 1;
        }
    }
}