using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Congratulations : MonoBehaviour
{
    private TextMeshProUGUI _scoreTxt;

    void Awake()
    {
        _scoreTxt = this.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetTextFieldForCongratulations()
    {
        _scoreTxt.text += GameManager.Instance.Score;
    }
}
