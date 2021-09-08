using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeSkinsCheckmark : MonoBehaviour
{
    private Image _image;
    void Start()
    {
        _image = GetComponent<Image>();

        if (gameObject.name.Contains($"{PlayerPrefs.GetInt("CubeIndex") + 1}"))
        {
            _image.enabled = true;
        }
        else
        {
            _image.enabled = false;
        }

    }

}
