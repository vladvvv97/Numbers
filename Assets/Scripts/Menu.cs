using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("Main_Menu");
        PlayerPrefs.Save();
        Time.timeScale = 1;
    }
   
}
