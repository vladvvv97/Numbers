using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speed : MonoBehaviour
{
    [SerializeField] private AnimationCurve speedValueCurve;

    private Slider _slider;
    private float _time;

    void Awake()
    {
        _slider = this.GetComponentInChildren<Slider>();
    }

    void Start()
    {
        _slider.minValue = speedValueCurve[speedValueCurve.keys.GetLowerBound(0)].value;
        _slider.maxValue = speedValueCurve[speedValueCurve.length - 1].value;
    }
    void Update()
    {
        if(!GameManager.Instance.IsPaused)
        {
            SpeedUp();
            UpdateSlider();
        }
    }
    public void ResetSpeed()
    {
        _time = 0;
    }
    private void SpeedUp()
    {
        _time += Time.deltaTime;
        GameManager.Instance.Speed = speedValueCurve.Evaluate(_time);
    }

    private void UpdateSlider()
    {
        _slider.value = GameManager.Instance.Speed;
    }
}
