using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyTaskSystem : MonoBehaviour
{
    [SerializeField] private bool isShowingTime;
    private Button _button;
    private TextMeshProUGUI _timeToNextRewardText;
    private DateTime dailyRewardUsed;
    private TimeSpan _timeToNextRewardValue;

    void Awake()
    {

    }

    void Start()
    {
        _button = gameObject.GetComponentInChildren<Button>();
        _timeToNextRewardText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        _timeToNextRewardText.text = "";
        InitializedDailyTasks();
    }

    void Update()
    {
        ShowTimeToNextReward();

        if (DateTime.Now.Hour == DateTime.Today.Hour && DateTime.Now.Minute == DateTime.Today.Minute && DateTime.Now.Second == DateTime.Today.Second)
        {
            InitializedDailyTasks();
        }        
    }

    private void InitializedDailyTasks()
    {
        dailyRewardUsed = GetDateTime(eNumSystem.ePlayerPrefsNames.DailyRewardUsed.ToString(), DateTime.MinValue);
        if (dailyRewardUsed.DayOfYear == DateTime.Now.DayOfYear && dailyRewardUsed.Year == DateTime.Now.Year)
        {
            Debug.Log("Already used");
            _button.interactable = false;
        }
        else
        {
            Debug.Log("Available");
            _button.interactable = true;
        }
    }

    private void ShowTimeToNextReward()
    {
        if (isShowingTime)
        {
            if (PlayerPrefs.HasKey(eNumSystem.ePlayerPrefsNames.DailyRewardUsed.ToString()))
            {
                _timeToNextRewardText.gameObject.SetActive(true);
                _timeToNextRewardValue = DateTime.Today.AddDays(1) - DateTime.Now;
                _timeToNextRewardText.text = $"{_timeToNextRewardValue.Hours:D2}:{_timeToNextRewardValue.Minutes:D2}:{_timeToNextRewardValue.Seconds:D2}";
            }
            else
            {
                _timeToNextRewardText.gameObject.SetActive(false);
            }
        }
        else
        {
            _timeToNextRewardText.gameObject.SetActive(false);
        }
    }

    public void GetDailyReward()
    {
        SetDateTime(eNumSystem.ePlayerPrefsNames.DailyRewardUsed.ToString(), DateTime.Now);
        _button.interactable = false;
        Debug.Log("Already used");
    }
    private void SaveData()
    {
        SetDateTime(eNumSystem.ePlayerPrefsNames.LastSaveTime.ToString(), DateTime.Now);
    }

    public void SetDateTime(string key, DateTime value)
    {
        string convertedToString = value.ToString("u", CultureInfo.InvariantCulture);
        PlayerPrefs.SetString(key, convertedToString);
    }

    public DateTime GetDateTime(string key, DateTime defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string stored = PlayerPrefs.GetString(key);
            DateTime result = DateTime.ParseExact(stored, "u", CultureInfo.InvariantCulture);
            return result;
        }
        else
        {
            return defaultValue;
        }
    }
}
