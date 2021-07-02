using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Set In Inspector")]
    public float WaitForReadyToMerge = 1f;
    public float Speed = 3f;
    public int[] CubeValueTypes =       new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    public int[] CubeValueTypeChance =  new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    public Color[] CubeColors = new Color[] { Color.red, Color.yellow, Color.green, Color.blue, Color.cyan, Color.magenta, Color.gray, Color.grey, Color.gray, Color.black };

    [Header("Sets Dynamically, DON'T TOUCH")]
    public int CamHeight;
    public int CamWidth;
    public float Offset;
    public bool AllCubesReadyToMerge;
    public NumberCube[] AllCubesOnScene;
    


    [Header("Drag'&'Drop In Inspector")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject oneNumberCubePrefab;
    [SerializeField] private GameObject twoNumberCubePrefab;
    [SerializeField] private GameObject threeNumberCubePrefab;
    [SerializeField] private GameObject[] cubeTypeChance;

    private Transform _transform;
    private Vector3 _position;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        CamHeight = Mathf.RoundToInt(Camera.main.orthographicSize);
        CamWidth = Mathf.RoundToInt(CamHeight * Camera.main.aspect);
        Offset = oneNumberCubePrefab.transform.localScale.y / 2;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InstantiateNumberCube();
        }

        AllCubesOnScene = FindObjectsOfType<NumberCube>();

        CheckIfAllCubesReadyToMerge();

        // ------------------------- Control for Testing purpose --------------------------- //

        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameObject CurrentCube = Instantiate(oneNumberCubePrefab, RandomUpperBound(oneNumberCubePrefab));
            CurrentCube.transform.DetachChildren();
            Destroy(CurrentCube);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            GameObject CurrentCube = Instantiate(twoNumberCubePrefab, RandomUpperBound(twoNumberCubePrefab));
            CurrentCube.transform.DetachChildren();
            Destroy(CurrentCube);
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            GameObject CurrentCube = Instantiate(threeNumberCubePrefab, RandomUpperBound(threeNumberCubePrefab));
            CurrentCube.transform.DetachChildren();
            Destroy(CurrentCube);
        }

        // -------------------------------------------------------------------------------- //
    }

    private void CheckIfAllCubesReadyToMerge()
    {       
        if (Array.TrueForAll(AllCubesOnScene, element => element.ReadyToMerge == true))
        {
            AllCubesReadyToMerge = true;
        }
        else
        {
            AllCubesReadyToMerge = false;
        }
    }

    private void InstantiateNumberCube()
    {
        GameObject CubeType = cubeTypeChance[Random.Range(0, cubeTypeChance.Length)];
        GameObject CurrentCube = Instantiate(CubeType, RandomUpperBound(CubeType));
        CurrentCube.transform.DetachChildren();
        Destroy(CurrentCube);
    }

    private Transform RandomUpperBound(GameObject CubeType)
    {
        _transform = this.transform;        
        int rnd = Random.Range(-CamWidth, CamWidth + 1);

        if (CubeType.GetComponentInChildren<OneNumberCube>())
        {
            _position = new Vector3
            ((rnd < 0 ? CubeType.transform.localScale.y / 2 + rnd : -CubeType.transform.localScale.y / 2 + rnd),
            CamHeight - CubeType.transform.localScale.y / 2,
            0);
        }
        else if (CubeType.GetComponentInChildren<TwoNumberCube>())
        {
            _position = new Vector3
            ((rnd < 0 ? CubeType.transform.localScale.y + rnd : -CubeType.transform.localScale.y + rnd),
            CamHeight - CubeType.transform.localScale.y / 2,
            0);
        }
        else if (CubeType.GetComponentInChildren<ThreeNumberCube>())
        {
            _position = new Vector3
            ((rnd < 0 ? CubeType.transform.localScale.y * 1.5f + rnd : -CubeType.transform.localScale.y * 1.5f + rnd),
             CamHeight - CubeType.transform.localScale.y / 2,
            0);
        }
        else
        {
            throw new System.NotImplementedException();
        }

        _transform.position = _position;
        return _transform;
    }


}
