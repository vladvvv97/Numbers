using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
            PlayerPrefs.SetInt("BestScore", 0);
            _scoreTxt.text = ($"{PlayerPrefs.GetInt("BestScore")}");
        }
    }

    void Update()
    {
        if (GameManager.Instance.SCORE > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", GameManager.Instance.SCORE);
            PlayerPrefs.Save();
            _scoreTxt.text = ($"{PlayerPrefs.GetInt("BestScore")}");
        }
    }
}
