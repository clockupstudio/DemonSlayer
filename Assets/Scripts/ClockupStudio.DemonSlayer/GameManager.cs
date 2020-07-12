using System.Collections;
using System.Collections.Generic;
using ClockupStudio.DemonSlayer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerHealth PlayerHealth;
    
    void Update()
    {
        Debug.Log($"Current HP ${PlayerHealth.CurrentHp}");
        if (PlayerHealth.CurrentHp <= 0)
        {
            StartCoroutine(GoToEnding());
        }
    }

    IEnumerator GoToEnding()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EndingScene");
    }
}
