using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public Canvas PauseScreen;

    public void ContiuedGame()
    {
        PauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadpauseScreen()
    {
        PauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

}