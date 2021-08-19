using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentScore : MonoBehaviour
{
    private Text _scoreTxt;

    void Awake()
    {
        _scoreTxt = this.GetComponent<Text>();
    }
    void Update()
    {
        _scoreTxt.text = "" + GameManager.Instance.SCORE;
    }
}
