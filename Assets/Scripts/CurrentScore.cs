using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentScore : MonoBehaviour
{
    private TextMeshProUGUI _scoreTxt;

    void Awake()
    {
        _scoreTxt = this.GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        _scoreTxt.text = ($"{GameManager.Instance.SCORE}");
    }
}
