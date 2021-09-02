using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    public Canvas OptionScreen;
    public Canvas mainScreen;

    public void LoadMainScreenFromOptionsScreen()
    {
        mainScreen.gameObject.SetActive(true);
        OptionScreen.gameObject.SetActive(false);
    }

    public void LoadOptionScreen()
    {
        mainScreen.gameObject.SetActive(false);
        OptionScreen.gameObject.SetActive(true);
    }

    public void LoadPlayScreen()
    {
        SceneManager.LoadScene("Level_1");
    }

}
