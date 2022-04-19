using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Set In Inspector")]
    public float WaitForReadyToMerge;
    public float DelayToDropNewCubeAfterCubesMerged;
    public float Speed;
    public float DropSpeed;
    public float _visibleTimeOfBacklights;
    public int[] CubeValueTypeChance =  new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    [Header("Sets Dynamically, DON'T TOUCH")]
    [HideInInspector]public int Score = 0;
    [HideInInspector] public List<NumberCube> CubesToDestroy;
    [HideInInspector] public int CamHeight;
    [HideInInspector] public int CamWidth;
    [HideInInspector] public float Offset;
    [HideInInspector] public float _timer = 0;
    [HideInInspector] public bool AllCubesReadyToMerge = false;
    [HideInInspector] public bool ReadyToDropNewCube = true;
    [HideInInspector] public Vector3 MousePosition;
    [HideInInspector] public NumberCube[] AllCubesOnScene;
    [HideInInspector] public GameObject CurrentCube;
    [HideInInspector] public bool IsPaused = false;
    
    [Header("Drag'&'Drop In Inspector")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject oneNumberCubePrefab;
    [SerializeField] private GameObject twoNumberCubePrefab;
    [SerializeField] private GameObject threeNumberCubePrefab;
    [SerializeField] private GameObject[] cubeTypeChance;
    [SerializeField] public GameObject VFX;

    private Transform _transform;
    private Vector3 _position;
    
    private float _initialSpeed;    

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
        _initialSpeed = Speed;
        
        InstantiateNumberCube();

    }

    void FixedUpdate() // Update -> Fixed Update 18.04.2022
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (AllCubesReadyToMerge == true && IsPaused == false)
        {
            _timer += Time.deltaTime;

            if (_timer >= DelayToDropNewCubeAfterCubesMerged)
            {
                InstantiateNumberCube();
                _timer = 0;
                
            }
        }

        if (CubesToDestroy.Count != 0)
        {
            AddScore();
        }

        AllCubesOnScene = FindObjectsOfType<NumberCube>();

        CheckIfAllCubesReadyToMerge();
    }

    public void SetSpeedEqualDropSpeed()
    {
        if (CurrentCube)
        {
            Rigidbody2D[] rb2d = CurrentCube.GetComponentsInChildren<Rigidbody2D>();

            foreach (var item in rb2d)
            {
                item.velocity = new Vector2(0, -DropSpeed);
            }
        }
    }
    private void InstantiateNumberCube()
    {
        GameObject CubeType = cubeTypeChance[Random.Range(0, cubeTypeChance.Length)];
        CurrentCube = Instantiate(CubeType, RandomUpperBound(CubeType));
    }
    private void CheckIfAllCubesReadyToMerge()
    {
        if (AllCubesOnScene.Length > 0)
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

    public void AddScore()
    {
        int result = 0;

        for (int i = 0; i < CubesToDestroy.Count; i++)
        {
            result += CubesToDestroy[i].Value;
        }

        Score += result;

        CubesToDestroy.Clear();

        int rnd = Random.Range(1, 3);
        if (rnd == 1) { AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.DestroyFirst); }
        else if (rnd == 2) { AudioManager.Instance.Sounds.PlaySound(AudioManager.eAudioNames.DestroySecond); }
    }

    public int DropLine()
    {
        if (CurrentCube)
        {
            if (CurrentCube.GetComponentInChildren<OneNumberCube>())
            {
                if (MousePosition.x < -2)
                {
                    return 10;
                }
                else if (MousePosition.x >= -2 && MousePosition.x < -1)
                {
                    return 11;
                }
                else if (MousePosition.x >= -1 && MousePosition.x < 0)
                {
                    return 12;
                }
                else if (MousePosition.x >= 0 && MousePosition.x < 1)
                {
                    return 13;
                }
                else if (MousePosition.x >= 1 && MousePosition.x < 2)
                {
                    return 14;
                }
                else if (MousePosition.x >= 2)
                {
                    return 15;
                }
                else
                {
                    return 404;
                }
            }
            else if (CurrentCube.GetComponentInChildren<TwoNumberCube>())
            {
                if (MousePosition.x < -1.5)
                {
                    return 20;
                }
                else if (MousePosition.x >= -1.5 && MousePosition.x < -0.5)
                {
                    return 21;
                }
                else if (MousePosition.x >= -0.5 && MousePosition.x < 0.5)
                {
                    return 22;
                }
                else if (MousePosition.x >= 0.5 && MousePosition.x < 1.5)
                {
                    return 23;
                }
                else if (MousePosition.x >= 1.5)
                {
                    return 24;
                }
                else
                {
                    return 404;
                }
            }
            else if (CurrentCube.GetComponentInChildren<ThreeNumberCube>())
            {
                if (MousePosition.x < -1)
                {
                    return 30;
                }
                else if (MousePosition.x >= -1 && MousePosition.x < 0)
                {
                    return 31;
                }
                else if (MousePosition.x >= 0 && MousePosition.x < 1)
                {
                    return 32;
                }
                else if (MousePosition.x >= 1)
                {
                    return 33;
                }
                else
                {
                    return 404;
                }
            }
            else
            {
                return 404;
            }
        }
        else
        {
            return 404;
        }
    }

    public void CubeControll()
    {
        if (CurrentCube != null)
        {
            if (CurrentCube.GetComponentInChildren<OneNumberCube>())
            {
                if (DropLine() == 10)
                {
                    CurrentCube.transform.position = new Vector2(-2.5f, transform.position.y);
                }
                else if (DropLine() == 11)
                {
                    CurrentCube.transform.position = new Vector2(-1.5f, transform.position.y);
                }
                else if (DropLine() == 12)
                {
                    CurrentCube.transform.position = new Vector2(-0.5f, transform.position.y);
                }
                else if (DropLine() == 13)
                {
                    CurrentCube.transform.position = new Vector2(0.5f, transform.position.y);
                }
                else if (DropLine() == 14)
                {
                    CurrentCube.transform.position = new Vector2(1.5f, transform.position.y);
                }
                else if (DropLine() == 15)
                {
                    CurrentCube.transform.position = new Vector2(2.5f, transform.position.y);
                }
                else { return; }
            }
            else if (CurrentCube.GetComponentInChildren<TwoNumberCube>())
            {
                if (DropLine() == 20)
                {
                    CurrentCube.transform.position = new Vector2(-2f, transform.position.y);
                }
                else if (DropLine() == 21)
                {
                    CurrentCube.transform.position = new Vector2(-1f, transform.position.y);
                }
                else if (DropLine() == 22)
                {
                    CurrentCube.transform.position = new Vector2(0f, transform.position.y);
                }
                else if (DropLine() == 23)
                {
                    CurrentCube.transform.position = new Vector2(1f, transform.position.y);
                }
                else if (DropLine() == 24)
                {
                    CurrentCube.transform.position = new Vector2(2f, transform.position.y);
                }
                else { return; }
            }
            else if (CurrentCube.GetComponentInChildren<ThreeNumberCube>())
            {
                if (DropLine() == 30)
                {
                    CurrentCube.transform.position = new Vector2(-1.5f, transform.position.y);
                }
                else if (DropLine() == 31)
                {
                    CurrentCube.transform.position = new Vector2(-0.5f, transform.position.y);
                }
                else if (DropLine() == 32)
                {
                    CurrentCube.transform.position = new Vector2(0.5f, transform.position.y);
                }
                else if (DropLine() == 33)
                {
                    CurrentCube.transform.position = new Vector2(1.5f, transform.position.y);
                }
                else { return; }
            }
        }
    }

}
