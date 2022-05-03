using UnityEngine;
using UnityEngine.UI;
using YG;

public class DataManagerYG : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Start()
    {
        GetLoad();
    }

    public void Save()
    {
        YandexGame.savesData.TopScore = PlayerPrefs.GetInt("BestScore");

        YandexGame.SaveProgress();
        PlayerPrefs.Save();
    }

    public void Load() => YandexGame.LoadProgress();

    public void GetLoad()
    {
        if (!PlayerPrefs.HasKey("BestScore")) { PlayerPrefs.SetInt("BestScore", YandexGame.savesData.TopScore); }
    }
}
