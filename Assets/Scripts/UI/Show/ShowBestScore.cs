using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowBestScore : MonoBehaviour
{
    private TextMeshProUGUI _scoreTxt;

    void Awake()
    {
        _scoreTxt = this.GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            _scoreTxt.text = ($"{PlayerPrefs.GetInt("BestScore")}");
        }
        else
        {
            _scoreTxt.text = ("0");
        }
    }

}
