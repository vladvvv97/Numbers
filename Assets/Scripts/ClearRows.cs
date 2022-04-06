using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearRows : MonoBehaviour
{
    public void ClearNumberOfRows(int quantity)
    {       
        float UpperBorderY = 4f;

        if (GameManager.Instance.AllCubesOnScene.Length > 0)
        {
            foreach (var element in GameManager.Instance.AllCubesOnScene)
            {
                if (element.transform.position.y > UpperBorderY - quantity)
                Destroy(element.gameObject);
            }
        }
    }
}
