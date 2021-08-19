using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    private Text _scoreTxt;

    void Awake()
    {
        _scoreTxt = this.GetComponent<Text>();
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            _scoreTxt.text = "Best Score: " + PlayerPrefs.GetInt("BestScore");
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", GameManager.Instance.SCORE);
            _scoreTxt.text = "Best Score: " + GameManager.Instance.SCORE;
        }
    }

    void Update()
    {
        if (GameManager.Instance.SCORE > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", GameManager.Instance.SCORE);
            PlayerPrefs.Save();
            _scoreTxt.text = "Best Score: " + GameManager.Instance.SCORE;
        }
    }
}
