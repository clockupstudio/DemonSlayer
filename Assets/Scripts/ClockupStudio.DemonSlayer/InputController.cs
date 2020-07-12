using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    public class InputController : MonoBehaviour
    {
        public GameObject Crosschair;
        public PlayerMovement PlayerMovement;
        public PlayerDirection PlayerDirection;
        public TimeManager TimeManager;

        private bool _controllerDisabled;

        // Update is called once per frame
        private void Update()
        {
            if (_controllerDisabled)
                return;

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (!Crosschair.activeSelf)
                {
                    Crosschair.SetActive(true);
                }
                else
                {
                    _controllerDisabled = true;
                    TimeManager.CancelSlowdown();
                    Crosschair.SetActive(false);
                    PlayerMovement.Move(Crosschair.transform.position);
                }

                return;
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                PlayerDirection.CurrentDirection = Direction.Right;
                return;
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                PlayerDirection.CurrentDirection = Direction.Left;
                return;
            }
        }

        public void EnableCrosschair()
        {
            _controllerDisabled = false;
            Crosschair.SetActive(true);
        }

        public void Timeout()
        {
            Debug.Log("Timeout!! You can't attack again until hit the ground.");
            Crosschair.SetActive(false);
        }
    }
}