using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    public Canvas OptionScreen;
    public void LoadOptionScreen()
    {
        this.gameObject.SetActive(false);
        OptionScreen.gameObject.SetActive(true);
    }
}
