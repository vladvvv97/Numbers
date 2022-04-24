using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
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
        InitializeRewardedAd();
    }

    private void InitializePlatform() => _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSAdUnitId : _androidAdUnitId;

    private void InitializeRewardedAd()
    {
        StartCoroutine(LoadAdRewarded());
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
    }
    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
        }
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

}