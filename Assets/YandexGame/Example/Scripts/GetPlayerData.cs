using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GetPlayerData : MonoBehaviour
{
    [SerializeField] ImageLoadYG imageLoad;
    [SerializeField] TextMeshProUGUI textPlayerData;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += DebugData;
    }
    private void OnDisable()
    {
        YandexGame.GetDataEvent -= DebugData;
    }

    private void Start()
    {
        if (YandexGame.startGame)
        {
            DebugData();
        }
    }

    void DebugData()
    {
        textPlayerData.text = YandexGame.playerName;

        if (imageLoad != null && YandexGame.auth)
            imageLoad.Load(YandexGame.playerPhoto);
    }
}
