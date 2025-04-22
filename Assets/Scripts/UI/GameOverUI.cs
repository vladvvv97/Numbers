using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    void OnEnable()
    {
        CurrencyManager.Instance.ResetRewardToAdd();
        CurrencyManager.Instance.AmountRewardCalculation();
        AudioManager.Instance.Music.MuteMusic();
    }
    void OnDestroy()
    {
        AudioManager.Instance.Music.MuteMusic();
    }
}
