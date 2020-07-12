using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClockupStudio.DemonSlayer
{
    public class RestartController : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }
}
