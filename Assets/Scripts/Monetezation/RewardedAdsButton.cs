using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button _showAdButton;
    [SerializeField] private eNumSystem.eCurrencyType rewardType;
    [SerializeField] private int rewardValue;

    [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] private string _iOSAdUnitId = "Rewarded_iOS";

    private string _adUnitId;

    void Awake()
    {
        InitializePlatform();
    }
    void Start()
    {
        InitializeRewardedAdButton();
    }

    private void InitializePlatform() => _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSAdUnitId : _androidAdUnitId;

    private void InitializeRewardedAdButton()
    {
        StartCoroutine(LoadAdRewarded());
        _showAdButton.interactable = false;
    }
    private IEnumerator LoadAdRewarded()
    {
        yield return new WaitForSeconds(1f);
        LoadAd();
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            _showAdButton.onClick.AddListener(ShowAd);
            _showAdButton.interactable = true;
        }
    }
    public void ShowAd()
    {
        _showAdButton.interactable = false;
        Advertisement.Show(_adUnitId, this);
    }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");

            switch(rewardType)
            {
                case eNumSystem.eCurrencyType.Coin:
                    CurrencyManager.Instance.AddCoins(rewardValue);
                    break;
                case eNumSystem.eCurrencyType.Diamond:
                    CurrencyManager.Instance.AddDiamonds(rewardValue);
                    break;
                default:
                    break;
            }
            AchievementConditionManager.Instance.InvokeOnAchievementVideoWatchedAction();
            TaskConditionManager.Instance.InvokeOnVideoWatchedAction();
        }
        else { Debug.Log("Skipped"); }

        if (GameManager.Instance)
        {
            if (!GameManager.Instance.IsPaused)
            {
                Time.timeScale = 1;
                GameManager.Instance.TouchController.gameObject.SetActive(true);
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Time.timeScale = 1;
            }
        }
        AudioManager.Instance.Music.MuteMusic(false);
        Advertisement.Load(_adUnitId, this);
    }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        AudioManager.Instance.Music.MuteMusic(true);
        Time.timeScale = 0;
        if (GameManager.Instance)
        {
            GameManager.Instance.TouchController.gameObject.SetActive(false);
        }
    }
    public void OnUnityAdsShowClick(string adUnitId) { }

    private void OnDestroy()
    {
        _showAdButton.onClick.RemoveListener(ShowAd);
    }
}