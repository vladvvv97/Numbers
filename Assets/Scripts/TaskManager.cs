using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private int rewardValue;
    [SerializeField] private Color rewardValueTextColor;
    [SerializeField] private eNumSystem.eCurrencyType rewardType;
    [SerializeField] private Sprite[] rewardImages;
    [SerializeField] private string taskConditionTextValue;
    [SerializeField] private int currentProgressValue;
    [SerializeField] private int endingProgressValue;

    [Header("Drag&Drop in Inspector")]
    [SerializeField] private TextMeshProUGUI rewardValueText;
    [SerializeField] private Image rewardImage;
    [SerializeField] private TextMeshProUGUI taskConditionText;
    [SerializeField] private Slider taskConditionSlider;
    [SerializeField] private TextMeshProUGUI taskConditionProgressText;
    [SerializeField] private Button claimButton;
    [SerializeField] private TextMeshProUGUI claimText;
    [SerializeField] private Image claimCheckmark;

    void Start()
    {
        InitializeTask();
    }

    private void InitializeTask()
    {
        rewardValueText.text = $"+{rewardValue}";
        rewardValueText.color = rewardValueTextColor;
        foreach (var i in rewardImages) { if (i.name == rewardType.ToString()) rewardImage.sprite = i; };

        taskConditionText.text = taskConditionTextValue;
        taskConditionSlider.value = (float)currentProgressValue / (float)endingProgressValue;
        taskConditionProgressText.text = $"{currentProgressValue}/{endingProgressValue}";

        claimCheckmark.gameObject.SetActive(false);
    }

}
