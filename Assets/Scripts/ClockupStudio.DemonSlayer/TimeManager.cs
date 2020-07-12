using UnityEngine;
using UnityEngine.Events;

namespace ClockupStudio.DemonSlayer
{
    public class TimeManager : MonoBehaviour
    {
        public float Length = 0.5f;

        // A slow down value to set to Time.timeScale. Default to 
        // 0.5f, which is 20x slower.
        public float TimeScaleSlowdown = 0.05f;

        // Invoke after Time.timeScale go back to normal (1f).
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
            Time.timeScale = TimeScaleSlowdown;
            _triggered = false;
            Debug.Log("Slowdown...");
        }

        public void CancelSlowdown()
        {
            Time.timeScale = 1;
        }
    }
}