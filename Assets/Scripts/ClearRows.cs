using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearRows : MonoBehaviour
{
    //public GameObject[] array;
    public void ClearNumberOfRows(int quantity)
    {       
        float UpperBorderY = 5f;

        if (GameManager.Instance.AllCubesOnScene.Length > 0)
        {
            //array = Array.FindAll(GameManager.Instance.AllCubesOnScene, element => transform.position.y > UpperBorderY - quantity);
            foreach (var element in GameManager.Instance.AllCubesOnScene)
            {
                if (element.transform.position.y > UpperBorderY - quantity)
                Destroy(element.gameObject);
            }
        }
        //FindObjectOfType<GameOverZone>()._alreadyChecked = false;
    }
}
