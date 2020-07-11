using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    public class InputController : MonoBehaviour
    {
        public GameObject Crosschair;
        public PlayerMovement PlayerMovement;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (!Crosschair.activeSelf)
                {
                    Crosschair.SetActive(true);
                }
                else
                {
                    Crosschair.SetActive(false);
                    PlayerMovement.Move(Crosschair.transform.position);
                }
            }
        }
    }
}