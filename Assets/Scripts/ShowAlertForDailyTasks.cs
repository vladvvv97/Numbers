using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowAlertForDailyTasks : MonoBehaviour
{
    private TextMeshProUGUI _alertNumberTMP;
    private Image _image;
    void Awake()
    {
        _alertNumberTMP = this.GetComponentInChildren<TextMeshProUGUI>();
        _image = this.GetComponent<Image>();
    }
    void Start()
    {
        ShowAlert();
        Subscribe();
    }
    private void OnDisable()
    {
        Unsubscribe();
    }

    private void ShowAlert(int index = 0)
    {
        if (TaskConditionManager.Instance.NumberOfCompletedTasks() == 0)
        {
            _image.enabled = false;
            _alertNumberTMP.enabled = false;
        }
        else
        {
            _image.enabled = true;
            _alertNumberTMP.enabled = true;
            _alertNumberTMP.text = TaskConditionManager.Instance.NumberOfCompletedTasks().ToString();
        }
       
    }
    private void ShowAlert()
    {
        if (TaskConditionManager.Instance.NumberOfCompletedTasks() == 0)
        {
            _image.enabled = false;
            _alertNumberTMP.enabled = false;
        }
        else
        {
            _image.enabled = true;
            _alertNumberTMP.enabled = true;
            _alertNumberTMP.text = TaskConditionManager.Instance.NumberOfCompletedTasks().ToString();
        }

    }
    private void Subscribe()
    {
        TaskConditionManager.Instance.OnLogin += ShowAlert;
        TaskConditionManager.Instance.OnGameFinished += ShowAlert;
        TaskConditionManager.Instance.OnScoreChanged += ShowAlert;
        TaskConditionManager.Instance.OnVideoWatched += ShowAlert;
        TaskManager.OnGetReward += ShowAlert;
    }
    private void Unsubscribe()
    {
        TaskConditionManager.Instance.OnLogin -= ShowAlert;
        TaskConditionManager.Instance.OnGameFinished -= ShowAlert;
        TaskConditionManager.Instance.OnScoreChanged -= ShowAlert;
        TaskConditionManager.Instance.OnVideoWatched -= ShowAlert;
        TaskManager.OnGetReward -= ShowAlert;
    }
}
