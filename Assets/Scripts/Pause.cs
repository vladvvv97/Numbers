using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void PauseGameWithoutTimeScale()
    {
        GameManager.Instance.IsPaused = true;
    }
}