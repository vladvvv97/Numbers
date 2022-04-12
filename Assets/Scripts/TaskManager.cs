using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] [Tooltip("No./#/¹ Tasks in descending order")][Range(1,4)] private int rewardIndex;   
    [SerializeField] private int rewardValue;
    [SerializeField] private Color rewardValueTextColor;
    [SerializeField] private eNumSystem.eCurrencyType rewardType;
    [SerializeField] private Sprite[] rewardImages;
    [SerializeField] private string taskConditionTextValue;
    [SerializeField] private string taskConditionProgressCompleteText;
                     private int currentProgressValue;
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

    public static Action OnGetReward;

    void Awake()
    {
        InitializeTaskFields();
        InitializeDailyTask();
    }

    void Subscribe()
    {
        switch (rewardIndex)
        {
            case 1:
                TaskConditionManager.Instance.OnLogin += InitializeTaskFields;
                TaskConditionManager.Instance.OnLogin += InitializeDailyTask;
                break;
            case 2:
                TaskConditionManager.Instance.OnGameFinished += InitializeTaskFields;
                TaskConditionManager.Instance.OnGameFinished += InitializeDailyTask;
                break;
            case 3:
                TaskConditionManager.Instance.OnScoreChanged += InitializeTaskFields;
                TaskConditionManager.Instance.OnScoreChanged += InitializeDailyTask;
                break;
            case 4:
                TaskConditionManager.Instance.OnVideoWatched += InitializeTaskFields;
                TaskConditionManager.Instance.OnVideoWatched += InitializeDailyTask;
                break;

            default:
                return;
        }
    }
    void Unsubscribe()
    {
        switch (rewardIndex)
        {
            case 1:
                TaskConditionManager.Instance.OnLogin -= InitializeTaskFields;
                TaskConditionManager.Instance.OnLogin -= InitializeDailyTask;
                break;
            case 2:
                TaskConditionManager.Instance.OnGameFinished -= InitializeTaskFields;
                TaskConditionManager.Instance.OnGameFinished -= InitializeDailyTask;
                break;
            case 3:
                TaskConditionManager.Instance.OnScoreChanged -= InitializeTaskFields;
                TaskConditionManager.Instance.OnScoreChanged -= InitializeDailyTask;
                break;
            case 4:
                TaskConditionManager.Instance.OnVideoWatched -= InitializeTaskFields;
                TaskConditionManager.Instance.OnVideoWatched -= InitializeDailyTask;
                break;

            default:
                return;
        }
    }


    void Update()
    {
        if (DateTime.Now.Hour == DateTime.Today.Hour && DateTime.Now.Minute == DateTime.Today.Minute && DateTime.Now.Second == DateTime.Today.Second)
        {
            TaskConditionManager.Instance.ResetAllCompleteProgress();
            InitializeDailyTask();
        }
    }

    void OnEnable()
    {
        InitializeTaskFields();
        InitializeDailyTask();
        Subscribe();
    }
    void OnDisable()
    {
        Unsubscribe();
    }
    public void InitializeTaskFields(int index = 0)
    {
        rewardValueText.text = $"+{rewardValue}";
        rewardValueText.color = rewardValueTextColor;
        foreach (var i in rewardImages) { if (i.name == rewardType.ToString()) rewardImage.sprite = i; };

        TaskConditionManager.Instance.EndingProgressValue(rewardIndex, endingProgressValue);
        currentProgressValue = TaskConditionManager.Instance.CurrentProgressValue(rewardIndex);

        taskConditionText.text = taskConditionTextValue;
        taskConditionSlider.value = (float)currentProgressValue / (float)endingProgressValue;
        taskConditionProgressText.text = $"{currentProgressValue}/{endingProgressValue}";

        claimCheckmark.gameObject.SetActive(false);
    }


    private void InitializeDailyTask(int index = 0)
    {
        DateTime LastDateDailyRewardWasUsed = DateTimeManager.GetDateTime(eNumSystem.ePlayerPrefsNames.DailyRewardUsed.ToString() + rewardIndex.ToString(), DateTime.MinValue);
        if (LastDateDailyRewardWasUsed.DayOfYear == DateTime.Now.DayOfYear && LastDateDailyRewardWasUsed.Year == DateTime.Now.Year)
        {
            MakeTaskCompleted();
        }
        else
        {
            if (TaskConditionManager.Instance.IsTaskCompleted(rewardIndex))
            {
                MakeTaskCompletable();
            }
            else
            {
                MakeTaskUncompleted();
            }
        }
    }

    public void GetDailyReward()
    {
        DateTimeManager.SetDateTime(eNumSystem.ePlayerPrefsNames.DailyRewardUsed.ToString() + rewardIndex.ToString(), DateTime.Now);
        MakeTaskCompleted();
        AddReward();
        TaskConditionManager.Instance.ResetCompleteProgress(rewardIndex);
        TaskConditionManager.Instance.isTaskRewardTaken(rewardIndex, true);
        OnGetReward?.Invoke();
    }
    private void SaveData()
    {
        DateTimeManager.SetDateTime(eNumSystem.ePlayerPrefsNames.LastSaveTime.ToString(), DateTime.Now);
    }

    public void MakeTaskCompleted()
    {
        claimButton.interactable = false;
        claimCheckmark.gameObject.SetActive(true);
        claimText.gameObject.SetActive(false);
        taskConditionSlider.value = 1;
        taskConditionProgressText.text = taskConditionProgressCompleteText;
    }

    public void MakeTaskUncompleted()
    {        
        claimButton.interactable = false;
        claimCheckmark.gameObject.SetActive(false);
        claimText.gameObject.SetActive(true);
        taskConditionSlider.value = (float)currentProgressValue / (float)endingProgressValue;
        taskConditionProgressText.text = $"{currentProgressValue}/{endingProgressValue}";
    }

    public void MakeTaskCompletable()
    {
        claimButton.interactable = true;
        claimCheckmark.gameObject.SetActive(false);
        claimText.gameObject.SetActive(true);
        taskConditionSlider.value = (float)currentProgressValue / (float)endingProgressValue;
        taskConditionProgressText.text = $"{currentProgressValue}/{endingProgressValue}";
    }

    public void AddReward()
    {
        if (rewardType == eNumSystem.eCurrencyType.Coin)
        {
            CurrencyManager.Instance.AddCoins(rewardValue);
        }
        else if(rewardType == eNumSystem.eCurrencyType.Diamond)
        {
            CurrencyManager.Instance.AddDiamonds(rewardValue);
        }
        else
        {
            return;
        }
    }
}
