using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    public Canvas mainScreen;
    public Canvas OptionScreen;
    public Canvas StoreScreen;
    public Canvas StoreCubesScreen;
    public Canvas StoreBackgroundsScreen;
    public Canvas StoreLightsScreen;

    public Sprite SoundON;
    public Sprite SoundOFF;
    public Sprite ONButton;
    public Sprite OFFButton;
    public Image ImSoundButton;
    public Image ImOnOffButton;


    public void LoadMainScreenFromOptionsScreen()
    {
        mainScreen.gameObject.SetActive(true);
        OptionScreen.gameObject.SetActive(false);
    }

    public void LoadMainScreenFromStoreScreen()
    {
        mainScreen.gameObject.SetActive(true);
        StoreScreen.gameObject.SetActive(false);
    }

    public void LoadStoreScreenFromSubStoreScreen()
    {
        StoreScreen.gameObject.SetActive(true);
        StoreCubesScreen.gameObject.SetActive(false);
        StoreBackgroundsScreen.gameObject.SetActive(false);
        StoreLightsScreen.gameObject.SetActive(false);
    }

    public void LoadOptionScreen()
    {
        mainScreen.gameObject.SetActive(false);
        OptionScreen.gameObject.SetActive(true);
    }

    public void LoadStoreScreen()
    {
        mainScreen.gameObject.SetActive(false);
        StoreScreen.gameObject.SetActive(true);
    }

    public void LoadStoreCubesScreen()
    {
        StoreScreen.gameObject.SetActive(false);
        StoreCubesScreen.gameObject.SetActive(true);
    }
    public void LoadStoreBackgroundsScreen()
    {
        StoreScreen.gameObject.SetActive(false);
        StoreBackgroundsScreen.gameObject.SetActive(true);
    }
    public void LoadStoreLightsScreen()
    {
        StoreScreen.gameObject.SetActive(false);
        StoreLightsScreen.gameObject.SetActive(true);
    }

    public void SoundImageSwap()
    {
        if (ImSoundButton.sprite == SoundON)
        {
            ImOnOffButton.sprite = OFFButton;
            ImSoundButton.sprite = SoundOFF;
            return;
        }

        if (ImSoundButton.sprite == SoundOFF)
        {
            ImOnOffButton.sprite = ONButton;
            ImSoundButton.sprite = SoundON;
            return;
        }
    }

    public void OnOffImageSwap()
    {
        if (ImOnOffButton.sprite == ONButton)
        {
            ImOnOffButton.sprite = OFFButton;
            ImSoundButton.sprite = SoundOFF;
            return;
        }

        if (ImOnOffButton.sprite == OFFButton)
        {
            ImOnOffButton.sprite = ONButton;
            ImSoundButton.sprite = SoundON;
            return;
        }
    }

    public void LoadPlayScreen()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}

