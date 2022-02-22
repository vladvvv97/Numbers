using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowClearRowsCost : MonoBehaviour
{
    private TextMeshProUGUI _costTMP;

    void Awake()
    {
        _costTMP = this.GetComponent<TextMeshProUGUI>();

    }
    void Start()
    {
        _costTMP.text = /* "-" + */ CurrencyManager.Instance.ClearRowsCost.ToString();
    }
}
