using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class BestScore : MonoBehaviour
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

    void Update()
    {
        if (GameManager.Instance.Score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", GameManager.Instance.Score);
            _scoreTxt.text = ($"{PlayerPrefs.GetInt("BestScore")}");
        }
    }
}
