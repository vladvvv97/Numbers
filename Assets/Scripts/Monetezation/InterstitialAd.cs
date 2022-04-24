using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] private string _iOSAdUnitId = "Interstitial_iOS";

    private string _adUnitId;
    void Awake()
    {
        InitializePlatform();      
    }
    void Start()
    {
        InitializeInterstitialAd();
    }

    private void InitializePlatform() => _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSAdUnitId : _androidAdUnitId;

    private void InitializeInterstitialAd() => LoadAd();

    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // Show the loaded content in the Ad Unit: 
    public void ShowAd(int chanceValue)
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
     
        int rnd = Random.Range(0, 101);

        Debug.Log("Showing Ad: " + _adUnitId);
        Debug.Log("RND = " + rnd);
        Debug.Log("Chance = " + chanceValue);

        if (rnd <= chanceValue)
        {
            Advertisement.Show(_adUnitId, this);
        }
        else
        {
            return;
        }       
    }

    // Implement Load Listener and Show Listener interface methods:  
    public void OnUnityAdsAdLoaded(string placementId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
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
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
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
}
