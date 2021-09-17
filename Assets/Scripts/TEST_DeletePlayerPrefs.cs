using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_DeletePlayerPrefs : MonoBehaviour
{
    public void DeletePlayerPref()
    {
        PlayerPrefs.DeleteAll();
    }
}
