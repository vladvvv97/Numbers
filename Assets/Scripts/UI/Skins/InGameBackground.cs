using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameBackground : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        _image.sprite = SkinsManager.Instance.InGameBackgrounds[PlayerPrefs.GetInt("InGameBackgroundIndex")];
    }
}
