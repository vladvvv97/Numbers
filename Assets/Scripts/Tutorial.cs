using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject swipeArrowPrefab;
    [SerializeField] private GameObject tutorialVideoPrefab;
    private bool isTwoNumberCubeSwipe;
    private bool isThreeNumberCubeSwipe;
    public bool isFirstTime
    {
        get => PlayerPrefs.GetInt(eNumSystem.eUserStatus.FirstTime.ToString()) == 0 ? false : true;
        set => PlayerPrefs.SetInt(eNumSystem.eUserStatus.FirstTime.ToString(), value == false ? 0 : 1);
    }
    void Start()
    {
        Initialize();
    }
    void Update()
    {
        if (GameManager.Instance.Score == 0) { tutorialVideoPrefab.SetActive(true); }
        else { tutorialVideoPrefab.SetActive(false); }
        if (GameManager.Instance.CurrentCube)
        {            
            if (GameManager.Instance.CurrentCube.tag == eNumSystem.eTagName.TwoNumberCube.ToString() && isTwoNumberCubeSwipe == false)
            {
                var currentCubes = GameManager.Instance.CurrentCube.GetComponentsInChildren<TwoNumberCube>();

                if(currentCubes[currentCubes.GetLowerBound(0)].Value != currentCubes[currentCubes.GetUpperBound(0)].Value)
                {
                    isTwoNumberCubeSwipe = true;
                    swipeArrowPrefab.SetActive(true);
                }                
            }
            else if (GameManager.Instance.CurrentCube.tag == eNumSystem.eTagName.ThreeNumberCube.ToString() && isThreeNumberCubeSwipe == false)
            {
                var currentCubes = GameManager.Instance.CurrentCube.GetComponentsInChildren<ThreeNumberCube>();

                if (!(currentCubes[0].Value == currentCubes[1].Value && currentCubes[0].Value == currentCubes[2].Value && currentCubes[1].Value == currentCubes[2].Value))
                {
                    isThreeNumberCubeSwipe = true;
                    swipeArrowPrefab.SetActive(true);
                }
            }
        }
        else { swipeArrowPrefab.SetActive(false);  }

    }
    private void Initialize()
    {
        if (PlayerPrefs.HasKey(eNumSystem.eUserStatus.FirstTime.ToString()))
        {
            Debug.Log("Not First Time");
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("First Time");
            isFirstTime = false;
        }
        swipeArrowPrefab.SetActive(false);
        tutorialVideoPrefab.SetActive(false);
    }
}
