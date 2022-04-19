using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadMainMenuScene()
    {
        GameManager.Instance.IsPaused = false;
        Time.timeScale = 1;
        PlayerPrefs.Save();
        SceneManager.LoadScene("Main_Menu");
    }
}
