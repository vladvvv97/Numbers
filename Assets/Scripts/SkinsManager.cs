using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsManager : MonoBehaviour
{
    public static SkinsManager Instance;
    public Sprite[] Cubes1;
    public Sprite[] Cubes2;
    public Sprite[] Cubes3;
    public Sprite[] Cubes4;
    public Sprite[] InGameBackgrounds;
    public Sprite[] Backlights;
    
    void Awake()
    {
        if (SkinsManager.Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("BacklightIndex"))
        {
            PlayerPrefs.SetInt("BacklightIndex", 0);
        }

        if (!PlayerPrefs.HasKey("InGameBackgroundIndex"))
        {
            PlayerPrefs.SetInt("InGameBackgroundIndex", 0);
        }

        if (!PlayerPrefs.HasKey("CubeIndex"))
        {
            PlayerPrefs.SetInt("CubeIndex", 0);
        }
    }

    public void SetBacklightIndex(int index)
    {
        PlayerPrefs.SetInt("BacklightIndex", index - 1);
        PlayerPrefs.Save();
    }
    public void SetInGameBackgroundIndex(int index)
    {
        PlayerPrefs.SetInt("InGameBackgroundIndex", index - 1);
        PlayerPrefs.Save();
    }
    public void SetCubeIndex(int index)
    {
        PlayerPrefs.SetInt("CubeIndex", index - 1);
        PlayerPrefs.Save();
    }
}
