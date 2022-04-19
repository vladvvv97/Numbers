using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    private Slider slider;
    void Awake() => slider = GetComponent<Slider>();
    void OnEnable() => slider.onValueChanged.AddListener(value => AudioManager.Instance.Music.ChangeMusicVolume(slider));
    private void OnDisable() => slider.onValueChanged.RemoveAllListeners();
    private void OnDestroy() => slider.onValueChanged.RemoveAllListeners();
}
