using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class Congratulations : MonoBehaviour
{
    private TextMeshProUGUI _scoreTxt;

    void Awake()
    {
        _scoreTxt = this.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetTextFieldForCongratulations()
    {
        if (YG2.lang == "ru") { _scoreTxt.text = ("Поздравляем!\nВаш счёт: " + ($"{GameManager.Instance.Score}")); }
        else if (YG2.lang == "tr") { _scoreTxt.text = ("Tebrikler!\nPuanınız:: " + ($"{GameManager.Instance.Score}")); }
        else { _scoreTxt.text = ("Congratulations!\nYour Score: " + ($"{GameManager.Instance.Score}")); }
    }
}
