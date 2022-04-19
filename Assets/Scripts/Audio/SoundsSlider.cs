using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsSlider : MonoBehaviour
{
    private Slider slider;
    void Awake() => slider = GetComponent<Slider>();
    void OnEnable()
    {
        slider.onValueChanged.AddListener(value => AudioManager.Instance.Sounds.ChangeSoundsVolume(slider));
        slider.onValueChanged.AddListener(value => PlayCheckSound());
    }

    private void OnDisable() => slider.onValueChanged.RemoveAllListeners();
    private void OnDestroy() => slider.onValueChanged.RemoveAllListeners();
    private void PlayCheckSound()
    {
        AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.Button);
    }
}
