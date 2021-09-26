using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextRewardTimeController : MonoBehaviour
{
    private TextMeshProUGUI _timeToNextRewardText;
    private DateTime dailyRewardUsed;
    private TimeSpan _timeToNextRewardValue;

    void Start()
    {
        foreach (var element in gameObject.GetComponentsInChildren<TextMeshProUGUI>()) 
        { 
            if (element.CompareTag("Time"))
            {
                _timeToNextRewardText = element;
                _timeToNextRewardText.text = "";
                return;
            }                
        }
    }

    void Update()
    {
        ShowTimeToNextReward();  
    }
    private void ShowTimeToNextReward()
    {
        _timeToNextRewardText.gameObject.SetActive(true);
        _timeToNextRewardValue = DateTime.Today.AddDays(1) - DateTime.Now;
        _timeToNextRewardText.text = $"{_timeToNextRewardValue.Hours:D2}:{_timeToNextRewardValue.Minutes:D2}:{_timeToNextRewardValue.Seconds:D2}";
    }

}
