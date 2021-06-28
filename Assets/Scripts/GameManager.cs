using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Set In Inspector")]
    public float WaitForTriggerEnterCheckTime;
    public float Speed;

    [Header("Sets Dynamically, DON'T TOUCH")]
    public int CamHeight;
    public int CamWidth;

    [Header("Drag'&'Drop In Inspector")]
    [SerializeField] private Camera MainCamera;
    [SerializeField] private GameObject OneNumberCubePrefab;
    [SerializeField] private GameObject TwoNumberCubePrefab;
    [SerializeField] private GameObject ThreeNumberCubePrefab;
    [SerializeField] private GameObject[] cubeTypeChance;

    private Transform Transform;
    private Vector3 Position;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        CamHeight = Mathf.RoundToInt(Camera.main.orthographicSize);
        CamWidth = Mathf.RoundToInt(CamHeight * Camera.main.aspect);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InstantiateNumberCube();
        }

        // ------------------------- Control for Testing purpose --------------------------- //

        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameObject CurrentCube = Instantiate(OneNumberCubePrefab, RandomUpperBound(OneNumberCubePrefab));
            CurrentCube.transform.DetachChildren();
            Destroy(CurrentCube);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            GameObject CurrentCube = Instantiate(TwoNumberCubePrefab, RandomUpperBound(TwoNumberCubePrefab));
            CurrentCube.transform.DetachChildren();
            Destroy(CurrentCube);
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            GameObject CurrentCube = Instantiate(ThreeNumberCubePrefab, RandomUpperBound(ThreeNumberCubePrefab));
            CurrentCube.transform.DetachChildren();
            Destroy(CurrentCube);
        }

        // -------------------------------------------------------------------------------- //
    }

    void InstantiateNumberCube()
    {
        GameObject CubeType = cubeTypeChance[Random.Range(0, cubeTypeChance.Length)];
        GameObject CurrentCube = Instantiate(CubeType, RandomUpperBound(CubeType));
        CurrentCube.transform.DetachChildren();
        Destroy(CurrentCube);
    }

    Transform RandomUpperBound(GameObject CubeType)
    {
        Transform = this.transform;        
        int rnd = Random.Range(-CamWidth, CamWidth + 1);

        if (CubeType.GetComponentInChildren<OneNumberCube>())
        {
            Position = new Vector3
            ((rnd < 0 ? CubeType.transform.localScale.y / 2 + rnd : -CubeType.transform.localScale.y / 2 + rnd),
            CamHeight - CubeType.transform.localScale.y / 2,
            0);
        }
        else if (CubeType.GetComponentInChildren<TwoNumberCube>())
        {
            Position = new Vector3
            ((rnd < 0 ? CubeType.transform.localScale.y + rnd : -CubeType.transform.localScale.y + rnd),
            CamHeight - CubeType.transform.localScale.y / 2,
            0);
        }
        else if (CubeType.GetComponentInChildren<ThreeNumberCube>())
        {
            Position = new Vector3
            ((rnd < 0 ? CubeType.transform.localScale.y * 1.5f + rnd : -CubeType.transform.localScale.y * 1.5f + rnd),
             CamHeight - CubeType.transform.localScale.y / 2,
            0);
        }
        else
        {
            throw new System.NotImplementedException();
        }

        Transform.position = Position;
        return Transform;
    }

}
