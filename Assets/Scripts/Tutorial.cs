using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject swipeArrowPrefab;
    [SerializeField] private GameObject tutorialVideoPrefab;
    private bool isTwoNumberCubeSwipe;
    private bool isThreeNumberCubeSwipe;
    public bool isFirstTime
    {
        get => YandexGame.savesData.isFirstSession;
        set => YandexGame.savesData.isFirstSession = value;
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
        if (!isFirstTime)
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
