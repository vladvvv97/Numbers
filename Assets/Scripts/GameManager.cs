using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int CamHeight;
    public static int CamWidth;

    public Camera MainCamera;
    public GameObject OneNumberCubePrefab;
    public GameObject TwoNumberCubePrefab;
    public GameObject ThreeNumberCubePrefab;

    [SerializeField] private GameObject[] cubeTypeChance;
    [SerializeField] private float offset;

    private Transform Transform;
    private Vector3 Position;

    void Awake()
    {
        CamHeight = Mathf.RoundToInt(Camera.main.orthographicSize);
        CamWidth = Mathf.RoundToInt(CamHeight * Camera.main.aspect);
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InstantiateNumberCube();
        }
       
    }

    void InstantiateNumberCube()
    {
        GameObject CubeType = cubeTypeChance[Random.Range(0, cubeTypeChance.Length)];
        GameObject CurrentCube = Instantiate(CubeType, RandomUpperBound(CubeType));
        CurrentCube.transform.parent = null;
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
