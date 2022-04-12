using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowAlertForAchievements : MonoBehaviour
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
        if (AchievementConditionManager.Instance.NumberOfCompletedAchievements() == 0)
        {
            _image.enabled = false;
            _alertNumberTMP.enabled = false;
        }
        else
        {
            _image.enabled = true;
            _alertNumberTMP.enabled = true;
            _alertNumberTMP.text = AchievementConditionManager.Instance.NumberOfCompletedAchievements().ToString();
        }

    }
    private void ShowAlert()
    {
        if (AchievementConditionManager.Instance.NumberOfCompletedAchievements() == 0)
        {
            _image.enabled = false;
            _alertNumberTMP.enabled = false;
        }
        else
        {
            _image.enabled = true;
            _alertNumberTMP.enabled = true;
            _alertNumberTMP.text = AchievementConditionManager.Instance.NumberOfCompletedAchievements().ToString();
        }

    }
    private void Subscribe()
    {
        AchievementConditionManager.Instance.OnAchievementLogin += ShowAlert;
        AchievementConditionManager.Instance.OnSwipe += ShowAlert;
        AchievementConditionManager.Instance.OnTopScoreChanged += ShowAlert;
        AchievementConditionManager.Instance.OnAchievementVideoWatched += ShowAlert;
        AchievementManager.OnGetAchievementReward += ShowAlert;
    }
    private void Unsubscribe()
    {
        AchievementConditionManager.Instance.OnAchievementLogin -= ShowAlert;
        AchievementConditionManager.Instance.OnSwipe -= ShowAlert;
        AchievementConditionManager.Instance.OnTopScoreChanged -= ShowAlert;
        AchievementConditionManager.Instance.OnAchievementVideoWatched -= ShowAlert;
        AchievementManager.OnGetAchievementReward -= ShowAlert;
    }
}
